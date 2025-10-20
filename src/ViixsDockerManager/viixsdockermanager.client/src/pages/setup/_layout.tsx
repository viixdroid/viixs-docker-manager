import type { FC } from 'react'
import type { WebSocketClient } from '../../clients/WebSocketClient'
import type { SetupStep, SetupStepConfiguration, SetupStepName, SetupStepNameStrings } from './_models/setup'
import type { SetupStepHandler, SetupStepOutletContext } from './_models/setupHandler'
import type { CreateUserAccountCommand } from './_models/userAccount'
import { Box, Grid, styled } from '@mui/material'
import { useEffect, useRef, useState } from 'react'
import { Outlet, useNavigate, useSearchParams } from 'react-router'
import { WebSocketClientManager } from '../../clients/managers/WebSocketClientManager'
import WebSocketProvider, { useWebSocketContext } from '../../components/providers/WebSocketHubProvider'
import NavigationButtons from './_components/(navigation)/NavigationButtons'
import SideBar from './_components/(sidebar)/SideBar'
import { SetupStepFactory } from './_factories/setupStepFactory'
import { SetupStartedCommand, SetupSteps, StartSetupCommand } from './_models/setup'

type SetupStepCommands
  = | CreateUserAccountCommand

const RootContainer = styled(Box)({
  height: '100vh',
  display: 'flex',
  overflow: 'hidden',
})

const SetupContentContainer = styled(Grid)(({ theme }) => ({
  display: 'flex',
  flexDirection: 'column',
  backgroundColor: theme.palette.background.default,
  overflowY: 'auto',
  [theme.breakpoints.down('md')]: {
    minHeight: 'auto',
    padding: theme.spacing(3, 2),
  },
  [theme.breakpoints.up('md')]: {
    minHeight: '100vh',
    maxHeight: '100vh',
    paddingLeft: theme.spacing(6),
    paddingRight: theme.spacing(6),
    paddingTop: theme.spacing(6),
    paddingBottom: theme.spacing(6),
  },
}))

const SetupFormContainer = styled(Box)(() => ({
  flex: 1,
  display: 'flex',
  flexDirection: 'column',
  justifyContent: 'space-between',
}))

interface SetupLayoutProps {
}

