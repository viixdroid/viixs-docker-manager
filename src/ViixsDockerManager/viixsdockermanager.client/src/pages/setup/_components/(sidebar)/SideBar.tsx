import type { FC } from 'react'
import type { SetupStepConfiguration } from '../../_models/setup'
import { Box, Grid, styled } from '@mui/material'
import ThemeSwitcherButton from '../../../../components/themes/ThemeSwitcherButton'
import SetupProgress from './SetupProgress'
import SideBarFooter from './SideBarFooter'
import SideBarTitle from './SideBarTitle'

const SetupSideBar = styled(Grid)(({ theme }) => ({
  'background': `linear-gradient(135deg, #0f172a 0%, #1e293b 50%, #0e7490 100%)`,
  'color': 'white',
  'display': 'flex',
  'flexDirection': 'column',
  'position': 'relative',
  'overflow': 'hidden',
  [theme.breakpoints.down('md')]: {
    position: 'sticky',
    top: 0,
    zIndex: 1000,
    minHeight: 'auto',
    padding: theme.spacing(3, 2),
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  [theme.breakpoints.up('md')]: {
    minHeight: '100vh',
    padding: theme.spacing(8),
  },
  '&::before': {
    content: '""',
    position: 'absolute',
    top: '-50%',
    right: '-50%',
    width: '100%',
    height: '100%',
    background: 'radial-gradient(circle, rgba(255,255,255,0.1) 0%, transparent 70%)',
    borderRadius: '50%',
  },
}))

const ThemeToggleButton = styled(Box)(({ theme }) => ({
  'position': 'absolute',
  'zIndex': 2,
  'color': 'white',
  '&:hover': {
    backgroundColor: 'rgba(255, 255, 255, 0.1)',
  },
  [theme.breakpoints.down('md')]: {
    top: 12,
    right: 12,
  },
  [theme.breakpoints.up('md')]: {
    top: 32,
    right: 32,
  },
}))

interface SideBarProps {
  title: string
  mobileTitle: string
  footerContent: string | React.ReactNode
  currentStep: number
  setupSteps: SetupStepConfiguration[]
}

const SideBar: FC<SideBarProps> = ({ currentStep, footerContent, mobileTitle, setupSteps, title }) => {
  return (
    <SetupSideBar size={{ xs: 12, md: 3 }}>
      <ThemeToggleButton>
        <ThemeSwitcherButton />
      </ThemeToggleButton>

      <SideBarTitle title={title} mobileTitle={mobileTitle} />

      <SetupProgress currentStep={currentStep} setupSteps={setupSteps} />

      <SideBarFooter content={footerContent} />
    </SetupSideBar>
  )
}

export default SideBar
