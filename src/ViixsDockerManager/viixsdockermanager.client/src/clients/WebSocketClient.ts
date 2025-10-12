import * as signalr from '@microsoft/signalr'

export interface WebSocketClient {
  connection: signalr.HubConnection
  start: () => Promise<void>
  stop: () => Promise<void>
  getConnectionId: () => string | null
  isConnected: () => boolean
  on: {
    <T = any>(eventName: string, callback: (data: T) => void): void
    (eventName: string, callback: (...args: any[]) => void): void
  }
  off: (eventName: string, callback?: (...args: any[]) => void) => void
  send: (methodName: string, ...args: any[]) => Promise<void>
  invoke: <T = any>(methodName: string, ...args: any[]) => Promise<T>
}

/**
 * Create a fully-managed SignalR WebSocket client.
 */
export function createWebSocketClient(endpoint: string): WebSocketClient {
  const connection = new signalr.HubConnectionBuilder()
    .withUrl(`/ws/${endpoint}`)
    .withAutomaticReconnect()
    .configureLogging(signalr.LogLevel.Information)
    .build()

  async function start() {
    // if (connection) {
    //   void connection?.stop()
    // }
    if (connection.state !== signalr.HubConnectionState.Disconnected) {
      return
    }

    try {
      await connection?.start()
    }
    catch (err) {
      console.error(`[WebSocket] Failed to connect to ${endpoint}`, err)
    }
  }

  async function stop() {
    try {
      await connection?.stop()
    }
    catch (err) {
      console.error(`[WebSocket] ⚠️ Error disconnecting from ${endpoint}`, err)
    }
  }

  function getConnectionId() {
    return connection?.connectionId
  }

  function isConnected() {
    return connection?.state === signalr.HubConnectionState.Connected
  }

  function on<T = any>(eventName: string, callback: (data: T) => void): void
  function on(eventName: string, callback: (...args: any[]) => void): void
  function on(eventName: string, callback: ((...args: any[]) => void) | ((data: any) => void)) {
    connection.on(eventName, callback as (...args: any[]) => void)
  }

  function off(eventName: string, callback?: (...args: any[]) => void) {
    if (callback) {
      connection?.off(eventName, callback)
      return
    }
    connection.off(eventName)
  }

  async function send(methodName: string, ...args: any[]) {
    try {
      await connection?.send(methodName, ...args)
    }
    catch (err) {
      console.error(`[WebSocket] Send failed (${methodName})`, err)
    }
  }

  async function invoke<T = any>(methodName: string, ...args: any[]): Promise<T> {
    try {
      return await connection?.invoke<T>(methodName, ...args)
    }
    catch (err) {
      console.error(`[WebSocket] Invoke failed (${methodName})`, err)
      throw err
    }
  }

  return { connection, start, stop, getConnectionId, isConnected, on, off, send, invoke }
}
