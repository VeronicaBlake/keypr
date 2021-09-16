<template>
  <div class="component container-fluid">
    <div class="row">
      <div class="col-md-12">
        <div class="row" v-if="state.activeVault">
          <h1>
            {{ state.activeVault.name }}
            <i class="fas fa-trash-alt" title="delete vault" @click.stop="destroyVault"></i>
          </h1>
        </div>
        <div class="row">
          <p>Keeps: {{ state.vaultKeeps.length }}</p>
        </div>
      </div>
      <div class="col-md-12">
        <div class="row" v-if="state.vaultKeeps.length !== 0">
          <KeepCard v-for=" k in state.vaultKeeps"
                    :key="k.id"
                    :keep="k"
          />
        </div>
        <div class="row" v-if="state.vaultKeeps.length == 0">
          <h1>No keeps in this vault</h1>
        </div>
        <div class="row" v-if="state.activeVault.isPrivate == true">
          <h1>This Vault is Private</h1>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { useRoute, useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
export default {
  name: 'Vaultpage',
  setup() {
    const route = useRoute()
    const router = useRouter()
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      activeVault: computed(() => AppState.activeVault)
    })
    onMounted(async() => {
      try {
        await vaultsService.getActiveVault(route.params.id)
        // await vaultsService.getVaultKeeps(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
        router.push({ name: 'Home' })
      }
    })
    return {
      state,
      route,
      async destroyVault() {
        try {
          if (await Pop.confirm()) {
            await vaultsService.destroyVault(state.activeVault.id)
            Pop.toast('Deleted Vault', 'success')
            router.push({ name: 'Home' })
          }
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
