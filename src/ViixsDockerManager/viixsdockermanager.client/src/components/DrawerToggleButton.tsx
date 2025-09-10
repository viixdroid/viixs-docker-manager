import KeyboardDoubleArrowLeftIcon from '@mui/icons-material/KeyboardDoubleArrowLeft'
import KeyboardDoubleArrowRightIcon from '@mui/icons-material/KeyboardDoubleArrowRight'
import IconButton from '@mui/material/IconButton'
import { useTheme } from '@mui/material/styles'
import React from 'react'

// Define the props our component will accept
interface DrawerToggleButtonProps {
  open: boolean
  handleToggle: () => void
  drawerWidth: number
}

const DrawerToggleButton: React.FC<DrawerToggleButtonProps> = ({ open, handleToggle, drawerWidth }) => {
  const theme = useTheme()

  return (
    <IconButton
      aria-label="toggle drawer"
      onClick={handleToggle}
      sx={{
        'position': 'absolute',
        'bottom': '16px',
        // Use the props to dynamically set the left position
        'left': open
          ? drawerWidth
          : {
              xs: `calc(${theme.spacing(7)} + 1px)`,
              sm: `calc(${theme.spacing(8)} + 1px)`,
            },
        'transform': 'translateX(-50%)',
        'transition': theme.transitions.create('left', {
          easing: theme.transitions.easing.sharp,
          duration: theme.transitions.duration.leavingScreen,
        }),
        'zIndex': theme => theme.zIndex.drawer + 1,
        'backgroundColor': 'background.paper',
        'border': '1px solid',
        'borderColor': 'divider',
        '&:hover': {
          backgroundColor: 'background.default',
        },
      }}
    >
      {/* Use the `open` prop to decide which icon to show */}
      {open ? <KeyboardDoubleArrowLeftIcon /> : <KeyboardDoubleArrowRightIcon />}
    </IconButton>
  )
}

export default DrawerToggleButton
