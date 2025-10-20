import type { SetupStepNameStrings } from '../_models/setup'
import type { SetupStepHandler } from '../_models/setupHandler'
import { SetupStep } from '../_models/setupHandler'

export class SetupStepFactory {
  static createSetupCommand<T extends SetupStepHandler<T>>(setupId: string, currentSetupStepName: SetupStepNameStrings | string, handler: T): SetupStep<T> {
    return new SetupStep(setupId, currentSetupStepName, handler)
  }
}
