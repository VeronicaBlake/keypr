<template>
  <div class="component container-fluid">
    <div class="row mt-5 ml-4">
      <div class="col-md-1 mr-5">
        <img
          :src="state.activeProfile.picture"
          alt="user photo"
          class="profile-pic pr-2 mt-2 mr-3"
          v-if="state.activeProfile.picture"
        />
      </div>
      <div class="col-md-2">
        <div class="row">
          <h1 class="m-0 p-0">
            {{ state.activeProfile.name }}
          </h1>
        </div>
        <div class="row d-flex flex-column mt-3">
          <div class="col">
            <h2>Vaults:</h2>
          </div>
          <div class="col">
            <h2>Keeps:</h2>
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-5 ml-5">
      <div class="col">
        <div class="row">
          <h2>
            Vaults
            <i class="fas fa-plus"></i>
          </h2>
        </div>
        <div class="row">
          <p>v-for for the vault card components goes here</p>
        </div>
      </div>
    </div>
    <div class="row mt-5 ml-5">
      <div class="col">
        <div class="row">
          <h2>
            Keeps
            <i class="fas fa-plus"></i>
          </h2>
        </div>
        <div class="row">
          <div class="card-columns ">
            <KeepCard v-for="k in state.keeps" :key="k.id" :keep="k" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
// import { vaultsService } from '../services/VaultsService'
// import { keepsService } from '../services/KeepsService'
import Notification from '../utils/Notifier'
import { AppState } from '../AppState'
import { accountService } from '../services/AccountService'
export default {
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    const state = reactive({
      newKeep: {},
      newVault: {},
      activeProfile: computed(() => AppState.activeProfile),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.activeKeeps)
    })
    onMounted(async() => {
      try {
        await profilesService.getActiveProfile(route.params.id)
        await profilesService.getProfileVaults(route.params.id)
        await profilesService.getProfileKeeps(route.params.id)
        await accountService.getProfile(route.params.id)
      } catch (error) {
        Notification.toast(error, 'error')
      }
    })
    return {
      state,
      route,
      activeKeeps: computed(() => AppState.activeKeeps),
      activeVaults: computed(() => AppState.activeVaults),
      keeps: computed(() => AppState.keeps)
      // async create vault - option to make it private
      // maybe create private vault too?
      // async create keep
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.profile-pic {
    max-width: 20vw;
    max-height: 30vh;
    min-height: 20vh;
    min-width: 20vh;
    margin-right: 5%;
  }
h1{
  font-size: 38pt;
  margin:0%
}
h2{
    font-size: 20pt;
    margin:0%
}
// img {
//   max-width: 100px;
// }
//TODO media queries on the profile creator row for mobile sizing
</style>
