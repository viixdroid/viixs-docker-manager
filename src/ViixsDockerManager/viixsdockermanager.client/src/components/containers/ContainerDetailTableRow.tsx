import type { FC, ReactNode } from 'react'
import { TableCell, TableRow } from '@mui/material'

interface ContainerDetailTableRowProps {
  title: string
  content: string | ReactNode
}

const ContainerDetailTableRow: FC<ContainerDetailTableRowProps> = ({ title, content }: ContainerDetailTableRowProps) => {
  return (
    <TableRow>
      <TableCell component="th" scope="row" sx={{ width: '7%', fontWeight: 'bold' }} variant="body">
        {title}
      </TableCell>
      <TableCell>{content}</TableCell>
    </TableRow>
  )
}

export default ContainerDetailTableRow
