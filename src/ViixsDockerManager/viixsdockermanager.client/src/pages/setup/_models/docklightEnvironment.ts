import type { ApiObject } from '../../../models/api-object'
import type { IQuery } from '../../../models/query-model'
import type { SetupStepCommand } from './setupHandler'
import SetupActionService, { SetupQueryService } from '../../../services/SetupServices'
import { SetupStepHandler } from './setupHandler'

export interface DockerProtocol {
  protocolUri: string
}

export interface DockLightEnvironmentConfig {
  environment: string
  isRunningInDocker: boolean
  protocol: DockerProtocol
}

export interface CreateDockLightEnvironmentCommand1 {
  name: string | undefined
  apiLocation: string | undefined
}

export class GetDockLightEnvironmentConfig implements IQuery<DockLightEnvironmentConfig> {
  async execute(): Promise<DockLightEnvironmentConfig> {
    const result = await SetupQueryService.getPossibleDockerProtocols()
    return result
  }
}

export class CreateDockLightEnvironmentCommand extends SetupStepHandler<CreateDockLightEnvironmentCommand> {
  name: string
  apiLocation: string

  constructor(name: string, apiLocation: string) {
    super()
    this.name = name
    this.apiLocation = apiLocation
  }

  protected executeStep(step: SetupStepCommand<CreateDockLightEnvironmentCommand>): Promise<void> {
    return SetupActionService.createDockLightEnvironment(step)
  }
}
