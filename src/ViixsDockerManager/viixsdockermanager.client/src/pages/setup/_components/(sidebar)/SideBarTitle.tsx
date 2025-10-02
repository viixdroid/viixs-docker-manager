import type { FC } from 'react'
import { Box, styled, Typography } from '@mui/material'

const ContentBox = styled(Box)({
  position: 'relative',
  zIndex: 1,
})

const StyledSetupTitle = styled(Typography)(({ theme }) => ({
  marginTop: theme.spacing(2),
  fontWeight: 700,
  lineHeight: 1.2,
  color: 'white',
  [theme.breakpoints.down('md')]: {
    fontSize: '1.25rem',
    marginTop: 0,
  },
}))

interface SideBarTitleProps {
  title: string
  mobileTitle: string
}

const SideBarTitle: FC<SideBarTitleProps> = ({ title, mobileTitle }) => {
  return (
    <>
      <ContentBox sx={{ display: { xs: 'none', md: 'block' } }}>
        <StyledSetupTitle variant="h3" as="h1">
          {title}
        </StyledSetupTitle>
      </ContentBox>

      <ContentBox sx={{ display: { xs: 'block', md: 'none' } }}>
        <StyledSetupTitle variant="h6" as="h1">
          {mobileTitle}
        </StyledSetupTitle>
      </ContentBox>
    </>
  )
}

export default SideBarTitle
