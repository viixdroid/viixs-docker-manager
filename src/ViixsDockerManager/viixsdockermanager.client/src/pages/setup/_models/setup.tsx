import type { ReactNode } from 'react'
import type { ICommand } from '../../../models/command-model'
import SetupActionService from '../../../services/SetupActionService'
import ConnectToDockLightEnvironmentStep from '../_steps/ConnectToDockLightEnvironment'
import RegisterNewUserStep from '../_steps/RegisterNewAccount'

export interface SetupStep {
  stepId: number // order of the step
  stepName: string
  title: string
  description: string
  component?: ReactNode
}

export const SetupSteps: SetupStep[] = [
  {
    stepId: 0,
    stepName: 'Welcome',
    title: 'Welcome!',
    description: 'In the following steps you will setup Viixs Docker Manager',
  },
  {
    stepId: 1,
    stepName: 'RegisterNewAccount',
    title: 'Create account',
    description: 'Create your user account to get started with Viixs Docker Manager.',
    component: <RegisterNewUserStep />,
  },
  {
    stepId: 2,
    stepName: 'ConnectToDockLightEnvironment',
    title: 'Connect to Docker',
    description: 'Connect to your local Docker to start managing your containers.',
    component: <ConnectToDockLightEnvironmentStep />,
  },
  {
    stepId: 3,
    stepName: 'Finish',
    title: 'Finished!',
    description: 'You are all set! Click finish to complete this setup and start using Viixs Docker Manager.',
  },
]

export class StartSetupCommand implements ICommand {
  connectionId: string
  setupId?: string

  constructor(connectionId: string, setupId?: string | undefined) {
    this.connectionId = connectionId
    this.setupId = setupId
  }

  execute(): Promise<void> {
    return SetupActionService.startSetup(this)
  }
}

export class SetupCommand2 implements ICommand {
  setupId: string
  internalCommand: ICommand

  constructor(setupId: string, internalCommand: ICommand) {
    this.setupId = setupId
    this.internalCommand = internalCommand
  }

  execute(): Promise<void> {
    return SetupActionService.executeStep2(this)
  }
}

export class SetupCommand<TInternalCommand extends ICommand> implements ICommand {
  setupId: string
  internalCommand: TInternalCommand

  constructor(setupId: string, internalCommand: TInternalCommand) {
    this.internalCommand = internalCommand
    this.setupId = setupId
  }

  execute(): Promise<void> {
    return SetupActionService.executeStep(this)
  }
}

export abstract class BaseSetupCommand<TInternalCommand extends ICommand> extends SetupCommand<TInternalCommand> {
  abstract url: string
  abstract executeInteral(command: BaseSetupCommand<TInternalCommand>): Promise<void>

  public execute(): Promise<void> {
    return this.executeInteral(this)
  }
}


