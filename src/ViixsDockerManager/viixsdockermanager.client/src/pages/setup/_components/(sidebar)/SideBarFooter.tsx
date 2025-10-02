import type { FC } from 'react'
import { Box, styled, Typography } from '@mui/material'

const ContentBox = styled(Box)({
  position: 'relative',
  zIndex: 1,
})

const FooterText = styled(Typography)(({ theme }) => ({
  opacity: 0.7,
  marginTop: 'auto',
  [theme.breakpoints.down('md')]: {
    display: 'none',
  },
}))

interface SideBarFooterProps {
  content: string | React.ReactNode
}

const SideBarFooter: FC<SideBarFooterProps> = ({ content }) => {
  return (
    <ContentBox>
      <FooterText variant="body2">{content}</FooterText>
    </ContentBox>
  )
}

export default SideBarFooter
