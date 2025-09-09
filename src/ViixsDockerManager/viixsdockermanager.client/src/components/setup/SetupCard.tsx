import {Card, CardContent, Typography} from "@mui/material";
import type {ReactNode} from "react";

interface SetupCardProps {
  title?: string
  children: ReactNode
}

const SetupCard = ({title, children}: SetupCardProps) => {
  return (
    <Card sx={{margin: 'auto', mt: 4, p: 2}}>
      <CardContent>
        {title && (
          <Typography variant="h5" component="div" sx={{mb: 2, textAlign: 'center'}}>
            {title}
          </Typography>
        )}
        {children}
      </CardContent>
    </Card>
  )
}

export default SetupCard;