import type { FC } from 'react'
import { Box, Grid, styled } from '@mui/material'
import { Outlet } from 'react-router'
import NavigationButtons from '../(navigation)/NavigationButtons'
import SetupStepContent from './SetupStepContent'

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

interface SetupContentProps {
  // step: SetupStep
  isFirstStep: boolean
  isLastStep: boolean
  handleNext: () => void
  handleBack: () => void
}

const SetupContent: FC<SetupContentProps> = ({ handleBack, handleNext, isFirstStep, isLastStep }) => {
  return (
    <SetupContentContainer flexGrow={1} size={{ xs: 12, md: 9 }}>
      <SetupFormContainer>
        {/* <SetupStepContent step={step} /> */}
        <Outlet />
        <NavigationButtons
          handleBack={handleBack}
          handleNext={handleNext}
          isFirstStep={isFirstStep}
          isLastStep={isLastStep}
        />
      </SetupFormContainer>
    </SetupContentContainer>
  )
}

export default SetupContent
