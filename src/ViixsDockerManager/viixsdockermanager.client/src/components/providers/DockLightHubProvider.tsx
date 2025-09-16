import type { FC, ReactNode } from 'react'
import * as signalr from '@microsoft/signalr'
import { createContext, useContext, useEffect, useState } from 'react'
import { useEnvironment } from './EnvironmentProvider'

interface DockLightHubContextType {
  connection: signalr.HubConnection | null
  connect: (environmentId: string) => Promise<void>
}

const DockLightHubContext = createContext<DockLightHubContextType | undefined>(undefined)

interface DockLightHubProviderProps {
  children: ReactNode
}

const DockLightHubProvider: FC<DockLightHubProviderProps> = ({ children }: DockLightHubProviderProps) => {
  const { environment } = useEnvironment()
  const [connection, setConnection] = useState<signalr.HubConnection | null>(null)

  const connect = async (environmentId: string) => {
    if (connection) {
      void connection.stop()
    }

    const newConnection = new signalr.HubConnectionBuilder()
      .withUrl(`/ws/docklight?environmentId=${environmentId}`)
      .withAutomaticReconnect()
      .build()

    try {
      await newConnection?.start()
    }
    catch (err) {
      console.error(err)
      setTimeout(() => connect(environmentId), 500)
    }

    setConnection(newConnection)
  }

  useEffect(() => {
    if (environment && environment.environmentId) {
      void connect(environment.environmentId)
    }
    else {
      if (connection) {
        void connection.stop()
        setConnection(null)
      }
    }
  }, [environment])

  return (
    <DockLightHubContext.Provider value={{ connection, connect }}>
      {children}
    </DockLightHubContext.Provider>
  )
}

export function useDockLightHub(): DockLightHubContextType {
  const context = useContext(DockLightHubContext)
  if (!context) {
    throw new Error('useDockLightHub must be used within a DockLightHubProvider')
  }
  return context
}

export default DockLightHubProvider
