import type { FC } from 'react'
import type { SetupStepConfiguration } from '../../_models/setup'
import { Box, styled, Typography } from '@mui/material'

const ContentContainer = styled(Box)(() => ({
  flex: 1,
  display: 'flex',
  flexDirection: 'column',
  justifyContent: 'space-between',
}))

const ContentWrapper = styled(Box)({
  display: 'flex',
  flexDirection: 'column',
  justifyContent: 'center',
  flex: 1,
})

const TitleSection = styled(Box)(({ theme }) => ({
  marginBottom: theme.spacing(4),
}))

const Title = styled(Box)(({ theme }) => ({
  display: 'flex',
  alignItems: 'center',
  gap: theme.spacing(2),
  marginBottom: theme.spacing(2),
}))

const TitleContent = styled(Typography)({
  fontWeight: 600,
})

interface SetupStepProps {
  step: SetupStepConfiguration
}

const SetupStepContent: FC<SetupStepProps> = ({ step }) => {
  return (
    <ContentContainer>
      <ContentWrapper>
        <TitleSection>
          <Title>
            <TitleContent variant="h4">
              {step.title}
            </TitleContent>
          </Title>
          <Typography variant="body1" color="text.primary" style={{ whiteSpace: 'pre-line' }}>
            {step.description}
          </Typography>
        </TitleSection>
        {step.component === undefined ? null : step.component}
      </ContentWrapper>
    </ContentContainer>
  )
}

export default SetupStepContent
