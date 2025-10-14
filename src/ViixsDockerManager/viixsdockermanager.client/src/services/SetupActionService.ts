import type { ICommand } from '../models/command-model'
import type { CreateUserAccountCommand } from '../pages/setup/_models/createuserAccount'
import type { SetupCommand, SetupCommand2, StartSetupCommand } from '../pages/setup/_models/setup'
import backendApi from '../clients/BackendApiClient'

const Url = (step: string) => `setup/${step}`

const SetupActionService = {
  startSetup: (startSetupCommand: StartSetupCommand): Promise<void> => backendApi.post(Url('start'), { json: startSetupCommand }),
  createUser: (createUserCommand: SetupCommand<CreateUserAccountCommand>): Promise<void> => backendApi.post(Url('createuser'), { json: createUserCommand }),
  executeStep: <TCommand extends ICommand>(setupCommand: SetupCommand<TCommand>): Promise<void> => backendApi.post(Url(setupCommand.), { json: setupCommand }),
  executeStep2: (setupCommand: SetupCommand2): Promise<void> => backendApi.post(Url('updateStep'), { json: setupCommand })
}

export default SetupActionService
