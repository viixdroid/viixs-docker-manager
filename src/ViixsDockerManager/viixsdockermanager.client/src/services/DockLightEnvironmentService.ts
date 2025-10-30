import type { DockLightEnvironment } from '../models/dock-light-environment'
import type { CreateDockLightEnvironmentCommand } from '../pages/setup/_models/docklightEnvironment'
import backendApi from '../clients/BackendApiClient'

const Url = 'docklightenvironments'

const DockLightEnvironmentService = {
  getAllDockLightEnvironments: async (): Promise<DockLightEnvironment[]> => backendApi.get(Url),
  createDockLightEnvironment: async (createDockLightEnvironmentCommand: CreateDockLightEnvironmentCommand): Promise<void> => backendApi.post(Url, { json: createDockLightEnvironmentCommand }),
}

export default DockLightEnvironmentService
