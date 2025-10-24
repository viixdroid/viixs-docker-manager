import type { ReactNode } from 'react'
import type { ICommand } from '../../../models/command-model'
import type { SetupStepCommand } from './setupHandler'
import SetupActionService from '../../../services/SetupServices'
import ConnectToDockLightEnvironmentStep from '../_steps/ConnectToDockLightEnvironment'
import CreateNewAccountStep from '../_steps/CreateNewAccountStep'
import { SetupStepHandler } from './setupHandler'

export type SetupStepNameStrings = 'Welcome' | 'CreateNewAccount' | 'ConnectToDockLightEnvironment' | 'Finish'

export interface SetupStepConfiguration {
  stepOrder: number // order of the step
  stepName: SetupStepNameStrings
  title: string
  description: string
  component?: ReactNode
}

export interface SetupStepName {
  stepName: SetupStepNameStrings
  order: number
  isFirstStep: boolean
  isLastStep: boolean
}

export interface SetupStep {
  setupId: string
  currentStep: SetupStepName
}

export const SetupSteps: SetupStepConfiguration[] = [
  {
    stepOrder: 0,
    stepName: 'Welcome',
    title: 'Welcome!',
    description: 'In the following steps you will setup Viixs Docker Manager',
  },
  {
    stepOrder: 1,
    stepName: 'CreateNewAccount',
    title: 'Create account',
    description: 'Create your user account to get started with Viixs Docker Manager.',
    component: <CreateNewAccountStep />,
  },
  {
    stepOrder: 2,
    stepName: 'ConnectToDockLightEnvironment',
    title: 'Connect to Docker',
    description: 'Connect to your local Docker to start managing your containers.',
    component: <ConnectToDockLightEnvironmentStep />,
  },
  {
    stepOrder: 3,
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

export class SetupStartedCommand extends SetupStepHandler<SetupStartedCommand> {
  setupId: string

  constructor(setupId: string) {
    super()
    this.setupId = setupId
  }

  protected executeStep(step: SetupStepCommand<SetupStartedCommand>): Promise<void> {
    return SetupActionService.setupStarted(step)
  }
}

export class FinishSetupCommand implements ICommand {
  setupId: string

  constructor(setupId: string) {
    this.setupId = setupId
  }

  execute(): Promise<void> {
    return SetupActionService.finishSetup(this)
  }
}
