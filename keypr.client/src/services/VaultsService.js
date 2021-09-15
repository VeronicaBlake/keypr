import { AppState } from '../AppState'
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
}

export const vaultsService = new VaultsService()
