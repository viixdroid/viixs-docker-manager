import type { FC } from 'react'
import { DarkModeOutlined, LightModeOutlined } from '@mui/icons-material'
import { IconButton, Tooltip } from '@mui/material'
import { ThemeColors } from './ThemeColors'
import { useThemeSwitcher } from './ThemeSwitcherProvider'

const ThemeSwitcherButton: FC = () => {
  const { mode, switchTheme } = useThemeSwitcher()
  const isLight = mode === 'light'

  return (
    <Tooltip title={`Switch to ${isLight ? 'dark' : 'light'} mode`}>
      <IconButton onClick={switchTheme}>
        {mode === 'light'
          && <LightModeOutlined sx={{ color: ThemeColors.snowGray }} />}
        {mode === 'dark'
          && <DarkModeOutlined />}
      </IconButton>
    </Tooltip>
  )
}

export default ThemeSwitcherButton
