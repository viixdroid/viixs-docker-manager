import type { ContainerDetails, ContainerSummary } from '../pages/environments/[environmentid]/(docklight)/containers/container-models'
import backendApi from '../clients/BackendApiClient'

const Url = (environmentId: string, containerId?: string) => `docklightenvironments/${environmentId}/containers${containerId ? `/${containerId}` : ''}`

const DockLightService = {
  getAllContainers: (environmentId: string): Promise<ContainerSummary[]> => backendApi.get(Url(environmentId)),
  getContainerDetails: (environmentId: string, containerId: string): Promise<ContainerDetails> => backendApi.get(Url(environmentId, containerId)),
}

export default DockLightService
