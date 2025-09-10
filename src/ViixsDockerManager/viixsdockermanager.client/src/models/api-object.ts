export interface ApiObject<TApiObject> {
  isSuccess: boolean
  errors: string[]
  result?: TApiObject
}
