import {Card, CardContent, Step, StepLabel, Stepper} from "@mui/material";

interface StepperProps {
  activeStep: number;
  steps: string[]// for now string array. should move to object based steps
}

const StepperCard = ({activeStep, steps}: StepperProps) => {
  return (
    <Card sx={{margin: 'auto', mt: 2, p: 2}}>
      <CardContent>
        <Stepper activeStep={activeStep} alternativeLabel>
          {steps.map((label) => (
            <Step key={label}>
              <StepLabel>{label}</StepLabel>
            </Step>
          ))}
        </Stepper>
      </CardContent>
    </Card>
  )
}

export default StepperCard