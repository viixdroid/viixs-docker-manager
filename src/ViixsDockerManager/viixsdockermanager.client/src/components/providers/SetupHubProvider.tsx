import type { FC, ReactNode } from 'react'
import * as signalr from '@microsoft/signalr'
import { createContext, useContext, useState } from 'react'

interface SetupHubContextType {
  connection: signalr.HubConnection | null
  connect: () => Promise<void>
}

const SetupContext = createContext<SetupHubContextType | undefined>(undefined)

interface SetupProviderProps {
  children: ReactNode
}

const SetupHubProvider: FC<SetupProviderProps> = ({ children }: SetupProviderProps) => {
  const [connection, setConnection] = useState<signalr.HubConnection | null>(null)

  const connect = async () => {
    if (connection) {
      void connection.stop()
    }

    const newConnection = new signalr.HubConnectionBuilder()
      .withUrl('/ws/setup')
      .withAutomaticReconnect()
      .build()

    try {
      await newConnection?.start()
    }
    catch (err) {
      console.error(err)
      setTimeout(() => connect(), 1000)
    }

    setConnection(newConnection)
  }

  return (
    <SetupContext.Provider value={{ connection, connect }}>
      {children}
    </SetupContext.Provider>
  )
}

export function useSetupHub(): SetupHubContextType {
  const context = useContext(SetupContext)
  if (!context) {
    throw new Error('useSetupHub must be used withing a SetupHubProvider')
  }
  return context
}

export default SetupHubProvider
