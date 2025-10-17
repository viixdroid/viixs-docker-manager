import * as signalr from '@microsoft/signalr'
import React, { createContext, useContext, useEffect, useRef, useState } from 'react'

interface WebSocketContextValue {
  connection: signalr.HubConnection | null
}

interface WebSocketProviderProps {
  endpoint: string
  queryParams?: Record<string, string> // Accept query parameters as a prop
  children: React.ReactNode
}
const WebSocketContext = createContext<WebSocketContextValue | undefined>(undefined)

const WebSocketProvider: React.FC<WebSocketProviderProps> = ({ endpoint, queryParams, children }) => {
  const [connection, setConnection] = useState<signalr.HubConnection | null>(null)
  const isMounted = useRef(true)

  // Helper function to construct the URL with query parameters
  const constructUrl = (endpoint: string, queryParams?: Record<string, string>): string => {
    if (!queryParams || Object.keys(queryParams).length === 0) {
      return `/ws/${endpoint}`
    }
    const queryString = new URLSearchParams(queryParams).toString()
    return `/ws/${endpoint}?${queryString}`
  }

  useEffect(() => {
    isMounted.current = true
    if (connection) {
      return
    }

    const url = constructUrl(endpoint, queryParams)
    const newConnection = new signalr.HubConnectionBuilder()
      .withUrl(url)
      .withAutomaticReconnect()
      .configureLogging(signalr.LogLevel.Information)
      .build()

    newConnection
      .start()
      .then(() => {
        if (isMounted.current) {
          console.log(`[WebSocket] Connected to ${endpoint}`)
          setConnection(newConnection)
        }
      })
      .catch((err) => {
        console.error(`[WebSocket] Connection to ${endpoint} failed:`, err)
      })

    return () => {
      isMounted.current = false
      newConnection
        .stop()
        .then(() => {
          console.log(`[WebSocket] Disconnected from ${endpoint}`)
          setConnection(null)
        })
        .catch((err) => {
          console.error(`[WebSocket] Error disconnecting from ${endpoint}:`, err)
        })
    }
  }, [endpoint])

  return <WebSocketContext.Provider value={{ connection }}>{children}</WebSocketContext.Provider>
}

export function useWebSocketContext(): WebSocketContextValue {
  const context = useContext(WebSocketContext)
  if (!context) {
    throw new Error('useWebSocketContext must be used within a WebSocketProvider')
  }
  return context
}

export default WebSocketProvider
