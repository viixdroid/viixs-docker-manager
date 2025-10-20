import type { FC } from 'react'
import type { SetupStepConfiguration } from './_models/setup.tsx'
import { Button, Grid, Step, StepLabel, Stepper, styled, Typography, useMediaQuery, useTheme } from '@mui/material'
import Box from '@mui/material/Box'
import { useEffect, useState } from 'react'
import { Navigate, useSearchParams } from 'react-router'
import ThemeSwitcherButton from '../../components/themes/ThemeSwitcherButton.tsx'
import SetupStepContent from './_components/(content)/SetupStepContent.tsx'
import { SetupSteps } from './_models/setup.tsx'

// const RootContainer = styled(Box)({
//   height: '100vh',
//   display: 'flex',
//   overflow: 'hidden',
// })

// const LeftColumn = styled(Grid)(({ theme }) => ({
//   'background': `linear-gradient(135deg, #0f172a 0%, #1e293b 50%, #0e7490 100%)`,
//   'color': 'white',
//   'display': 'flex',
//   'flexDirection': 'column',
//   'position': 'relative',
//   'overflow': 'hidden',
//   [theme.breakpoints.down('md')]: {
//     position: 'sticky',
//     top: 0,
//     zIndex: 1000,
//     minHeight: 'auto',
//     padding: theme.spacing(3, 2),
//     flexDirection: 'row',
//     alignItems: 'center',
//     justifyContent: 'space-between',
//   },
//   [theme.breakpoints.up('md')]: {
//     minHeight: '100vh',
//     padding: theme.spacing(8),
//   },
//   '&::before': {
//     content: '""',
//     position: 'absolute',
//     top: '-50%',
//     right: '-50%',
//     width: '100%',
//     height: '100%',
//     background: 'radial-gradient(circle, rgba(255,255,255,0.1) 0%, transparent 70%)',
//     borderRadius: '50%',
//   },
// }))

// const ThemeToggleButton = styled(Box)(({ theme }) => ({
//   'position': 'absolute',
//   'zIndex': 2,
//   'color': 'white',
//   '&:hover': {
//     backgroundColor: 'rgba(255, 255, 255, 0.1)',
//   },
//   [theme.breakpoints.down('md')]: {
//     top: 12,
//     right: 12,
//   },
//   [theme.breakpoints.up('md')]: {
//     top: 32,
//     right: 32,
//   },
// }))

// const ContentBox = styled(Box)({
//   position: 'relative',
//   zIndex: 1,
// })

// const HeroTitle = styled(Typography)(({ theme }) => ({
//   marginTop: theme.spacing(2),
//   fontWeight: 700,
//   lineHeight: 1.2,
//   color: 'white',
//   [theme.breakpoints.down('md')]: {
//     fontSize: '1.25rem',
//     marginTop: 0,
//   },
// }))

// const ProgressSection = styled(Box)(({ theme }) => ({
//   position: 'relative',
//   zIndex: 1,
//   [theme.breakpoints.up('md')]: {
//     flex: 1,
//     display: 'flex',
//     flexDirection: 'column',
//     justifyContent: 'center',
//     marginTop: theme.spacing(4),
//     marginBottom: theme.spacing(4),
//   },
//   [theme.breakpoints.down('md')]: {
//     flex: 1,
//     display: 'flex',
//     flexDirection: 'column',
//     alignItems: 'flex-start',
//     marginLeft: theme.spacing(2),
//     gap: theme.spacing(1),
//   },
// }))

// const ProgressTitle = styled(Typography)(({ theme }) => ({
//   marginBottom: theme.spacing(3),
//   opacity: 0.8,
//   fontWeight: 600,
//   [theme.breakpoints.down('md')]: {
//     display: 'none',
//   },
// }))

