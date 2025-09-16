import DockLightActionService from '../services/DockLightActionService'

// TODO: Move to actual good place
export interface ICommmand {
  execute: () => Promise<void>
}

export abstract class BaseContainerActionCommand implements ICommmand {
  environmentId: string
  containerId: string
  containerName: string

  constructor(environmentId: string | undefined, containerId: string, containerName: string) {
    if (!environmentId) {
      throw new Error('No environment found or given.')
    }

    this.environmentId = environmentId
    this.containerId = containerId
    this.containerName = containerName
  }

  abstract execute(): Promise<void>
}

export class StartContainerCommand extends BaseContainerActionCommand {
  execute(): Promise<void> {
    return DockLightActionService.startContainer(this)
  }
}
export class StopContainerCommand extends BaseContainerActionCommand {
  execute(): Promise<void> {
    return DockLightActionService.stopContainer(this)
  }
}
export class RestartContainerCommand extends BaseContainerActionCommand {
  execute(): Promise<void> {
    return DockLightActionService.restartContainer(this)
  }
}
export class KillContainerCommand extends BaseContainerActionCommand {
  execute(): Promise<void> {
    return DockLightActionService.killContainer(this)
  }
}
