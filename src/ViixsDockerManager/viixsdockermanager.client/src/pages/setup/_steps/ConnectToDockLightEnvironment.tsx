import type { FC } from 'react'
import type { ApiObject } from '../../../models/api-object.ts'
import type { DockLightEnvironmentConfig } from '../_models/docklightEnvironment.ts'
import type { SetupStepOutletContext } from '../_models/setupHandler.ts'
import { Alert, Button, Stack, styled, TextField, Typography } from '@mui/material'
import Box from '@mui/material/Box'
import { useEffect, useRef, useState } from 'react'
import { useNavigate, useOutletContext } from 'react-router'
import DockLightEnvironmentService from '../../../services/DockLightEnvironmentService.ts'
import { CreateDockLightEnvironmentCommand, GetDockLightEnvironmentConfig } from '../_models/docklightEnvironment.ts'

const FormBox = styled(Box)(({ theme }) => ({
  display: 'flex',
  flexDirection: 'column',
  gap: theme.spacing(4),
}))

const StyledAlert = styled(Alert)(({ theme }) => ({
  marginBottom: theme.spacing(3),
}))

const ConnectToDockLightEnvironmentStep: FC = () => {
  const { onNextStepCallback } = useOutletContext<SetupStepOutletContext<CreateDockLightEnvironmentCommand>>()

  const [initialEnvironment, setInitialEnvironment] = useState<DockLightEnvironmentConfig>()
  const [protocol, setProtocol] = useState<string>('')
  const [name, setName] = useState<string>('')
  const nameRef = useRef(name)
  const protocolRef = useRef(protocol)

  const getPossibleDockerProtocols = async () => {
    const dockerProtocolQuery = new GetDockLightEnvironmentConfig()
    const queryResult = await dockerProtocolQuery.execute()
    setProtocol(queryResult.protocol.protocolUri)
    setInitialEnvironment(queryResult)
  }

  const saveNewEnvironment = (): CreateDockLightEnvironmentCommand => {
    return new CreateDockLightEnvironmentCommand(nameRef.current, protocolRef.current)
  }
  useEffect(() => {
    void getPossibleDockerProtocols()
  }, [])

  useEffect(() => {
    protocolRef.current = protocol
  }, [protocol])

  useEffect(() => {
    nameRef.current = name
  }, [name])

  useEffect(() => {
    // Register a function once, on mount
    onNextStepCallback(() => saveNewEnvironment())

    return () => {
      // Clean up when unmounting
      onNextStepCallback(() => undefined)
    }
  }, [])

  return (
    <>
      <FormBox>
        <Stack spacing={2}>
          <StyledAlert severity="info">
            Viixs Docker Manager is running on
            {' '}

            <Box component="span" sx={{ fontWeight: 'bold' }}>
              {initialEnvironment?.environment}
            </Box>
            {initialEnvironment?.isRunningInDocker ? ' in docker.' : '.'}
            <br />
            <br />
            {'We will connect to Docker using: '}
            <Box component="span" sx={{ fontWeight: 'bold' }}>
              {initialEnvironment?.protocol.protocolUri}
            </Box>
          </StyledAlert>
          <TextField
            fullWidth
            label="Environment Name"
            type="email"
            variant="outlined"
            required
            value={name}
            onChange={e => setName(e.target.value)}
          />
        </Stack>
      </FormBox>

    </>
  )
}

export default ConnectToDockLightEnvironmentStep
