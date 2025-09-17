import type { PaletteMode, Theme } from '@mui/material/styles'
import { createTheme } from '@mui/material/styles'
import '@fontsource-variable/outfit'
import '@fontsource-variable/lexend-deca'
import '@fontsource-variable/noto-sans'

export function ModernMonochromeTheme(mode: PaletteMode): Theme {
  return createTheme({
    palette: {
      mode,
      primary: {
        main: mode === 'light' ? '#424242' : '#bdbdbd', // Medium-dark gray for light, lighter gray for dark
      },
      secondary: {
        main: mode === 'light' ? '#e91e63' : '#f06292', // Bright fuchsia accent for light, slightly brighter for dark
      },
      error: {
        main: mode === 'light' ? '#d32f2f' : '#ef5350',
      },
      background: {
        default: mode === 'light' ? '#dadadaff' : '#2c2c2c', // White for light, dark charcoal for dark
        paper: mode === 'light' ? '#f5f5f5' : '#212121', // '#2c2c2c', // Very light gray for light, slightly lighter charcoal for dark
      },
      text: {
        primary: mode === 'light' ? '#212121' : '#e0e0e0', // Dark gray for light, light gray for dark
        secondary: mode === 'light' ? '#616161' : '#a0a0a0',
      },
    },
    typography: {
      fontFamily: ['Noto Sans Variable', 'Roboto', 'sans-serif'].join(','),
      h5: {
        color: mode === 'light' ? '#303030' : '#f0f0f0',
        fontWeight: 'bold',
      },
      h4: {
        fontWeight: 600,
        color: mode === 'light' ? '#303030' : '#f0f0f0',
      },
      fontWeightBold: '700',
      fontWeightMedium: '500',
      fontWeightLight: '300',
      fontWeightRegular: '400',
    },
    components: {
      MuiButton: {
        styleOverrides: {
          root: {
            borderRadius: 4, // Sharper corners for a modern look
            textTransform: 'none', // Often preferred in modern designs
          },
          containedSecondary: {
            // Subtle accent shadow
            boxShadow: mode === 'light' ? '0 3px 5px 2px rgba(233, 30, 99, .2)' : '0 3px 5px 2px rgba(240, 98, 146, .2)',
          },
        },
      },
      MuiAppBar: {
        styleOverrides: {
          root: {
            borderRadius: 0, // Add this for the AppBar itself
            // Make sure to adjust margin/padding if needed so it doesn't float oddly
          },
          colorPrimary: {
            backgroundColor: mode === 'light' ? '#303030' : '#121212',
          },
        },
      },
      MuiPaper: {
        styleOverrides: {
          root: {
            borderRadius: 8, // Slightly rounded paper elements
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
}
