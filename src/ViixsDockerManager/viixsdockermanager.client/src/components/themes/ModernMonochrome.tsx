import type { PaletteMode, Theme } from '@mui/material/styles'
import { createTheme } from '@mui/material/styles'
import { ThemeColors } from './ThemeColors'
import '@fontsource-variable/outfit'
import '@fontsource-variable/lexend-deca'
import '@fontsource-variable/noto-sans'

export function ModernMonochromeTheme(mode: PaletteMode): Theme {
  const isLight = mode === 'light'
  return createTheme({
    palette: {
      mode,
      primary: {
        main: isLight ? ThemeColors.graphiteGray : ThemeColors.silverGray,
      },
      secondary: {
        main: isLight ? ThemeColors.fuchsiaPink : ThemeColors.fuchsiaPink,
      },
      error: {
        main: isLight ? ThemeColors.crimsonRed : ThemeColors.salmonRed,
      },
      background: {
        default: isLight ? ThemeColors.concreteGray : ThemeColors.charcoalBlack,
        paper: isLight ? ThemeColors.cloudGray : ThemeColors.onyxBlack,
      },
      text: {
        primary: isLight ? ThemeColors.inkBlack : ThemeColors.mistGray,
        secondary: isLight ? ThemeColors.slateGray : ThemeColors.ashGray,
      },
    },
    typography: {
      fontFamily: ['Noto Sans Variable', 'Roboto', 'sans-serif'].join(','),
      h5: {
        color: isLight ? ThemeColors.coalGray : ThemeColors.snowGray,
        fontWeight: 'bold',
      },
      h4: {
        fontWeight: 600,
        color: isLight ? ThemeColors.coalGray : ThemeColors.snowGray,
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
            boxShadow: isLight
              ? '0 3px 5px 2px rgba(233, 30, 99, .2)'
              : '0 3px 5px 2px rgba(240, 98, 146, .2)',
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
            backgroundColor: isLight ? ThemeColors.coalGray : ThemeColors.obsidianBlack,
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
