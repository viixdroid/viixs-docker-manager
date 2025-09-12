import type { TypographyVariant } from '@mui/material'
import type { FC } from 'react'
import { Typography } from '@mui/material'
import { formatDistance } from 'date-fns'

interface DateTimeAgoProps {
  dateTime: string | number | Date // | undefined
  variant?: TypographyVariant
}

const DateTimeAgo: FC<DateTimeAgoProps> = ({ dateTime, variant }: DateTimeAgoProps) => {
  return (
    <Typography variant={variant || 'body1'}>{formatDistance(dateTime, new Date(), { addSuffix: true })}</Typography>
  )
}

export default DateTimeAgo
