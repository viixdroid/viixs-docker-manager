export interface DockerProtocol {
  protocolUri: string
}

export interface InitialDockLightEnvironment {
  environment: string
  isRunningInDocker: boolean
  protocol: DockerProtocol
}

export interface CreateDockLightEnvironmentCommand {
  name: string | undefined
  apiLocation: string | undefined
}