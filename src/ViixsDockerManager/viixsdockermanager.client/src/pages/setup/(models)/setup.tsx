import type { ReactNode } from 'react'
import AddInitialDockLightStep from '../(steps)/initialDocklightEnvironment'
import RegisterNewUserStep from '../(steps)/registerNewUser'

export interface SetupStep {
  step: number // on which step this step must show.
  title: string
  description: string
  component?: ReactNode
}

export const SetupSteps: SetupStep[] = [
  {
    step: 0,
    title: 'Welcome!',
    description: 'In the following steps you will setup Viixs Docker Manager',
  },
  {
    step: 1,
    title: 'Create account',
    description: 'Create your user account to get started with Viixs Docker Manager.',
    component: <RegisterNewUserStep />,
  },
  {
    step: 2,
    title: 'Connect to Docker',
    description: 'Connect to your local Docker to start managing your containers.',
    component: <AddInitialDockLightStep />,
  },
  {
    step: 3,
    title: 'Finished!',
    description: 'You are all set! Click finish to complete this setup and start using Viixs Docker Manager.',
  },
]
