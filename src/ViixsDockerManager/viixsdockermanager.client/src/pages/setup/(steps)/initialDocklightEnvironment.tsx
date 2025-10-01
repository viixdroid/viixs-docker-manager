import type { FC } from 'react'
import type { CreateDockLightEnvironmentCommand, InitialDockLightEnvironment } from '../(models)/initialdocklightenvironment.ts'
import type { ApiObject } from '../../../models/api-object.ts'
import { Alert, Button, Stack, styled, TextField, Typography } from '@mui/material'
import Box from '@mui/material/Box'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'
import DockLightEnvironmentService from '../../../services/DockLightEnvironmentService.ts'

const FormBox = styled(Box)(({ theme }) => ({
  display: 'flex',
  flexDirection: 'column',
  gap: theme.spacing(4),
}))

const StyledAlert = styled(Alert)(({ theme }) => ({
  marginBottom: theme.spacing(3),
}))

const AddInitialDockLightStep: FC = () => {
  const navigate = useNavigate()

  const [initialEnvironment, setInitialEnvironment] = useState<InitialDockLightEnvironment>()
  const [name, setName] = useState<string>()

  const getPossibleDockerProtocols = async () => {
    const response = await fetch('/api/docklightenvironments/setup/protocols')
    const result: ApiObject<InitialDockLightEnvironment> = await response.json() // TODO: Do not assume this is always goes right and such. use service or hooks or smth.
    if (!result.isSuccess) {
      throw new Error(result.errors?.toString())
    }
    if (result.result) {
      setInitialEnvironment(result.result)
    }
  }

  const saveNewEnvironment = async () => {
    const command: CreateDockLightEnvironmentCommand = {
      name,
      apiLocation: initialEnvironment?.protocol.protocolUri,
    }

    try {
      await DockLightEnvironmentService.createDockLightEnvironment(command)
      navigate('/')
    }
    catch (err) {
      console.error(err)
    }
  }
  useEffect(() => {
    void getPossibleDockerProtocols()
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

          <TextField
            label="Docker API Endpoint"
            id="connection"
            fullWidth
            defaultValue={initialEnvironment?.protocol.protocolUri ?? ''}
            slotProps={{
              input: {
                readOnly: true,
              },
            }}
          />

        </Stack>
      </FormBox>

    </>
  )
}

export default AddInitialDockLightStep
