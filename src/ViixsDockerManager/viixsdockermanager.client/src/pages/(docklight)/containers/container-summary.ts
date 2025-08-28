export interface ContainerPort{
    ip: string,
    privatePort: number,
    publicPort?: number,
    type: string,
}

export type ContainerStatus = 'unknown' | 'created' | 'running' | 'paused' | 'restarting' | 'exited' | 'removing' | 'dead';
export interface ContainerSummary {
    id: string,
    name: string,
    image: string,
    ports: ContainerPort[],
    created: Date,
    status: ContainerStatus
}

