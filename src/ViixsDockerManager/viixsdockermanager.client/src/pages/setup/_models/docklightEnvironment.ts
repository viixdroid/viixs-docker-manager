import type { ApiObject } from '../../../models/api-object'
import type { IQuery } from '../../../models/query-model'
import { SetupQueryService } from '../../../services/SetupServices'

export interface DockerProtocol {
  protocolUri: string
}

export interface DockLightEnvironmentConfig {
  environment: string
  isRunningInDocker: boolean
  protocol: DockerProtocol
}

export interface CreateDockLightEnvironmentCommand {
  name: string | undefined
  apiLocation: string | undefined
}

export class GetDockLightEnvironmentConfig implements IQuery<DockLightEnvironmentConfig> {
  async execute(): Promise<DockLightEnvironmentConfig> {
    const result = await SetupQueryService.getPossibleDockerProtocols()
    if (result.isSuccess && result.result) {
      return result.result
    }
    throw new Error(result.errors?.toString())
  }
}
