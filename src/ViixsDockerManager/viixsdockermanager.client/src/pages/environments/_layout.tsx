import type { CSSObject, Theme } from '@mui/material/styles'
import type { FC } from 'react'

// Icon Imports
import AccountCircleIcon from '@mui/icons-material/AccountCircle'
import SettingsIcon from '@mui/icons-material/Settings'
import StorageIcon from '@mui/icons-material/Storage'
import MuiAppBar from '@mui/material/AppBar'
import Box from '@mui/material/Box'
import MuiDrawer from '@mui/material/Drawer'
import List from '@mui/material/List'
import ListItem from '@mui/material/ListItem'
import ListItemButton from '@mui/material/ListItemButton'
import ListItemIcon from '@mui/material/ListItemIcon'
import ListItemText from '@mui/material/ListItemText'
// MUI Imports
import { styled } from '@mui/material/styles'

import Toolbar from '@mui/material/Toolbar'
import Typography from '@mui/material/Typography'
import { useState } from 'react'
import { Outlet, Link as RouterLink, useLocation } from 'react-router'
import DrawerToggleButton from '../../components/DrawerToggleButton'
import DockLightHubProvider from '../../components/providers/DockLightHubProvider'
import EnvironmentProvider from '../../components/providers/EnvironmentProvider'

const drawerWidth = 240

// --- Styled Components for Smooth Transitions ---

// Mixin for the "opened" state styles
function openedMixin(theme: Theme): CSSObject {
  return {
    width: drawerWidth,
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
    overflowX: 'hidden',
  }
}

// Mixin for the "closed" state styles
function closedMixin(theme: Theme): CSSObject {
  return {
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    overflowX: 'hidden',
    width: `calc(${theme.spacing(7)} + 1px)`, // Mini-drawer width
    [theme.breakpoints.up('sm')]: {
      width: `calc(${theme.spacing(8)} + 1px)`,
    },
  }
}

// The custom styled Drawer component
const Drawer = styled(MuiDrawer, { shouldForwardProp: prop => prop !== 'open' })(({ theme, open }) => ({
  width: drawerWidth,
  flexShrink: 0,
  whiteSpace: 'nowrap',
  boxSizing: 'border-box',
  ...(open && {
    ...openedMixin(theme),
    '& .MuiDrawer-paper': openedMixin(theme),
  }),
  ...(!open && {
    ...closedMixin(theme),
    '& .MuiDrawer-paper': closedMixin(theme),
  }),
}))

// --- The Layout Component ---

const DockLightLayout: FC = () => {
  const [open, setOpen] = useState(true) // State to control the drawer
  const { pathname } = useLocation()

  // TODO: pass menuitems
  const menuItems = [
    { text: 'Containers', path: '/containers', icon: <StorageIcon /> },
    { text: 'Profile', path: '/account/profile', icon: <AccountCircleIcon /> },
    { text: 'Settings', path: '/account/settings', icon: <SettingsIcon /> },
  ]

  return (
    <Box sx={{ display: 'flex' }}>
      <MuiAppBar
        position="fixed"
        color="primary"
        elevation={0}
        sx={{ zIndex: theme => theme.zIndex.drawer + 1 }}
      >
        <Toolbar>
          <Box
            component="img"
            sx={{
              height: 32,
              width: 32,
              maxHeight: { xs: 64, md: 64 },
              maxWidth: { xs: 64, md: 64 },
              paddingRight: '10px',
            }}
            alt="Viixs Docker manager"
            src="/favicon.svg"
          />

          <Typography
            variant="h6"
            noWrap
            component="div"
          >
            Viixs Docker Manager
          </Typography>
        </Toolbar>
      </MuiAppBar>

      <Drawer
        variant="permanent"
        open={open}
      >
        <Toolbar />

        <Box sx={{
          display: 'flex',
          flexDirection: 'column',
          height: '100%',
        }}
        >
          <List>
            {menuItems.map(item => (
              <ListItem
                key={item.text}
                disablePadding
                sx={{ display: 'block' }}
              >
                <ListItemButton
                  component={RouterLink}
                  to={item.path}
                  selected={pathname === item.path}
                  sx={{ minHeight: 48, px: 2.5, justifyContent: open ? 'initial' : 'center' }}
                >
                  <ListItemIcon sx={{
                    minWidth: 0,
                    mr: open ? 3 : 'auto',
                    justifyContent: 'center',
                  }}
                  >
                    {item.icon}
                  </ListItemIcon>

                  <ListItemText
                    primary={item.text}
                    sx={{ opacity: open ? 1 : 0 }}
                  />
                </ListItemButton>
              </ListItem>
            ))}
          </List>

          <Box sx={{ flexGrow: 1 }} />

          <List>
            <ListItem>
              {open
                && <ListItemText primary="Version xyz" />}
            </ListItem>
          </List>
        </Box>
      </Drawer>

      <Box
        component="main"
        sx={{ flexGrow: 1, p: 3 }}
      >
        <Toolbar />
        <EnvironmentProvider>
          <DockLightHubProvider>
            <Outlet />
          </DockLightHubProvider>
        </EnvironmentProvider>
      </Box>

      <DrawerToggleButton
        open={open}
        handleToggle={() => setOpen(!open)}
        drawerWidth={drawerWidth}
      />
    </Box>
  )
}

export default DockLightLayout
