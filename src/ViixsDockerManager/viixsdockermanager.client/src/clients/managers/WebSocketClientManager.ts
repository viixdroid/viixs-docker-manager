import type { WebSocketClient } from '../WebSocketClient'
import { createWebSocketClient } from '../WebSocketClient'

export class WebSocketClientManager {
  private clients = new Map<string, WebSocketClient>()

  getClient(endpoint: string): WebSocketClient {
    if (!this.clients.has(endpoint)) {
      const client = createWebSocketClient(endpoint)
      this.clients.set(endpoint, client)
    }
    return this.clients.get(endpoint)!
  }

  async startAll() {
    await Promise.all([...this.clients.values()].map(c => c.start()))
  }

  async stopAll() {
    await Promise.all([...this.clients.values()].map(c => c.stop()))
  }
}
