import {StrictMode} from 'react'
import {createRoot} from 'react-dom/client'
import {Routes} from '@generouted/react-router'
import './index.css'
import {createTheme, ThemeProvider} from "@mui/material";

const darkTheme = createTheme({
  colorSchemes: {
    dark: true
  }
})

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ThemeProvider theme={darkTheme}>
      <Routes/>
    </ThemeProvider>
  </StrictMode>
);
