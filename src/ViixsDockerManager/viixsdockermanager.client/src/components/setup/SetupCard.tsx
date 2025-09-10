import type { FC, ReactNode } from 'react'
import { Card, CardContent, Typography } from '@mui/material'

interface SetupCardProps {
  title?: string
  children: ReactNode
}

const SetupCard: FC<SetupCardProps> = ({ title, children }: SetupCardProps) => {
  return (
    <Card sx={{ margin: 'auto', mt: 4, p: 2 }}>
      <CardContent>
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
      </CardContent>
    </Card>
  )
}

export default SetupCard
