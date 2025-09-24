import type { ICommand } from '../../../models/command-model'
import userAccountService from '../../../services/UserAccountService'

export class CreateUserAccountCommand implements ICommand {
  emailAddress: string
  password: string

  constructor(emailAddress: string, password: string) {
    this.emailAddress = emailAddress
    this.password = password
  }

  execute(): Promise<void> {
    return userAccountService.registerNewUser(this)
  }
}
