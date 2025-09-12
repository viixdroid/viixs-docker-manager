import { Routes } from '@generouted/react-router'
import { ThemeProvider } from '@mui/material'
import CssBaseline from '@mui/material/CssBaseline'
import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import NordTheme from './components/themes/NordTheme.tsx'
import './index.css'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ThemeProvider theme={NordTheme}>
      <CssBaseline />
      <Routes />
    </ThemeProvider>
  </StrictMode>,
)
