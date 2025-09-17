import { createTheme } from '@mui/material'
import '@fontsource-variable/outfit'
import '@fontsource-variable/lexend-deca'
import '@fontsource-variable/noto-sans'

// Catppuccino Macchiato Color Palette
const catppuccinMacchiato = {
  rosewater: '#F4DBD6',
  flamingo: '#EEBEBE',
  pink: '#F0C6CE',
  mauve: '#DDB6F2',
  red: '#ED8796',
  maroon: '#E9A4B1',
  peach: '#F5A97F',
  yellow: '#EED49F',
  green: '#A6DA95',
  teal: '#8BD5CA',
  sky: '#91D7E3',
  sapphire: '#7DC4E4',
  blue: '#8AADF4',
  lavender: '#B7BDF8',
  text: '#CAD3F5',
  subtext1: '#B8C0E0',
  subtext0: '#A5ADCB',
  overlay2: '#939AB7',
  overlay1: '#8087A2',
  overlay0: '#6C7084',
  surface2: '#585D6F',
  surface1: '#454B5E',
  surface0: '#313540',
  base: '#24273A',
  mantle: '#1E2030',
  crust: '#181926',
}

const CatppuccinMacchiatoTheme = createTheme({
  palette: {
    mode: 'dark', // Set the palette mode to dark
    primary: {
      main: catppuccinMacchiato.blue, // A soft blue for primary actions
      light: catppuccinMacchiato.lavender,
      dark: '#738DCF', // A slightly darker blue for hover/active states
      contrastText: catppuccinMacchiato.crust, // Dark contrast for light primary
    },
    secondary: {
      main: catppuccinMacchiato.mauve, // A soothing purple for secondary actions
      light: catppuccinMacchiato.pink,
      dark: '#BD9DCF', // A slightly darker purple
      contrastText: catppuccinMacchiato.crust,
    },
    error: {
      main: catppuccinMacchiato.red, // Classic red for errors
      light: catppuccinMacchiato.flamingo,
      dark: '#C7707D',
      contrastText: catppuccinMacchiato.crust,
    },
    warning: {
      main: catppuccinMacchiato.peach, // Warm orange for warnings
      light: catppuccinMacchiato.yellow,
      dark: '#D09068',
      contrastText: catppuccinMacchiato.crust,
    },
    info: {
      main: catppuccinMacchiato.sapphire, // Light blue for informational messages
      light: catppuccinMacchiato.sky,
      dark: '#66A7C0',
      contrastText: catppuccinMacchiato.crust,
    },
    success: {
      main: catppuccinMacchiato.green, // Soft green for success messages
      light: catppuccinMacchiato.teal,
      dark: '#87B87E',
      contrastText: catppuccinMacchiato.crust,
    },
    background: {
      default: catppuccinMacchiato.base, // Main background color
      paper: catppuccinMacchiato.mantle, // Used for cards, dialogs, etc.
    },
    text: {
      primary: catppuccinMacchiato.text, // Main text color
      secondary: catppuccinMacchiato.subtext1, // Secondary text color
      disabled: catppuccinMacchiato.overlay1, // Disabled text color
    },
    divider: catppuccinMacchiato.surface1, // Color for dividers
  },
  typography: {
    fontFamily: [
      'Noto Sans Variable',
      'Roboto',
      'sans-serif',
    ].join(','),
    h1: { color: catppuccinMacchiato.rosewater },
    h2: { color: catppuccinMacchiato.rosewater },
    h3: { color: catppuccinMacchiato.rosewater },
    h4: { color: catppuccinMacchiato.rosewater },
    h5: { color: catppuccinMacchiato.rosewater, fontFamily: 'Lexend Deca Variable', fontWeight: 'bold' },
    h6: { color: catppuccinMacchiato.rosewater },
    // Ensure that selected tabs don't have excessive font weight
    fontWeightMedium: 500,
  },
  components: {
    MuiAppBar: {
      styleOverrides: {
        root: {
          backgroundColor: catppuccinMacchiato.crust, // Darker app bar
          color: catppuccinMacchiato.text,
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: 'none', // Catppuccino often prefers less aggressive styling
        },
        containedPrimary: {
          'backgroundColor': catppuccinMacchiato.blue,
          '&:hover': {
            backgroundColor: catppuccinMacchiato.lavender,
          },
        },
        containedSecondary: {
          'backgroundColor': catppuccinMacchiato.mauve,
          '&:hover': {
            backgroundColor: catppuccinMacchiato.pink,
          },
        },
      },
    },
    MuiPaper: {
      styleOverrides: {
        root: {
          backgroundColor: catppuccinMacchiato.mantle, // Consistent paper background
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          backgroundColor: catppuccinMacchiato.surface0, // Slightly lighter card background
        },
      },
    },
    MuiTooltip: {
      styleOverrides: {
        tooltip: {
          backgroundColor: catppuccinMacchiato.surface2,
          color: catppuccinMacchiato.text,
        },
      },
    },
    MuiDrawer: {
      styleOverrides: {
        paper: {
          backgroundColor: catppuccinMacchiato.crust, // Drawer background
        },
      },
    },
    MuiTab: {
      styleOverrides: {
        root: {
          'fontFamily': 'Outfit Variable',
          'fontSize': '0.99em',
          'textTransform': 'none', // Prevent screaming uppercase
          'minWidth': 0, // Allow tabs to size more naturally if needed
          'color': catppuccinMacchiato.subtext0, // Default text color for inactive tabs
          'fontWeight': 400, // Regular font weight for inactive tabs
          '&:hover': {
            backgroundColor: catppuccinMacchiato.surface1, // Subtle hover background
            color: catppuccinMacchiato.text, // Text color on hover
          },
          '&.Mui-selected': {
            color: catppuccinMacchiato.blue, // Selected tab text color (primary blue)
            fontWeight: 500, // Slightly bolder for selected, but not 'bold'
          },
          // For focus ring, if needed:
          // '&.Mui-focusVisible': {
          //   backgroundColor: 'rgba(138, 173, 244, 0.1)', // Subtle focus background
          // },
        },
      },
    },
    MuiTabs: {
      styleOverrides: {
        indicator: {
          backgroundColor: catppuccinMacchiato.blue, // Indicator color (primary blue)
          height: 3, // Adjust thickness if desired
        },
      },
    },
  },
})

export default CatppuccinMacchiatoTheme
