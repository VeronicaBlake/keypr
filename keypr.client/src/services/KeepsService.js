import { AppState } from '../AppState'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }

  async increaseViews() {
    const keeps = this.getAllKeeps
    keeps.views++
  }
}

export const keepsService = new KeepsService()
