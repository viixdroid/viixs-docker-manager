import type { FC } from 'react'
import type { BaseContainerActionCommand } from '../../../../../models/container-action-models.ts'
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
import { KillContainerCommand, RestartContainerCommand, StartContainerCommand, StopContainerCommand } from '../../../../../models/container-action-models.ts'
import DockLightService from '../../../../../services/DockLightServices.ts'

const Index: FC = () => {
  const { environment } = useEnvironment()

  const [containers, setContainers] = useState<ContainerSummary[]>()

  const [disabledItemIds, setDisabledItemIds] = useState(new Set())

  const getContainerData = async () => {
    try {
      if (environment?.environmentId) {
        const response = await DockLightService.getAllContainers(environment?.environmentId)
        setContainers(response)
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

  const performAction = async (actionCommand: BaseContainerActionCommand) => {
    disableRow(actionCommand.containerId)
    await actionCommand.execute()
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
                        <IconButton color="success" onClick={_ => performAction(new StartContainerCommand(environment?.environmentId, container.id, container.name))} disabled={disabledItemIds.has(container.id)}>
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
                        <IconButton color="warning" onClick={_ => performAction(new RestartContainerCommand(environment?.environmentId, container.id, container.name))} disabled={disabledItemIds.has(container.id)}>
                          <RestartAlt />
                        </IconButton>
                      )}
                      title={`Restart container ${container.name}`}
                    />
                  )}
                <Tooltip
                  children={(
                    <IconButton color="secondary" onClick={_ => performAction(new StopContainerCommand(environment?.environmentId, container.id, container.name))}disabled={disabledItemIds.has(container.id)}>
                      <Stop />
                    </IconButton>
                  )}
                  title={`Start container ${container.name}`}
                />
                <Tooltip
                  children={(
                    <IconButton color="error" onClick={_ => performAction(new KillContainerCommand(environment?.environmentId, container.id, container.name))} disabled={disabledItemIds.has(container.id)}>
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
        afterToastShown={containerid => enableRow(containerid)}
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
