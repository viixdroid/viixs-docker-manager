import type { FC } from 'react'
import { Box, Grid, styled, useMediaQuery, useTheme } from '@mui/material'
import { use, useEffect, useState } from 'react'
import { Outlet, useNavigate, useSearchParams } from 'react-router'
import SetupContent from './_components/(content)/SetupContent'
import NavigationButtons from './_components/(navigation)/NavigationButtons'
import SideBar from './_components/(sidebar)/SideBar'
import { SetupSteps } from './_models/setup'

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

const SetupLayout: FC = () => {
  const navigate = useNavigate()

  const [searchParams, setSearchParams] = useSearchParams()
  const currentStepName = searchParams.get('step')

  const [currentStepId, setCurrentStepId] = useState<number>(0)

  const [isNextStepLoading, setIsNextStepLoading] = useState<boolean>(false)

  const isLastStep = () => currentStepId === SetupSteps[SetupSteps.length - 1].stepId
  // const theme = useTheme()
  // const isMobile = useMediaQuery(theme.breakpoints.down('md'))

  const [onBeforeNavigateCallback, setOnBeforeNavigateCallback] = useState<(() => Promise<boolean>) | null>(null)

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
    setCurrentStepId(step.stepId)
  }, [currentStepName])

  const navigateStep = (stepName: string) => {
    navigate(`/setup?step=${stepName}`)
  }

  const handleBack = () => {
    const previousStepId = currentStepId - 1
    const previousStepName = SetupSteps.find(s => s.stepId === previousStepId)?.stepName
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
      if (onBeforeNavigateCallback) {
        const canNavigate = await onBeforeNavigateCallback()
        if (!canNavigate) {
          setIsNextStepLoading(false)
          return
        }
      }

      const nextStepId = currentStepId + 1
      const nextStepName = SetupSteps.find(s => s.stepId === nextStepId)?.stepName
      if (isLastStep()) {
        navigate(`/environments`)
      }
      if (nextStepName === undefined) {
        return
      }
      navigateStep(nextStepName)
    }
    finally {
      setIsNextStepLoading(false)
    }
  }

  return (
    <RootContainer>
      <Grid container sx={{ flex: 1, height: '100%', flexDirection: { xs: 'column', md: 'row' } }}>
        <SideBar
          title="Viixs Docker Manager Setup"
          mobileTitle="Setup"
          footerContent="v1.1.0"
          currentStep={currentStepId}
          setupSteps={SetupSteps}
        />
        <SetupContentContainer flexGrow={1} size={{ xs: 12, md: 9 }}>
          <SetupFormContainer>
            <Outlet
              context={{
                registerOnBeforeNavigate: (callback: () => Promise<boolean>) =>
                  setOnBeforeNavigateCallback(() => callback),
              }}
            />
            <NavigationButtons
              isFirstStep={currentStepId === SetupSteps[0].stepId}
              isLastStep={isLastStep()}
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

export default SetupLayout
