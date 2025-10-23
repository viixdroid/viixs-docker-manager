import type { SetupStepNameStrings } from '../_models/setup'
import type { SetupStepHandler } from '../_models/setupHandler'
import { SetupStepCommand } from '../_models/setupHandler'

export class SetupStepFactory {
  static createSetupCommand<T extends SetupStepHandler<T>>(setupId: string, currentSetupStepName: SetupStepNameStrings | string, handler: T): SetupStepCommand<T> {
    return new SetupStepCommand(setupId, currentSetupStepName, handler)
  }
}
