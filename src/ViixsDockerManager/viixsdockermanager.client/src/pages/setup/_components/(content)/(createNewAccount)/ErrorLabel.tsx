import Box from '@mui/material/Box'
import { useTheme } from '@mui/material/styles'
import React from 'react'

interface ErrorLabelProps {
  errors: string[]
}

const ErrorLabel: React.FC<ErrorLabelProps> = ({ errors }) => {
  const theme = useTheme()

  if (!errors || errors.length === 0)
    return null

  return (
    <Box component="ul" mt={1} sx={{ pl: 2, mb: 0 }}>
      {errors.map((err, idx) => (
        <li key={idx} style={{ color: theme.palette.error.main, fontSize: '0.85rem' }}>
          {err}
        </li>
      ))}
    </Box>
  )
}

export default ErrorLabel
