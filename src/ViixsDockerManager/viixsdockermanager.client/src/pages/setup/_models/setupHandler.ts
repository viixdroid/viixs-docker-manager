import type { ICommand } from '../../../models/command-model'

export interface SetupStepOutletContext<TCommand extends SetupStepHandler<TCommand>> {
  isDisabled: boolean
  onNextStepCallback: (callback: () => TCommand | undefined) => void
}

export abstract class SetupStepHandler<TSelf extends SetupStepHandler<TSelf>> implements ICommand {
  protected abstract executeStep(step: SetupStepCommand<TSelf>): Promise<void>

  executeWithSetup(step: SetupStepCommand<TSelf>): Promise<void> {
    return this.executeStep(step)
  }

  execute(): Promise<void> {
    throw new Error('This step must be executed via a SetupStep wrapper.')
  }
}

export class SetupStepCommand<TInternalCommand extends SetupStepHandler<TInternalCommand>> implements ICommand {
  setupId: string
  internalCommand: TInternalCommand
  currentSetupStepName: string

  constructor(setupId: string, currentSetupStepName: string, internalCommand: TInternalCommand) {
    this.setupId = setupId
    this.internalCommand = internalCommand
    this.currentSetupStepName = currentSetupStepName
  }

  execute(): Promise<void> {
    return this.internalCommand.executeWithSetup(this)
  }
}
