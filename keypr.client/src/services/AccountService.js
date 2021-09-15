import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getProfile(id) {
    try {
      const res = await api.get(`api/profiles/${id}`)
      AppState.activeProfile = res.data
    } catch (error) {
      logger.error('Error', error)
    }
  }
}
export const accountService = new AccountService()
