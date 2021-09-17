<template>
  <a class="dropdown-item " @click="addToVault()">{{ vault.name }}</a>
</template>

<script>
import Pop from '../utils/Notifier'
import { vaultsService } from '../services/VaultsService'
// import { AppState } from '../AppState'
export default {
  props: {
    vault: {
      type: Object,
      required: true
    },
    keep: {
      type: Object,
      required: true
    }
  },
  name: 'VaultDropdown',
  setup(props) {
    return {
      async addToVault() {
        try {
          const vaultKeep = {
            vaultId: props.vault.id,
            keepId: props.keep.id
          }
          vaultsService.addToVault(vaultKeep)
          Pop.toast('Keep aded to Vault', 'success')
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