const SetupLayout: FC<SetupLayoutProps> = () => {
  const navigate = useNavigate()

  const [searchParams, setSearchParams] = useSearchParams()
  const currentStepName = searchParams.get('step')

  const [currentStepId, setCurrentStepId] = useState<number>(0)

  const [isNextStepLoading, setIsNextStepLoading] = useState<boolean>(false)

  const [webSocketClientManager] = useState<WebSocketClientManager>(new WebSocketClientManager())
  const [webSocketClient, setWebSocketClient] = useState<WebSocketClient>()
  const { connection } = useWebSocketContext()
  const [setupId, setSetupId] = useState<string>()
  const [setupStep, setSetupStep] = useState<SetupStep>()

  const isLastStep = () => currentStepId === SetupSteps[SetupSteps.length - 1].stepOrder
  // const theme = useTheme()
  // const isMobile = useMediaQuery(theme.breakpoints.down('md'))

  const [onNextStepCallback, setOnNextStepCallback] = useState<(() => SetupStepHandler<SetupStepCommands> | undefined) | null>(null)

  const outletContext: SetupStepOutletContext<SetupStepCommands> = {
    isDisabled: isNextStepLoading,
    onNextStepCallback: callback => setOnNextStepCallback(() => callback),
  }

  // useEffect(() => {
  //   const createWebSocketClient = async () => {
  //     const webSocketClient = webSocketClientManager.getClient('setup')
  //     await webSocketClient.start()
  //       .then(() => {
  //         setWebSocketClient(webSocketClient)
  //       })
  //     // .then(() => {
  //   }

  //   createWebSocketClient()
  //   // })
  // }, [currentStepId])

  // useEffect(() => {

  // }, [])
  const navigateStep = (setupStepName: SetupStepName) => {
    navigate(`/setup?step=${setupStepName.stepName}`)
  }
  const handleBack = () => {
    // get previous step name and navigate to it.
    // maybe save it in a state?

    const previousStepId = currentStepId - 1
    const previousStepName = SetupSteps.find(s => s.stepOrder === previousStepId)?.stepName
    if (previousStepName === undefined) {
      return
    }
    navigateStep(previousStepName)
  }

  const handleNext = async () => {
    if (isNextStepLoading) {
      return
    }
    setIsNextStepLoading(true)
    try {
      if (onNextStepCallback) {
        const stepCommand = onNextStepCallback()
        if (stepCommand) {
          console.log(`Current setupStepname? : ${setupStep?.currentStep.stepName}`)
          const setupCommand = SetupStepFactory.createSetupCommand(setupId!, setupStep?.currentStep.stepName ?? '', stepCommand) // TODO: Actually handle correct step name
          await setupCommand.execute()
        }
      }
      if (setupStep) {
        console.log(`Current Step: ${setupStep.currentStep.stepName}, Next Step: ${setupStep.nextStep.stepName}`)
        if (setupStep.nextStep.isLastStep) {
          navigate(`/environments`)
        }
        else if (setupStep.currentStep.isFirstStep) {
          console.log(`${setupStep.currentStep.stepName} + ${setupStep.nextStep.stepName} + ${setupStep.currentStep.isFirstStep}`)

          const setupStartedCommand = SetupStepFactory.createSetupCommand(setupId!, setupStep?.currentStep.stepName ?? 'Welcome', new SetupStartedCommand(setupId!))
          await setupStartedCommand.execute()
        }
        // else {
        //   if (setupStep.nextStep.stepName !== setupStep.currentStep.stepName) {
        //     navigateStep(setupStep.nextStep)
        //   }
        // }
      }

      // // Should be done after getting information from the websockets.
      // const nextStepId = currentStepId + 1
      // const nextStepName = SetupSteps.find(s => s.stepId === nextStepId)?.stepName
      // if (isLastStep()) {
      //   navigate(`/environments`)
      // }
      // if (nextStepName === undefined) {
      //   return
      // }
      // navigateStep(nextStepName)
    }
    finally {
      setIsNextStepLoading(false)
    }
  }

  useEffect(() => {
    if (connection) {
      console.log(connection.connectionId)
      const command = new StartSetupCommand(connection.connectionId!)
      command.execute()
    }

    // if (!webSocketClient || !webSocketClient.isConnected) {
    //   // console.log('not connected?')
    //   return
    // }
    // if (!connectionId) {
    //   // console.log(connectionId)
    // }
  }, [connection])

  useEffect(() => {
    if (connection) {
      connection.on('OnSetupStarted', (setupStep: SetupStep) => {
        console.log(`${setupStep.setupId} + ${setupStep.currentStep.stepName} + ${setupStep.nextStep.stepName}`)
        setSetupId(setupStep.setupId)
        setSetupStep(setupStep)
      })
      connection.on('OnNextSetupStep', (nextSetupStep: SetupStep) => {
        console.log(`Next step: ${nextSetupStep.nextStep.stepName}`)
        setSetupStep(nextSetupStep)
        if (setupStep?.currentStep.order !== nextSetupStep.nextStep.order) {
          navigateStep(nextSetupStep.nextStep)
        }
        // if (setupStep?.currentStep.order === nextSetupStep.currentStep.order) {
        //   console.log('Step orders are the same, not navigating.')
        //   setSetupStep(nextSetupStep)
        //   return
        // }

        // if (setupStep?.currentStep.stepName !== nextSetupStep.nextStep.stepName) {
        //   setSetupStep(nextSetupStep)
        //   navigateStep(nextSetupStep.nextStep)
        // } else {
        //   setSetupStep(nextSetupStep)
        // }
      })
    }
    else {
      console.error('No connection')
    }
  }, [connection])

  useEffect(() => {
    if (currentStepName === null) {
      setSearchParams({ step: 'Welcome' })
      return
    }
    const localCurrentStepName = searchParams.get('step')
    const step = SetupSteps.find(s => s.stepName === localCurrentStepName)
    if (step === undefined) {
      return
    }
    setCurrentStepId(step.stepOrder)
  }, [])

  return (
    <RootContainer>
      <Grid container sx={{ flex: 1, height: '100%', flexDirection: { xs: 'column', md: 'row' } }}>
        <SideBar
          title="Viixs Docker Manager Setup"
          mobileTitle="Setup"
          footerContent={(
            <>
              v1.1.0
              {' '}
              <br />
              {setupId}
            </>
          )}
          currentStep={currentStepId}
          setupSteps={SetupSteps}
        />
        <SetupContentContainer flexGrow={1} size={{ xs: 12, md: 9 }}>
          <SetupFormContainer>
            <Outlet context={outletContext} />
            <NavigationButtons
              isFirstStep={setupStep?.currentStep.isFirstStep || false}
              isLastStep={setupStep?.currentStep.isLastStep || false}
              handleBack={handleBack}
              handleNext={handleNext}
              isNextStepLoading={isNextStepLoading}
            />
          </SetupFormContainer>
        </SetupContentContainer>
      </Grid>
    </RootContainer>
  )
}

const SetupLayoutWithWebSocket: FC = () => {
  return (
    <WebSocketProvider endpoint="setup">
      <SetupLayout />
    </WebSocketProvider>
  )
}
export default SetupLayoutWithWebSocket
