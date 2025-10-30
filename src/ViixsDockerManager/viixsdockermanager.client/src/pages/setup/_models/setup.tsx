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
    description: 'In the next few steps, we will walk you through the essential initial configuration. \r\n\r\nThis includes creating your new user account and establishing a secure connection to your local Docker instance.',
  },
  {
    stepOrder: 1,
    stepName: 'CreateNewAccount',
    title: 'Create your account',
    description: 'To get started with Viixs Docker Manager, please create your initial administrator account.',
    component: <CreateNewAccountStep />,
  },
  {
    stepOrder: 2,
    stepName: 'ConnectToDockLightEnvironment',
    title: 'Connect to Docker',
    description: 'Initially we will connect to your local docker environment. Later you can add more environments if needed',
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
