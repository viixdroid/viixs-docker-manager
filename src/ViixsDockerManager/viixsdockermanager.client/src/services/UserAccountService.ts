import type { CreateUserAccountCommand } from '../pages/setup/_models/createuserAccount'
import backendApi from '../clients/BackendApiClient'

const Url = 'users'

const userAccountService = {
  registerNewUser: async (createUserAccountCommand: CreateUserAccountCommand): Promise<void> => backendApi.post(`${Url}/register`, { json: createUserAccountCommand }),
}

export default userAccountService
