import type { FC } from 'react'
import type { CreateDockLightEnvironmentCommand, InitialDockLightEnvironment } from '../(models)/initialdocklightenvironment.ts'
import type { ApiObject } from '../../../models/api-object.ts'
import { Button, Stack, TextField, Typography } from '@mui/material'
import Box from '@mui/material/Box'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'

const AddInitialDockLightStep: FC = () => {
  const navigate = useNavigate()

  const [initialEnvironment, setInitialEnvironment] = useState<InitialDockLightEnvironment>()
  const [name, setName] = useState<string>()

  const getPossibleDockerProtocols = async () => {
    const response = await fetch('/api/docklightenvironments/setup/protocols')
    const result: ApiObject<InitialDockLightEnvironment> = await response.json() // TODO: Do not assume this is always goes right and such. use service or hooks or smth.
    if (!result.isSuccess) {
      throw new Error(result.errors.toString())
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

    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(command),
    }

    const response = await fetch('/api/docklightenvironments', requestOptions)
    if (response.ok) {
      navigate('/')
    }
  }
  useEffect(() => {
    void getPossibleDockerProtocols()
  }, [])

  return (
    <>
      <Stack spacing={1}>
        <Typography variant="body1">
          To setup Viix's Docker Manager, we need to create an environment.
          <br />
          This environment will communicate with your local docker engine.
          <br />
          Based upon your OS, the protocol is predefined.
          <br />
          You are running on
          {' '}

          <Box component="span" sx={{ fontWeight: 'bold' }}>
            {initialEnvironment?.environment}
          </Box>
          {initialEnvironment?.isRunningInDocker ? ' in docker.' : '.'}
        </Typography>

        <Stack spacing={1} direction="column" alignItems="start">
          <Typography variant="body1" component="div" sx={{ textAlign: 'start' }}>
            <Box component="span" sx={{ fontStyle: 'oblique' }}>
              Environment name
            </Box>
          </Typography>

          <TextField
            id="outlined-basic"
            fullWidth
            placeholder="The name of the environment"
            variant="outlined"
            value={name}
            onChange={e => setName(e.target.value)}
          />
        </Stack>

        <Stack
          spacing={1}
          direction="column"
          alignItems="start"
        >
          <Typography
            variant="body1"
            component="div"
            sx={{ textAlign: 'start' }}
          >
            <Box
              component="span"
              sx={{ fontStyle: 'oblique' }}
            >
              Protocol
            </Box>
          </Typography>

          <TextField
            id="connection"
            fullWidth
            defaultValue={initialEnvironment?.protocol.protocolUri}
            slotProps={{
              input: {
                readOnly: true,
              },
            }}
          />
        </Stack>

        <Button
          variant="contained"
          onClick={saveNewEnvironment}
        >
          Temporary save button
        </Button>
      </Stack>

    </>
  )
}

export default AddInitialDockLightStep
