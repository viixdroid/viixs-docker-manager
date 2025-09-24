import type { FC } from 'react'
import { Button, Container, Grid, Stack, Typography } from '@mui/material'
import Box from '@mui/material/Box'
import { useState } from 'react'
import SetupCard from '../../components/setup/SetupCard.tsx'
import StepperCard from '../../components/setup/StepperCard.tsx'
import InitialDocklightEnvironment from './(steps)/initialDocklightEnvironment.tsx'
import RegisterNewUserStep from './(steps)/registerNewUser.tsx'

const steps = ['Create user account', 'Setup initial docker environment']

const Setup: FC = () => {
  const [
    activeStep,
    setActiveStep,
  ] = useState<number>(0)

  const isLastStep = () => activeStep === steps.length - 1

  const handleGoToPreviousStep = () => setActiveStep(Math.max(activeStep - 1, 0))

  const handleGoToNextStep = () => setActiveStep(Math.min(activeStep + 1, steps.length - 1))

  return (
    <>
      <Container maxWidth="md">
        <Box sx={{
          height: '100vh',
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
        }}
        >
          <Stack spacing={2} alignItems="center">
            <Box sx={{
              justifyContent: 'start',
            }}
            >
              <Typography component="h1" variant="h4" sx={{ mb: 4 }}>
                Set up Viix's Docker Manager
              </Typography>
            </Box>

            <Box sx={{ width: '100%' }}>
              <StepperCard
                activeStep={activeStep}
                steps={steps}
              />

              {activeStep === 0
                && (
                  <SetupCard title={steps[activeStep]}>
                    <RegisterNewUserStep />
                  </SetupCard>
                )}
              {activeStep === 1
                && (
                  <SetupCard title={steps[activeStep]}>
                    <InitialDocklightEnvironment />
                  </SetupCard>
                )}
            </Box>

            <Box sx={{
              display: 'flex',
              justifyContent: 'flex-end',
              width: '100%',
            }}
            >
              <Grid container spacing={2} sx={{ mt: 2 }}>
                <Grid size={6}>
                  <Button
                    fullWidth
                    color="info"
                    variant="outlined"
                    disabled={activeStep === 0}
                    onClick={handleGoToPreviousStep}
                  >
                    Terug
                  </Button>
                </Grid>

                <Grid size={6}>
                  {isLastStep()
                    ? (
                        <Button fullWidth variant="contained" color="info">
                          Afronden
                        </Button>
                      )
                    : (
                        <Button fullWidth variant="contained" color="info" onClick={handleGoToNextStep}>
                          Volgende
                        </Button>
                      )}
                </Grid>
              </Grid>
            </Box>
          </Stack>
        </Box>
      </Container>
    </>
  )
}

export default Setup
