<template>
  <div class="modal fade"
       id="create-vault"
       tabindex="-1"
       role="dialog"
       aria-labelledby="create-vault"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">
            New Vault
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createdVault">
            <div class="form-group">
              <label class="pr-2" for="vault-name">Vault Name</label>
              <input type="text"
                     id="vault-name"
                     class="form-control"
                     placeholder="vault Name..."
                     maxlength="50"
                     required
                     v-model="state.createdVault.name"
              >
            </div>
            <div class="form-group">
              <label class="pr-2" for="vault-description">Description</label>
              <input type="text"
                     id="vault-description"
                     class="form-control"
                     placeholder="Description..."
                     maxlength="200"
                     required
                     v-model="state.createdVault.description"
              >
              <div class="form-group">
                <label class="pr-2" for="vault-isPrivate">Make Vault Private?</label>
                <input type="checkbox"
                       id="vault-isPrivate"
                       v-model="state.createdVault.isPrivate"
                >
              </div>
              <div>
                <button v-if="state.createdVault" type="submit" class="btn btn-primary mr-3">
                  Save
                </button>
                <button type="button"
                        class="btn btn-secondary closeModal"
                        data-dismiss="modal"
                >
                  Close
                </button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import $ from 'jquery'
import Pop from '../utils/Notifier'
import { vaultsService } from '../services/VaultsService'
import { reactive, computed } from 'vue'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
export default {
  name: 'Component',
  setup() {
    const router = useRouter()
    const state = reactive({
      createdVault: { isPrivate: false }
      // thisVault: computed(() => AppState.activeVault)
    })
    return {
      state,
      async createdVault() {
        try {
          await vaultsService.createVault(state.createdVault)
          state.createdVault = { isPrivate: false }
          $('#create-vault').modal('hide')
          // router.push({ name: 'vault', params: { id } })
          Pop.toast('Created Vault Successfully', 'success')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
</style>
