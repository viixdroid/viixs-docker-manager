import type { PaletteMode } from '@mui/material'
import { ThemeProvider } from '@mui/material/styles'
import React, { createContext, useContext, useEffect, useMemo, useState } from 'react'
import { ThemeStorageKey } from '../../constants/StorageKeys'
import { ModernMonochromeTheme } from './ModernMonochrome'

interface ThemeSwitcherContextType {
  mode: PaletteMode
  switchTheme: () => void
}

const ThemeToggleContext = createContext<ThemeSwitcherContextType | undefined>(undefined)

const ThemeSwitcherProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const getChosenTheme = (): PaletteMode => {
    const chosenTheme = localStorage.getItem(ThemeStorageKey)
    if (chosenTheme) {
      if (chosenTheme === 'light' || chosenTheme === 'dark') {
        return chosenTheme
      }
    }

    const doesUserPreferDark = window.matchMedia('(prefers-color-scheme: dark)').matches
    return doesUserPreferDark ? 'dark' : 'light'
  }

  const [mode, setMode] = useState<PaletteMode>(getChosenTheme)

  useEffect(() => {
    localStorage.setItem(ThemeStorageKey, mode)
  }, [mode])

  const theme = useMemo(() => ModernMonochromeTheme(mode), [mode])

  const switchTheme = () => {
    setMode(previouseThemeVariant => (previouseThemeVariant === 'light' ? 'dark' : 'light'))
  }

  return (
    <ThemeToggleContext.Provider value={{ mode, switchTheme }}>
      <ThemeProvider theme={theme}>{children}</ThemeProvider>
    </ThemeToggleContext.Provider>
  )
}

export function useThemeSwitcher(): ThemeSwitcherContextType {
  const context = useContext(ThemeToggleContext)
  if (!context) {
    throw new Error('useThemeToggle must be used inside ThemeProviderWithToggle')
  }
  return context
}

export default ThemeSwitcherProvider
