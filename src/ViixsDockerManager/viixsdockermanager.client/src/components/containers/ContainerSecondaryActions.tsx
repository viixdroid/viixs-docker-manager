import type { FC } from 'react'
import type { BaseContainerActionCommand } from '../../models/container-action-models'
import type { ContainerSummary } from '../../pages/environments/[environmentid]/(docklight)/containers/container-models'
import { DeleteForeverOutlined, PlayArrow, RestartAlt, Stop } from '@mui/icons-material'
import { IconButton, Tooltip } from '@mui/material'
import { KillContainerCommand, RestartContainerCommand, StartContainerCommand, StopContainerCommand } from '../../models/container-action-models'

interface ContainerSecondaryActionProps {
  container: ContainerSummary
  environmentId: string | undefined
  isDisabled: boolean
  performActionCommand: (actionCommmand: BaseContainerActionCommand) => Promise<void>
}

const ContainerSecondaryAction: FC<ContainerSecondaryActionProps> = ({
  container,
  environmentId,
  isDisabled,
  performActionCommand,
}: ContainerSecondaryActionProps) => {
  return (
    <>
      {container.state !== 'running'
        && (
          <Tooltip
            children={(
              <IconButton
                color="success"
                onClick={_ => performActionCommand(new StartContainerCommand(environmentId, container.id, container.name))}
                disabled={isDisabled}
              >
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
              <IconButton
                color="warning"
                onClick={_ => performActionCommand(new RestartContainerCommand(environmentId, container.id, container.name))}
                disabled={isDisabled}
              >
                <RestartAlt />
              </IconButton>
            )}
            title={`Restart container ${container.name}`}
          />
        )}
      <Tooltip
        children={(
          <IconButton
            color="secondary"
            onClick={_ => performActionCommand(new StopContainerCommand(environmentId, container.id, container.name))}
            disabled={isDisabled}
          >
            <Stop />
          </IconButton>
        )}
        title={`Stop container ${container.name}`}
      />
      <Tooltip
        children={(
          <IconButton
            color="error"
            onClick={_ => performActionCommand(new KillContainerCommand(environmentId, container.id, container.name))}
            disabled={isDisabled}
          >
            <DeleteForeverOutlined />
          </IconButton>
        )}
        title={`Kill container ${container.name}`}
      />
    </>
  )
}

export default ContainerSecondaryAction
