import type { FC } from 'react'
import type { CreateDockLightEnvironmentCommand } from './_models/docklightEnvironment'
import type { SetupStep, SetupStepName } from './_models/setup'
import type { SetupStepHandler, SetupStepOutletContext } from './_models/setupHandler'
import type { CreateUserAccountCommand } from './_models/userAccount'
import { Box, Grid, styled } from '@mui/material'
import { useEffect, useState } from 'react'
import { Outlet, useNavigate, useSearchParams } from 'react-router'
import WebSocketProvider, { useWebSocketContext } from '../../components/providers/WebSocketHubProvider'
import NavigationButtons from './_components/(navigation)/NavigationButtons'
import SideBar from './_components/(sidebar)/SideBar'
import { SetupStepFactory } from './_factories/setupStepFactory'
import { FinishSetupCommand, SetupStartedCommand, SetupSteps, StartSetupCommand } from './_models/setup'

type SetupStepCommands
  = | CreateUserAccountCommand
    | CreateDockLightEnvironmentCommand

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

  const [isNextStepLoading, setIsNextStepLoading] = useState<boolean>(false)

  const { connection } = useWebSocketContext()
  const [setupId, setSetupId] = useState<string>()
  const [setupStep, setSetupStep] = useState<SetupStep>()

  const [onNextStepCallback, setOnNextStepCallback] = useState<(() => SetupStepHandler<SetupStepCommands> | undefined) | null>(null)

  const outletContext: SetupStepOutletContext<SetupStepCommands> = {
    isDisabled: isNextStepLoading,
    onNextStepCallback: callback => setOnNextStepCallback(() => callback),
  }

  const navigateStep = (setupStepName: SetupStepName) => {
    navigate(`/setup?step=${setupStepName.stepName}`)
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
          const setupCommand = SetupStepFactory.createSetupCommand(setupId!, setupStep?.currentStep.stepName ?? '', stepCommand)
          await setupCommand.execute()
        }
      }
      if (setupStep) {
        if (setupStep.currentStep.isLastStep) {
          connection?.stop()
          const finishSetupCommand = new FinishSetupCommand(setupId!)
          await finishSetupCommand.execute()
          navigate(`/environments`)
        }
        else if (setupStep.currentStep.isFirstStep) {
          const setupStartedCommand = SetupStepFactory.createSetupCommand(setupId!, setupStep?.currentStep.stepName ?? 'Welcome', new SetupStartedCommand(setupId!))
          await setupStartedCommand.execute()
        }
      }
    }
    finally {
      setIsNextStepLoading(false)
    }
  }

  useEffect(() => {
    if (connection) {
      const command = new StartSetupCommand(connection.connectionId!)
      command.execute()
    }
  }, [connection])

  useEffect(() => {
    if (connection) {
      connection.on('OnSetupStarted', (setupStep: SetupStep) => {
        setSetupId(setupStep.setupId)
        setSetupStep(setupStep)
        connection.off('OnSetupStarted')
      })
      connection.on('OnNextSetupStep', (nextSetupStep: SetupStep) => {
        setSetupStep(nextSetupStep)
        if (setupStep?.currentStep.order !== nextSetupStep.currentStep.order) {
          navigateStep(nextSetupStep.currentStep)
        }
      })
    }
    else {
      console.error('No connection')
    }
  }, [connection])

  useEffect(() => {
    if (currentStepName === null) {
      setSearchParams({ step: 'Welcome' })
    }
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
          currentStep={setupStep?.currentStep.order || 0}
          setupSteps={SetupSteps}
        />
        <SetupContentContainer flexGrow={1} size={{ xs: 12, md: 9 }}>
          <SetupFormContainer>
            <Outlet context={outletContext} />
            <NavigationButtons
              // isFirstStep={setupStep?.currentStep.isFirstStep || false}
              isLastStep={setupStep?.currentStep.isLastStep || false}
              // handleBack={() => { }}
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
