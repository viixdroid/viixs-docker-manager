export interface ContainerPort {
  ip: string
  privatePort: number
  publicPort?: number
  type: string
}

export type ContainerState = 'unknown' | 'created' | 'running' | 'paused' | 'restarting' | 'exited' | 'removing' | 'dead'
export type ContainerHealth = 'unknown' | 'starting' | 'healthy' | 'unhealthy' | 'none'

export interface ContainerSummary {
  id: string
  name: string
  image: string
  ports: ContainerPort[]
  created: Date
  state: ContainerState
}

export interface ContainerStatus {
  health: ContainerHealth
  state: ContainerState
  startedTime: Date
}

export interface ContainerDetails1 {
  id: string
  name: string
  createdTime: Date
  status: ContainerStatus
}
