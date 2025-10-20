import type { ApiObject } from '../models/api-object'
import type { DockLightEnvironmentConfig, GetDockLightEnvironmentConfig } from '../pages/setup/_models/docklightEnvironment'
import type { SetupStartedCommand, StartSetupCommand } from '../pages/setup/_models/setup'
import type { SetupStep } from '../pages/setup/_models/setupHandler'
import type { CreateUserAccountCommand } from '../pages/setup/_models/userAccount'
import backendApi from '../clients/BackendApiClient'

const ActionUrl = (step: string) => `setup/${step}`
const QueryUrl = (query: string) => `setup/${query}`

const SetupActionService = {
  startSetup: (startSetupCommand: StartSetupCommand): Promise<void> => backendApi.post(ActionUrl('start'), { json: startSetupCommand }),
  setupStarted: (setupStartedCommand: SetupStep<SetupStartedCommand>): Promise<void> => backendApi.post(ActionUrl('started'), { json: setupStartedCommand }),
  createUser: (createUserCommand: SetupStep<CreateUserAccountCommand>): Promise<void> => backendApi.post(ActionUrl('createuser'), { json: createUserCommand }),
}

const SetupQueryService = {
  getPossibleDockerProtocols: (): Promise<ApiObject<DockLightEnvironmentConfig>> => backendApi.get(QueryUrl('docklight/configuration')),
}

export default SetupActionService
export { SetupQueryService }
