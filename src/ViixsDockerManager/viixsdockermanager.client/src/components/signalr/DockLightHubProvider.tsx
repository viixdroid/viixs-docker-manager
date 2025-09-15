import type { FC, ReactNode } from 'react'
import * as signalr from '@microsoft/signalr'
import { createContext, useContext, useState } from 'react'

interface DockLightHubContextType {
  connection: signalr.HubConnection | null
  connect: (environmentId: string) => Promise<void>
}

const DockLightHubContext = createContext<DockLightHubContextType | undefined>(undefined)

interface DockLightHubProviderProps {
  children: ReactNode
}

export const DockLightHubProvider: FC<DockLightHubProviderProps> = ({ children }: DockLightHubProviderProps) => {
  const [connection, setConnection] = useState<signalr.HubConnection | null>(null)

  const connect = async (environmentId: string) => {
    // if (connection) {
    //   void connection.stop()
    // }

    const newConnection = new signalr.HubConnectionBuilder()
      .withUrl(`/ws/docklight?environmentId=${environmentId}`)
      .withAutomaticReconnect()
      .build()

    try {
      await newConnection?.start()
    }
    catch {
      setTimeout(() => connect(environmentId), 500)
    }

    setConnection(newConnection)
  }

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
