<template>
  <a class="dropdown-item " @click="addToVault(vault)">{{ vault.name }}</a>
</template>

<script>
import Pop from '../utils/Notifier'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  name: 'VaultDropdown',
  setup() {
    return {
      async addToVault(vault) {
        try {
          const vaultKeep = {
            vaultId: vault.Id,
            keepId: AppState.keep.Id
          }
          vaultsService.addToVault(vaultKeep)
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
