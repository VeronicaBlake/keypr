<template>
  <div class="card-container">
    <div class="card bg-dark text-white mt-2 mx-2" style="">
      <img class=" card-img card-img-top" :src="vaultKeep.img" :alt="vaultKeep.name">
      <div class="card-img-overlay">
        <div class="row d-flex align-content-end fillDiv">
          <div class="col-12">
            <div class="d-flex justify-content-end y-top">
              <h1 class="card-title shadowed">
                <!-- NOTE this is the v-if for hiding the icons -->
                <i class="fas fa-trash-alt" title="remove from vault" @click.stop="removeKeep" v-if="vaultKeep.creatorId === account.id"></i>
              </h1>
            </div>
            <div class="d-flex justify-content-end y-bottom">
              <h1 class="card-title shadowed">
                {{ vaultKeep.name }}
                <router-link :to="{name: 'Profile', params: {id:vaultKeep.creatorId}}" @click.stop>
                  <img class="rounded-circle creator" :src="vaultKeep.creator.picture" :alt="vaultKeep.creator.name">
                </router-link>
              </h1>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- <vaultKeepDetailsModal :vaultKeep="vaultKeep" /> -->
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
import { logger } from '../utils/Logger'

export default {
  props: {
    vaultKeep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      async removeKeep() {
        try {
          if (await Pop.confirm()) {
            vaultsService.removeFromVault(AppState.activeVault.id, props.vaultKeep.vaultKeepId)
            Pop.toast('Removed Keep From Vault', 'success')
          }
        } catch (error) {
          Pop.toast(error, 'error')
          logger.log(error)
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.card-img{
  object-fit: contain;
  position: relative
}
.shadowed{
  text-shadow: 2px 2px 20px rgb(7, 7, 7)
}
.creator{
  max-width: 50px;
  max-height: 50px
}
.y-bottom {
  align-self: flex-end;
}
.fillDiv{
  width: 100%;
  height: 100%
}

</style>
