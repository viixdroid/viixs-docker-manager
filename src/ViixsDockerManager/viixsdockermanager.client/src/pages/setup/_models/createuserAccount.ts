import type { ICommand } from '../../../models/command-model'
import SetupActionService from '../../../services/SetupActionService'
import userAccountService from '../../../services/UserAccountService'
import { BaseSetupCommand } from './setup'

export class CreateUserAccountCommand implements ICommand {
  emailAddress: string
  password: string
  role?: string

  constructor(emailAddress: string, password: string, role: string = '') {
    this.emailAddress = emailAddress
    this.password = password
    this.role = role
  }

  execute(): Promise<void> {
    return SetupActionService.createUser(this)
  }
}
