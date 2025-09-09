import {StrictMode} from 'react'
import {createRoot} from 'react-dom/client'
import {Routes} from '@generouted/react-router'
import './index.css'
import {ThemeProvider} from "@mui/material";
import nordTheme from "./components/themes/nordTheme.tsx";
import CssBaseline from "@mui/material/CssBaseline";


createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ThemeProvider theme={nordTheme}>
      <CssBaseline />
      <Routes/>
    </ThemeProvider>
  </StrictMode>
);
