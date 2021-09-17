<template>
  <div class="card-container" data-toggle="modal" :data-target="'#keepDetails-'+keep.id" @click="viewKeep">
    <!-- TODO add an @click="" -->
    <div class="card bg-dark text-white mt-2 mx-2" style="">
      <img class=" card-img card-img-top" :src="keep.img" :alt="keep.name">
      <div class="card-img-overlay">
        <div class="row d-flex align-content-end fillDiv">
          <div class="col-12">
            <div class="d-flex justify-content-end y-top">
              <h1 class="card-title shadowed">
                <i class="fas fa-trash-alt" title="remove from vault" @click.stop="removeKeep"></i>
              </h1>
            </div>
            <div class="d-flex justify-content-end y-bottom">
              <h1 class="card-title shadowed">
                {{ keep.name }}
                <router-link :to="{name: 'Profile', params: {id:keep.creatorId}}" @click.stop>
                  <img class="rounded-circle creator" :src="keep.creator.picture" :alt="keep.creator.name">
                </router-link>
              </h1>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <KeepDetailsModal :keep="keep" />
</template>

<script>
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      viewKeep() {
        keepsService.increaseViews(props.keep.id)
      },
      removeKeep() {
        vaultsService.removeFromVault(AppState.activeVault.id, props.keep.id)
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
.y-top {
  align-self: flex-start;
}
.fillDiv{
  width: 100%;
  height: 100%
}

</style>
