// nordThemeV7.ts
import { createTheme } from '@mui/material/styles'
import '@fontsource-variable/outfit'
import '@fontsource-variable/lexend-deca'
import '@fontsource-variable/noto-sans'

// Nord kleuren
const nordColors = {
  polarNight0: '#2E3440',
  polarNight1: '#3B4252',
  polarNight2: '#434C5E',
  polarNight3: '#4C566A',
  snowStorm0: '#D8DEE9',
  snowStorm1: '#E5E9F0',
  snowStorm2: '#ECEFF4',
  frost0: '#8FBCBB',
  frost1: '#88C0D0',
  frost2: '#81A1C1',
  frost3: '#5E81AC',
  auroraRed: '#BF616A',
  auroraOrange: '#D08770',
  auroraYellow: '#EBCB8B',
  auroraGreen: '#A3BE8C',
  auroraPurple: '#B48EAD',
}

const NordTheme = createTheme({
  palette: {
    mode: 'dark',
    primary: {
      main: nordColors.frost2,
      light: nordColors.frost1,
      dark: nordColors.frost3,
      contrastText: nordColors.snowStorm2,
    },
    secondary: {
      main: nordColors.auroraPurple,
      light: nordColors.auroraYellow,
      dark: nordColors.auroraRed,
      contrastText: nordColors.snowStorm2,
    },
    background: {
      default: nordColors.polarNight0,
      paper: nordColors.polarNight1,
    },
    text: {
      primary: nordColors.snowStorm1,
      secondary: nordColors.snowStorm0,
      disabled: nordColors.polarNight3,
    },
    error: { main: nordColors.auroraRed },
    warning: { main: nordColors.auroraOrange },
    info: { main: nordColors.frost1 },
    success: { main: nordColors.auroraGreen },
  },
  typography: {
    fontFamily: ['Noto Sans Variable', 'Roboto', 'sans-serif'].join(','),
    h1: { color: nordColors.snowStorm2 },
    h2: { color: nordColors.snowStorm2 },
    h3: { color: nordColors.snowStorm2 },
    h4: { color: nordColors.snowStorm2 },
    h5: { color: nordColors.snowStorm2, fontFamily: 'Lexend Deca Variable', fontWeight: 'bold' },
    h6: { color: nordColors.snowStorm2 },
    body1: { color: nordColors.snowStorm1 },
    body2: { color: nordColors.snowStorm0 },
    button: { color: nordColors.snowStorm2 },
    fontWeightBold: '700',
    fontWeightMedium: '500',
    fontWeightLight: '300',
    fontWeightRegular: '400',
  },
  components: {
    MuiCssBaseline: {
      styleOverrides: {
        body: { backgroundColor: nordColors.polarNight0, color: nordColors.snowStorm1 },
      },
    },
    MuiAppBar: {
      styleOverrides: {
        colorPrimary: {
          backgroundColor: nordColors.polarNight1,
          boxShadow: 'none', // verwijdert gradient / overlay
        },
      },
    },
    MuiDrawer: {
      styleOverrides: {
        paper: {
          backgroundColor: nordColors.polarNight1,
          boxShadow: 'none',
          borderRight: `1px solid ${nordColors.polarNight2}`,
          borderTop: 0,
          borderBottom: 0,
          borderLeft: 0,
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        contained: {},
        root: {
          textTransform: 'none',
        },
        colorPrimary: {
          '&.MuiButton-contained': {
            'backgroundColor': nordColors.frost2,
            'color': nordColors.snowStorm2,
            '&:hover': { backgroundColor: nordColors.frost3 },
          },
        },
        colorSecondary: {
          '&.MuiButton-contained': {
            'backgroundColor': nordColors.auroraGreen,
            'color': nordColors.snowStorm2,
            '&:hover': { backgroundColor: nordColors.auroraYellow },
          },
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          backgroundColor: nordColors.polarNight1,
          color: nordColors.snowStorm1,
        },
      },
    },
    MuiTab: {
      styleOverrides: {
        root: {
          textTransform: 'none',
          fontFamily: 'Outfit Variable',
          fontSize: '0.99em',
        },
      },
    },
  },
})

export default NordTheme
