import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }

  async increaseViews(id) {
    await api.get('api/keeps/' + id)
    const keep = AppState.keeps.find(k => k.id === id)
    keep.views++
  }

  async increaseKeeps(id) {
    await api.get('api/keeps/' + id)
    const keep = AppState.keeps.find(k => k.id === id)
    keep.keeps++
  }

  async createKeep(keep) {
    const res = await api.post('/api/keeps', keep)
    logger.log(res.data)
    AppState.keeps = res.data
  }

  async destroyKeep(id) {
    await api.delete('/api/keeps/' + id)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)
  }
}

export const keepsService = new KeepsService()
