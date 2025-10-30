import type { SetupStepCommand } from './setupHandler'
import SetupActionService from '../../../services/SetupServices'
import { SetupStepHandler } from './setupHandler'

export class CreateUserAccountCommand extends SetupStepHandler<CreateUserAccountCommand> {
  emailAddress: string
  password: string

  constructor(emailAddress: string, password: string) {
    super()
    this.emailAddress = emailAddress
    this.password = password
  }

  protected executeStep(step: SetupStepCommand<CreateUserAccountCommand>): Promise<void> {
    return SetupActionService.createUser(step)
  }
}
