import type { IQuery } from '../../models/query-model'
import { SetupQueryService } from '../../services/SetupServices'

export interface IsSetupFinished {
  isFinished: boolean
}

export class IsSetupFinishedQuery implements IQuery<IsSetupFinished> {
  async execute(): Promise<IsSetupFinished> {
    const result = await SetupQueryService.isSetupFinished()
    return result
  }
}
