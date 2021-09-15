<template>
  <div class="component container-fluid">
    <div class="row">
      <div class="col">
        <div class="row">
          <h1>
            {{ state.activeVault }}
            <i class="fas fa-trash-alt" title="delete vault"></i>
          </h1>
        </div>
        <div class="row">
          <p>Keeps: {{ state.vaultKeeps.length }}</p>
        </div>
      </div>
      <div class="row">
        <p>{{ state.vaultKeeps }}</p>
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

//find the keeps that have this vault id

<style lang="scss" scoped>

</style>
