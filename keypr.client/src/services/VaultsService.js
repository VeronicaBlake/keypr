import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async getActiveVault(id) {
    const res = await api.get('api/vaults/' + id)
    AppState.activeVault = res.data
  }

  async getVaultKeeps(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.vaultKeeps = res.data
  }

  async createVault(vault) {
    const res = await api.post('/api/vaults', vault)
    AppState.activeVaults.push(res.data)
    return res.data.id
  }
}

export const vaultsService = new VaultsService()
