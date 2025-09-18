import { Routes } from '@generouted/react-router'
import CssBaseline from '@mui/material/CssBaseline'
import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import ThemeSwitcherProvider from './components/themes/ThemeSwitcherProvider.tsx'
import './index.css'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ThemeSwitcherProvider>
      <CssBaseline />
      <Routes />
    </ThemeSwitcherProvider>
  </StrictMode>,
)
