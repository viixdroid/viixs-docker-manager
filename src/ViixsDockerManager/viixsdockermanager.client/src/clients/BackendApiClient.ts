import type { Options } from 'ky'
import type { ApiObject } from '../models/api-object'
import ky, { HTTPError } from 'ky'

const methods = ['get', 'post', 'put', 'delete', 'patch', 'head'] as const

type HttpMethod = typeof methods[number]

const backendApiClient = ky.create({
  prefixUrl: '/api',
  headers: {
    'Content-Type': 'application/json',
  },
  hooks: {
    beforeRequest: [
      (request) => {
        const token = localStorage.getItem('token') // TODO: centralize
        if (token) {
          request.headers.set('Authorization', `Bearer ${token}`)
        }
      },
    ],
  },
})

const backendApi: Record<HttpMethod, <T>(url: string, options?: Options) => Promise<T>> = new Proxy({} as any, {
  get(_, prop: string) {
    if (!methods.includes(prop as HttpMethod)) {
      throw new Error(`Unknown method: ${prop}`)
    }
    return async <TResult>(url: string, options?: Options): Promise<TResult | undefined> => {
      try {
        const response: ApiObject<TResult> = await (backendApiClient as any)[prop](url, options).json()
        if (response.isSuccess) {
          return response.result
        }

        if (response.errors) {
          throw new Error(response.errors.join(','))
        }
      }
      catch (httpError) {
        if (httpError instanceof HTTPError) {
          try {
            const errorRes: ApiObject<TResult> = await httpError.response.json()
            if (errorRes.errors) {
              throw new Error(errorRes.errors.join(','))
            }
          }
          catch {
            throw new Error(httpError.message)
          }
        }
        throw httpError // throw error automatically
      }
    }
  },
})

export default backendApi