// const StyledStepper = styled(Stepper)(({ theme }) => ({
//   '& .MuiStepLabel-root .Mui-completed': { color: 'white' },
//   '& .MuiStepLabel-root .Mui-active': { color: 'white' },
//   '& .MuiStepLabel-label': { color: 'white', opacity: 0.6, fontSize: '1rem' },
//   '& .MuiStepLabel-label.Mui-active': { opacity: 1, fontWeight: 600 },
//   '& .MuiStepLabel-label.Mui-completed': { opacity: 0.8 },
//   '& .MuiStepIcon-root': { color: 'rgba(255, 255, 255, 0.2)', fontSize: '2rem' },
//   '& .MuiStepIcon-root.Mui-active': {
//     'color': 'white',
//     '& .MuiStepIcon-text': {
//       fill: '#1e3a5f',
//       fontWeight: 600,
//     },
//   },
//   '& .MuiStepIcon-root.Mui-completed': {
//     'color': 'white',
//     '& .MuiStepIcon-text': {
//       fill: '#1e3a5f',
//     },
//   },
//   '& .MuiStepIcon-text': {
//     fill: 'white',
//   },
//   '& .MuiStepConnector-line': { borderColor: 'rgba(255, 255, 255, 0.2)' },
//   [theme.breakpoints.down('md')]: {
//     'width': '100%',
//     '& .MuiStepIcon-root': { fontSize: '1.75rem' },
//     '& .MuiStepLabel-label': { display: 'none' },
//     '& .MuiStepConnector-root': {
//       flex: 1,
//       marginLeft: theme.spacing(1),
//       marginRight: theme.spacing(1),
//     },
//     '& .MuiStepConnector-line': {
//       borderColor: 'rgba(255, 255, 255, 0.3)',
//       borderTopWidth: 2,
//     },
//     '& .MuiStep-root': {
//       paddingLeft: 0,
//       paddingRight: 0,
//     },
//   },
// }))

// const FooterText = styled(Typography)(({ theme }) => ({
//   opacity: 0.7,
//   marginTop: 'auto',
//   [theme.breakpoints.down('md')]: {
//     display: 'none',
//   },
// }))

// const RightColumn = styled(Grid)(({ theme }) => ({
//   display: 'flex',
//   flexDirection: 'column',
//   backgroundColor: theme.palette.background.default,
//   overflowY: 'auto',
//   [theme.breakpoints.down('md')]: {
//     minHeight: 'auto',
//     padding: theme.spacing(3, 2),
//   },
//   [theme.breakpoints.up('md')]: {
//     minHeight: '100vh',
//     maxHeight: '100vh',
//     paddingLeft: theme.spacing(6),
//     paddingRight: theme.spacing(6),
//     paddingTop: theme.spacing(6),
//     paddingBottom: theme.spacing(6),
//   },
// }))

// const TitleSection = styled(Box)(({ theme }) => ({
//   marginBottom: theme.spacing(4),
// }))

// const TitleRow = styled(Box)(({ theme }) => ({
//   display: 'flex',
//   alignItems: 'center',
//   gap: theme.spacing(2),
//   marginBottom: theme.spacing(2),
// }))

// const StepTitle = styled(Typography)({
//   fontWeight: 600,
// })

// const FormContainer = styled(Box)(() => ({
//   flex: 1,
//   display: 'flex',
//   flexDirection: 'column',
//   justifyContent: 'space-between',
// }))

// const FormContentWrapper = styled(Box)({
//   display: 'flex',
//   flexDirection: 'column',
//   justifyContent: 'center',
//   flex: 1,
// })

// const ButtonContainer = styled(Box)(({ theme }) => ({
//   display: 'flex',
//   justifyContent: 'flex-end',
//   gap: theme.spacing(2),
//   marginTop: theme.spacing(4),
//   paddingTop: theme.spacing(3),
//   borderTop: `1px solid ${theme.palette.divider}`,
// }))

// const BackButton = styled(Button)<{ isFirstStep: boolean }>(({ isFirstStep }) => ({
//   paddingLeft: 32,
//   paddingRight: 32,
//   minWidth: 120,
//   visibility: isFirstStep ? 'hidden' : 'visible',
// }))

// const NextButton = styled(Button)({
//   paddingLeft: 32,
//   paddingRight: 32,
//   minWidth: 120,
// })

// // const MobileProgressText = styled(Typography)(() => ({
// //   fontSize: '0.75rem',
// //   opacity: 0.9,
// //   fontWeight: 500,
// //   letterSpacing: 0.5,
// // }))

