import type { FC } from 'react'
import type { ApiObject } from '../../../../../models/api-object.ts'
import type { ContainerSummary } from './container-models.ts'
import { DeleteForeverOutlined, PlayArrow, RestartAlt, RestartAltOutlined, Stop } from '@mui/icons-material'
import { Box, Button, Chip, Grid, IconButton, List, ListItem, ListItemText, Stack, Tooltip, Typography } from '@mui/material'
import ButtonGroup from '@mui/material/ButtonGroup'
import ListItemButton from '@mui/material/ListItemButton'
import { useEffect, useState } from 'react'
import { Link, useLocation, useParams } from 'react-router'
import ContainerActionResultToast from '../../../../../components/containers/ContainerActionResultToast.tsx'
import DateTimeAgo from '../../../../../components/DateTimeAgo.tsx'

const Index: FC = () => {
  const { environmentid } = useParams()
  const { state } = useLocation()

  const [containers, setContainers] = useState<ContainerSummary[]>()
  const [isError, setIsError] = useState<boolean>(false)
  const [isLoading, setIsLoading] = useState<boolean>(false)

  const getContainerData = async () => {
    try {
      setIsLoading(true)
      const response = await fetch(`/api/docklightenvironments/${environmentid}/containers`)

      if (!response.ok) {
        setContainers([])
        setIsLoading(false)
        return
      }
      // console.log(await response.text())
      const data: ApiObject<ContainerSummary[]> = await response.json()

      if (!data.isSuccess) {
        setIsError(true)
        setIsLoading(false)
        return
      }
      if (data.result) {
        // console.log(data.result)
        setContainers(data.result)
        setIsError(false)
      }
      setIsLoading(false)
    }
    catch (error) {
      console.error(error)
      setIsError(true)
      setIsLoading(false)
    }
  }

  const startContainer = async (containerSummary: ContainerSummary) => {
    const containerId = containerSummary.id
    const startContainerCommand = {
      environmentId: environmentid,
      containerId,
      containerName: containerSummary.name,
    }

    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(startContainerCommand),
    }

    await fetch(`/api/docklightenvironments/${environmentid}/containers/${containerId}/start`, requestOptions)
  }

  useEffect(() => {
    void getContainerData()
  }, [])

  const containerContent
    = (
      <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
        {containers?.map(container => (
          <ListItem
            disablePadding
            alignItems="flex-start"
            key={container.id}
            secondaryAction={(
              <>
                {container.state !== 'running'
                  && (
                    <Tooltip
                      children={(
                        <IconButton color="success" onClick={_ => startContainer(container)}>
                          <PlayArrow />
                        </IconButton>
                      )}
                      title={`Start container ${container.name}`}
                    />
                  )}
                {container.state === 'running'
                  && (
                    <Tooltip
                      children={(
                        <IconButton color="warning">
                          <RestartAlt />
                        </IconButton>
                      )}
                      title={`Start container ${container.name}`}
                    />
                  )}
                <Tooltip
                  children={(
                    <IconButton color="secondary">
                      <Stop />
                    </IconButton>
                  )}
                  title={`Start container ${container.name}`}
                />
                <Tooltip
                  children={(
                    <IconButton color="error">
                      <DeleteForeverOutlined />
                    </IconButton>
                  )}
                  title={`Start container ${container.name}`}
                />
              </>
            )}
          >
            <ListItemButton
              component={Link}
              to={{ pathname: `${container.name}` }}
              state={{ container }}
              divider={true}
            >
              <ListItemText
                primary={container.name}
                slotProps={{ secondary: { component: 'div' } }}
                secondary={(
                  <>
                    <Typography
                      component="span"
                      variant="body2"
                      sx={{ color: 'text.secondary' }}
                    >
                      <Stack
                        direction="row"
                        spacing={0.5}
                        alignItems="center"
                        sx={{ display: 'inline-flex' }}
                      >
                        <span>created at</span>
                        <DateTimeAgo dateTime={container.created} variant="body2" />
                        <span>with image</span>
                        <span>{container.image}</span>
                      </Stack>
                    </Typography>

                    <Box
                      component="div"
                      sx={{
                        mt: 1,
                        display: 'flex',
                        flexWrap: 'wrap',
                        gap: 0.5,
                      }}
                    >
                      {container.ports.map(port => (
                        <Chip key={`${port.ip}:${port.privatePort}:${port.publicPort}:${port.type}`} label={`${port.privatePort}:${port.publicPort}`} variant="filled" size="small" />
                      ))}
                    </Box>
                  </>
                )}
              />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
    )

  return (
    <>
      <ContainerActionResultToast
        handleOnContainerStarted={getContainerData}
      />
      <Typography variant="h5">
        Environment:
        {' '}
        {`${state.environment.name}`}
      </Typography>

      <Grid
        container
        justifyContent="space-between"
      >
        <Button onClick={getContainerData}>Refresh</Button>

        <ButtonGroup
          variant="outlined"
          aria-label="Basic button group"
        >
          <Button variant="outlined" color="success" startIcon={<PlayArrow />}>
            Start
          </Button>
          <Button variant="outlined" color="warning" startIcon={<RestartAltOutlined />}>
            Restart
          </Button>
          <Button variant="outlined" color="secondary" startIcon={<Stop />}>
            Stop
          </Button>
        </ButtonGroup>
      </Grid>

      {isLoading && !isError && <div><p>We loading data atm... pls wait..</p></div>}
      {!isLoading && isError && <div><p>An error from the backend.</p></div>}
      {containers && containerContent}
    </>
  )
}

export default Index
