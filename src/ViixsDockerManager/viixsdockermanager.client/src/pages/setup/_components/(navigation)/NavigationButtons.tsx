import type { FC } from 'react'
import { Box, Button, CircularProgress, styled } from '@mui/material'

const ButtonContainer = styled(Box)(({ theme }) => ({
  display: 'flex',
  justifyContent: 'flex-end',
  gap: theme.spacing(2),
  marginTop: theme.spacing(4),
  paddingTop: theme.spacing(3),
  borderTop: `1px solid ${theme.palette.divider}`,
}))

const BackButton = styled(Button)(() => ({
  paddingLeft: 32,
  paddingRight: 32,
  minWidth: 120,
}))

const NextButton = styled(Button)({
  paddingLeft: 32,
  paddingRight: 32,
  minWidth: 120,
})

interface NavigationButtonsProps {
  handleBack: () => void
  handleNext: () => void
  isFirstStep: boolean
  isLastStep: boolean
  isNextStepLoading?: boolean
}

const NavigationButtons: FC<NavigationButtonsProps> = ({
  handleBack,
  handleNext,
  isFirstStep,
  isLastStep,
  isNextStepLoading,
}) => {
  return (
    <ButtonContainer>
      {isFirstStep
        ? null
        : (
            <BackButton
              variant="outlined"
              size="large"
              color="info"
              onClick={handleBack}
            >
              Back
            </BackButton>
          )}

      <NextButton
        variant="contained"
        color="secondary"
        onClick={handleNext}
        size="large"
        disabled={isNextStepLoading}
        endIcon={isNextStepLoading ? <CircularProgress size={20} /> : null}
      >
        {isNextStepLoading ? 'Processing ...' : isLastStep ? 'Finish' : 'Next'}
      </NextButton>
    </ButtonContainer>
  )
}

export default NavigationButtons
