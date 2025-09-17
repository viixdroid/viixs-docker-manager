import type { KillContainerCommand, RestartContainerCommand, StartContainerCommand, StopContainerCommand } from '../models/container-action-models'
import backendApi from '../clients/BackendApiClient'

const Url = (environmentId: string, containerId: string) => `docklightenvironments/${environmentId}/containers/${containerId}`

const DockLightActionService = {
  startContainer: (startContainerCommand: StartContainerCommand): Promise<void> => backendApi.post(`${Url(startContainerCommand.environmentId, startContainerCommand.containerId)}/start`, { json: startContainerCommand }),
  stopContainer: (stopContainerCommand: StopContainerCommand): Promise<void> => backendApi.post(`${Url(stopContainerCommand.environmentId, stopContainerCommand.containerId)}/stop`, { json: stopContainerCommand }),
  restartContainer: (restartContainerCommand: RestartContainerCommand): Promise<void> => backendApi.post(`${Url(restartContainerCommand.environmentId, restartContainerCommand.containerId)}/restart`, { json: restartContainerCommand }),
  killContainer: (killContainerCommand: KillContainerCommand): Promise<void> => backendApi.post(`${Url(killContainerCommand.environmentId, killContainerCommand.containerId)}/kill`, { json: killContainerCommand }),
}

export default DockLightActionService
