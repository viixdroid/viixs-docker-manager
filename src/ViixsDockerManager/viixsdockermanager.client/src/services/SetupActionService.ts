import type { StartSetupCommand } from '../pages/setup/_models/setup'
import backendApi from '../clients/BackendApiClient'

const Url = (step: string) => `setup/${step}`

const SetupActionService = {
  startSetup: (startSetupCommand: StartSetupCommand): Promise<void> => backendApi.post(Url('start'), { json: startSetupCommand }),
}

export default SetupActionService
