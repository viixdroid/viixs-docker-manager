export interface IQuery<TResult> {
  execute: () => Promise<TResult>
}
