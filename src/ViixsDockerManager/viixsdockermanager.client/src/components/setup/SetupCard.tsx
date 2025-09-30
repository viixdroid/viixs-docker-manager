import type { FC, ReactNode } from 'react'
import { Card, CardContent, Typography } from '@mui/material'

interface SetupCardProps {
  title?: string
  children: ReactNode
}

const SetupCard: FC<SetupCardProps> = ({ title, children }: SetupCardProps) => {
  return (
    <>
      {title && (
        <Typography
          variant="h5"
          component="div"
          sx={{ mb: 2, textAlign: 'center' }}
        >
          {title}
        </Typography>
      )}

      {children}

    </>
  )
}

export default SetupCard
