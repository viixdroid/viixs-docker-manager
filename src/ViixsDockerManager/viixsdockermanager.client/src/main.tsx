import { Routes } from '@generouted/react-router'
import { ThemeProvider } from '@mui/material'
import CssBaseline from '@mui/material/CssBaseline'
import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import nordTheme from './components/themes/nordTheme.tsx'
import './index.css'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ThemeProvider theme={nordTheme}>
      <CssBaseline />
      <Routes />
    </ThemeProvider>
  </StrictMode>,
)