const SetupPage: FC = () => {
  const [searchParams] = useSearchParams()

  const [setupStep, setSetupStep] = useState<SetupStepConfiguration>()

  useEffect(() => {
    const step = searchParams.get('step')
    if (step === null) { // Step is set and managed in layout. we wait until it is set
      return
    }
    const setupStep = SetupSteps.find(s => s.stepName === step)
    if (setupStep === undefined) {
      throw new Error(`Invalid step: ${step}`)
    }
    setSetupStep(setupStep)
  }, [searchParams])

  return (
    <>
      {setupStep && (
        <SetupStepContent
          step={setupStep}
        />
      )}
    </>
  )
  // return (
  //   <Navigate to="/setup/01" replace={true} />

  // const theme = useTheme()
  // const [activeStep, setActiveStep] = useState<number>(0)
  // const isMobile = useMediaQuery(theme.breakpoints.down('md'))

  // const isLastStep = () => activeStep === SetupSteps.length - 1

  // const handleGoToPreviousStep = () => setActiveStep(Math.max(activeStep - 1, 0))

  // const handleGoToNextStep = () => setActiveStep(Math.min(activeStep + 1, SetupSteps.length - 1))

  // return (
  //   <RootContainer>
  //     <Grid container sx={{ flex: 1, height: '100%', flexDirection: { xs: 'column', md: 'row' } }}>
  //       <LeftColumn size={{ xs: 12, md: 3 }}>
  //         <ThemeToggleButton>
  //           <ThemeSwitcherButton />
  //         </ThemeToggleButton>

  //         <ContentBox sx={{ display: { xs: 'none', md: 'block' } }}>
  //           <HeroTitle variant="h3" as="h1">
  //             Viix's Docker Manager Setup
  //           </HeroTitle>
  //         </ContentBox>

  //         <ContentBox sx={{ display: { xs: 'block', md: 'none' } }}>
  //           <HeroTitle variant="h6" as="h1">
  //             Setup
  //           </HeroTitle>
  //         </ContentBox>

  //         <ProgressSection>
  //           <ProgressTitle variant="subtitle2">Your progress</ProgressTitle>
  //           {isMobile && (
  //             <MobileProgressText>
  //               Step
  //               {' '}
  //               {activeStep + 1}
  //               {' '}
  //               of
  //               {' '}
  //               {SetupSteps.length}
  //             </MobileProgressText>
  //           )}
  //           <StyledStepper activeStep={activeStep} orientation={isMobile ? 'horizontal' : 'vertical'}>
  //             {SetupSteps.map(step => (
  //               <Step key={step.step}>
  //                 <StepLabel>{step.title}</StepLabel>
  //               </Step>
  //             ))}
  //           </StyledStepper>
  //         </ProgressSection>

  //         <ContentBox>
  //           <FooterText variant="body2">v1.1.0</FooterText>
  //         </ContentBox>
  //       </LeftColumn>

  //       <RightColumn flexGrow={1} size={{ xs: 12, md: 9 }}>
  //         <FormContainer>
  //           <FormContentWrapper>
  //             <TitleSection>
  //               <TitleRow>
  //                 <StepTitle variant="h4">
  //                   {SetupSteps.find(ss => ss.step === activeStep)?.title}
  //                 </StepTitle>
  //               </TitleRow>
  //               <Typography variant="body1" color="text.primary">
  //                 {SetupSteps.find(ss => ss.step === activeStep)?.description}
  //               </Typography>
  //             </TitleSection>

  //             {SetupSteps.find(ss => ss.step === activeStep) === undefined
  //               ? (
  //                   null
  //                 )
  //               : (
  //                   SetupSteps.find(ss => ss.step === activeStep)?.component
  //                 )}
  //           </FormContentWrapper>

  //           <ButtonContainer>
  //             <BackButton
  //               variant="outlined"
  //               color="info"
  //               disabled={activeStep === 0}
  //               onClick={handleGoToPreviousStep}
  //               size="large"
  //               isFirstStep={activeStep === 0}
  //             >
  //               Back
  //             </BackButton>
  //             <NextButton
  //               variant="contained"
  //               color="secondary"
  //               onClick={handleGoToNextStep}
  //               size="large"
  //             >
  //               {isLastStep() ? 'Finish' : 'Next'}
  //             </NextButton>
  //           </ButtonContainer>
  //         </FormContainer>
  //       </RightColumn>
  //     </Grid>
  //   </RootContainer>
  // )
}

export default SetupPage
