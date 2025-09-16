import type { FC } from 'react'
import type { ApiObject } from '../../../../../models/api-object.ts'
import type { ContainerSummary } from './container-models.ts'
import { DeleteForeverOutlined, PlayArrow, RestartAlt, RestartAltOutlined, Stop } from '@mui/icons-material'
import { Box, Button, Chip, Grid, IconButton, List, ListItem, ListItemText, Stack, Tooltip, Typography } from '@mui/material'
import ButtonGroup from '@mui/material/ButtonGroup'
import ListItemButton from '@mui/material/ListItemButton'
import { useEffect, useState } from 'react'
import { Link } from 'react-router'
import ContainerActionResultToast from '../../../../../components/containers/ContainerActionResultToast.tsx'
import DateTimeAgo from '../../../../../components/DateTimeAgo.tsx'
import { useEnvironment } from '../../../../../components/providers/EnvironmentProvider.tsx'

const Index: FC = () => {
  const { environment } = useEnvironment()

  const [containers, setContainers] = useState<ContainerSummary[]>()

  const [disabledItemIds, setDisabledItemIds] = useState(new Set())

  const getContainerData = async () => {
    try {
      const response = await fetch(`/api/docklightenvironments/${environment?.environmentId}/containers`)

      if (!response.ok) {
        setContainers([])
        return
      }
      // console.log(await response.text())
      const data: ApiObject<ContainerSummary[]> = await response.json()

      if (!data.isSuccess) {
        return
      }
      if (data.result) {
        setContainers(data.result)
      }
    }
    catch (error) {
      console.error(error)
    }
  }

  const enableRow = async (containerId: string) => {
    await getContainerData()
    setDisabledItemIds((previous) => {
      const newDisabledItemsSet = new Set(previous)
      newDisabledItemsSet.delete(containerId)
      return newDisabledItemsSet
    })
  }

  const disableRow = (containerId: string) => {
    setDisabledItemIds(previous => new Set(previous).add(containerId))
  }

  const startContainer = async (containerSummary: ContainerSummary) => {
    const containerId = containerSummary.id
    disableRow(containerId)
    const startContainerCommand = {
      environmentId: environment?.environmentId,
      containerId,
      containerName: containerSummary.name,
    }

    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(startContainerCommand),
    }

    await fetch(`/api/docklightenvironments/${environment?.environmentId}/containers/${containerId}/start`, requestOptions)
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
                        <IconButton color="success" onClick={_ => startContainer(container)} disabled={disabledItemIds.has(container.id)}>
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
                        <IconButton color="warning" disabled={disabledItemIds.has(container.id)}>
                          <RestartAlt />
                        </IconButton>
                      )}
                      title={`Start container ${container.name}`}
                    />
                  )}
                <Tooltip
                  children={(
                    <IconButton color="secondary" disabled={disabledItemIds.has(container.id)}>
                      <Stop />
                    </IconButton>
                  )}
                  title={`Start container ${container.name}`}
                />
                <Tooltip
                  children={(
                    <IconButton color="error" disabled={disabledItemIds.has(container.id)}>
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
              disabled={disabledItemIds.has(container.id)}
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
        handleOnContainerStarted={containerid => enableRow(containerid)}
      />
      <Typography variant="h5">
        Environment:
        {' '}
        {`${environment?.name}`}
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
      {containers && containerContent}
    </>
  )
}

export default Index
