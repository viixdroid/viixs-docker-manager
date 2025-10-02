import type { FC } from 'react'
import type { SetupStep } from '../../_models/setup'
import { Box, Step, StepLabel, Stepper, styled, Typography, useMediaQuery, useTheme } from '@mui/material'
import { useNavigate } from 'react-router'

const ProgressSection = styled(Box)(({ theme }) => ({
  position: 'relative',
  zIndex: 1,
  [theme.breakpoints.up('md')]: {
    flex: 1,
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'center',
    marginTop: theme.spacing(4),
    marginBottom: theme.spacing(4),
  },
  [theme.breakpoints.down('md')]: {
    flex: 1,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'flex-start',
    marginLeft: theme.spacing(2),
    gap: theme.spacing(1),
  },
}))

const ProgressTitle = styled(Typography)(({ theme }) => ({
  marginBottom: theme.spacing(3),
  opacity: 0.8,
  fontWeight: 600,
  [theme.breakpoints.down('md')]: {
    display: 'none',
  },
}))

const MobileProgressText = styled(Typography)(() => ({
  fontSize: '0.75rem',
  opacity: 0.9,
  fontWeight: 500,
  letterSpacing: 0.5,
}))

const ProgessStepper = styled(Stepper)(({ theme }) => ({
  '& .MuiStepLabel-root .Mui-completed': { color: 'white' },
  '& .MuiStepLabel-root .Mui-active': { color: 'white' },
  '& .MuiStepLabel-label': { color: 'white', opacity: 0.6, fontSize: '1rem' },
  '& .MuiStepLabel-label.Mui-active': { opacity: 1, fontWeight: 600 },
  '& .MuiStepLabel-label.Mui-completed': { opacity: 0.8 },
  '& .MuiStepIcon-root': { color: 'rgba(255, 255, 255, 0.2)', fontSize: '2rem' },
  '& .MuiStepIcon-root.Mui-active': {
    'color': 'white',
    '& .MuiStepIcon-text': {
      fill: '#1e3a5f',
      fontWeight: 600,
    },
  },
  '& .MuiStepIcon-root.Mui-completed': {
    'color': 'white',
    '& .MuiStepIcon-text': {
      fill: '#1e3a5f',
    },
  },
  '& .MuiStepIcon-text': {
    fill: 'white',
  },
  '& .MuiStepConnector-line': { borderColor: 'rgba(255, 255, 255, 0.2)' },
  [theme.breakpoints.down('md')]: {
    'width': '100%',
    '& .MuiStepIcon-root': { fontSize: '1.75rem' },
    '& .MuiStepLabel-label': { display: 'none' },
    '& .MuiStepConnector-root': {
      flex: 1,
      marginLeft: theme.spacing(1),
      marginRight: theme.spacing(1),
    },
    '& .MuiStepConnector-line': {
      borderColor: 'rgba(255, 255, 255, 0.3)',
      borderTopWidth: 2,
    },
    '& .MuiStep-root': {
      paddingLeft: 0,
      paddingRight: 0,
    },
  },
}))

const ClickableStepLabel = styled(StepLabel)(() => ({
  'cursor': 'pointer',
  '&:hover': {
    cursor: 'pointer',
  },

}))

interface SetupProgressProps {
  currentStep: number
  setupSteps: SetupStep[]
}

const SetupProgress: FC<SetupProgressProps> = ({ currentStep, setupSteps }) => {
  const navigate = useNavigate()
  const theme = useTheme()
  const isMobile = useMediaQuery(theme.breakpoints.down('md'))

  return (
    <ProgressSection>
      <ProgressTitle variant="subtitle2">Your progress</ProgressTitle>
      {isMobile && (
        <MobileProgressText>
          {`${setupSteps[currentStep].title} - Step ${currentStep + 1} of ${setupSteps.length}`}
        </MobileProgressText>
      )}
      <ProgessStepper activeStep={currentStep} orientation={isMobile ? 'horizontal' : 'vertical'}>
        {setupSteps.map(step => (
          <Step key={step.stepId}>
            <ClickableStepLabel onClick={() => navigate(`/setup?step=${step.stepName}`)}>
              {step.title}
            </ClickableStepLabel>
          </Step>
        ))}
      </ProgessStepper>
    </ProgressSection>
  )
}

export default SetupProgress
