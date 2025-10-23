import type { CreateDockLightEnvironmentCommand, DockLightEnvironmentConfig } from '../pages/setup/_models/docklightEnvironment'
import type { SetupStartedCommand, StartSetupCommand } from '../pages/setup/_models/setup'
import type { SetupStepCommand } from '../pages/setup/_models/setupHandler'
import type { CreateUserAccountCommand } from '../pages/setup/_models/userAccount'
import backendApi from '../clients/BackendApiClient'

const ActionUrl = (step: string) => `setup/${step}`
const QueryUrl = (query: string) => `setup/${query}`

const SetupActionService = {
  startSetup: (startSetupCommand: StartSetupCommand): Promise<void> => backendApi.post(ActionUrl('start'), { json: startSetupCommand }),
  setupStarted: (setupStartedCommand: SetupStepCommand<SetupStartedCommand>): Promise<void> => backendApi.post(ActionUrl('started'), { json: setupStartedCommand }),
  createUser: (createUserCommand: SetupStepCommand<CreateUserAccountCommand>): Promise<void> => backendApi.post(ActionUrl('createuser'), { json: createUserCommand }),
  createDockLightEnvironment: (createDockLightEnvironmentCommand: SetupStepCommand<CreateDockLightEnvironmentCommand>): Promise<void> => backendApi.post(ActionUrl('createdocklightenvironment'), { json: createDockLightEnvironmentCommand }),
}

const SetupQueryService = {
  getPossibleDockerProtocols: (): Promise<DockLightEnvironmentConfig> => backendApi.get(QueryUrl('docklight/configuration')),
}

export default SetupActionService
export { SetupQueryService }
