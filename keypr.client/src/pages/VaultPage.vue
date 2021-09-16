<template>
  <div class="component container-fluid">
    <div class="row">
      <div class="col-md-12">
        <div class="row" v-if="state.activeVault">
          <h1>
            {{ state.activeVault.name }}
            <i class="fas fa-trash-alt" title="delete vault"></i>
          </h1>
        </div>
        <div class="row">
          <p>Keeps: {{ state.vaultKeeps.length }}</p>
        </div>
      </div>
      <div class="col-md-12">
        <div class="row" v-if="state.vaultKeeps.length !== 0">
          <p>{{ state.vaultKeeps }}</p>
        </div>
        <div class="row" v-if="state.vaultKeeps.length == 0">
          <h1>No keeps in this vault</h1>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import Notification from '../utils/Notifier'
import { AppState } from '../AppState'
export default {
  name: 'Vaultpage',
  setup() {
    const route = useRoute()
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      activeVault: computed(() => AppState.activeVault)
    })
    onMounted(async() => {
      try {
        await vaultsService.getActiveVault(route.params.id)
        await vaultsService.getVaultKeeps(route.params.id)
      } catch (error) {
        Notification.toast(error, 'error')
        // TODO router push
      }
    })
    return {
      state,
      route
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
