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

  async addToVault(vaultKeep) {
    await api.post('api/vaultKeeps', vaultKeep)
    // this.getVaultKeeps(vaultKeep.vaultId)
  }

  async removeFromVault(vaultId, vaultKeepId) {
    await api.delete(`api/vaultKeeps/${vaultKeepId}`)
    this.getVaultKeeps(vaultId)
  }

  async createVault(vault) {
    const res = await api.post('/api/vaults', vault)
    AppState.activeVaults.push(res.data)
    return res.data.id
  }

  async destroyVault(id) {
    await api.delete('/api/vaults/' + id)
    AppState.vaults = AppState.activeVaults.filter(v => v.id !== id)
  }
}

export const vaultsService = new VaultsService()
