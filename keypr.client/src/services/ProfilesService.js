import { AppState } from '../AppState'
import { api } from './AxiosService'

class ProfilesService {
  async getActiveProfile(id) {
    const res = await api.get('api/profiles/' + id)
    AppState.activeProfile = res.data
  }

  async getProfileVaults(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.activeVaults = res.data.vaults
  }

  async getProfileKeeps(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.activeKeeps = res.data.keeps
  }

  async getAllProfiles() {
    const res = await api.get('api/profile/')
    AppState.activeProfile = res.data
  }
}

export const profilesService = new ProfilesService()
