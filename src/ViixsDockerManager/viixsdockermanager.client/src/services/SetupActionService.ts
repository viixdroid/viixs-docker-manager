import type { CreateUserAccountCommand } from '../pages/setup/_models/createuserAccount'
import type { StartSetupCommand } from '../pages/setup/_models/setup'
import type { SetupStep } from '../pages/setup/_models/setupHandler'
import backendApi from '../clients/BackendApiClient'

const Url = (step: string) => `setup/${step}`

const SetupActionService = {
  startSetup: (startSetupCommand: StartSetupCommand): Promise<void> => backendApi.post(Url('start'), { json: startSetupCommand }),
  createUser: (createUserCommand: SetupStep<CreateUserAccountCommand>): Promise<void> => backendApi.post(Url('createuser'), { json: createUserCommand }),
}

export default SetupActionService
