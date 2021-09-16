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
            <h2>Vaults:{{ state.activeVaults.length }}</h2>
          </div>
          <div class="col">
            <h2>Keeps: {{ state.activeKeeps.length }}</h2>
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-5 ml-5">
      <div class="col">
        <div class="row">
          <h2>
            Vaults
            <div class="col-12 mx-0 px-0 mt-2">
              <!-- v-if -->
              <button class="btn btn-large btn-primary" data-toggle="modal" data-target="#create-vault" title="Create Vault">
                <i class="fas fa-plus text-white"> Create Vault</i>
              </button>
            </div>
          </h2>
          <div class="my-2 row filld">
            <div class="row">
              <VaultProfileCard v-for="v in state.activeVaults" :key="v.id" :vault="v" />
            </div>
          </div>
        </div>
        <div class="row mt-5">
          <div class="col">
            <div class="row">
              <h2>
                Keeps
                <div class="col-12 mx-0 px-0 mt-2">
                  <!-- v-if -->
                  <button class="btn btn-large btn-primary" data-toggle="modal" data-target="#create-keep" title="Create Keep">
                    <i class="fas fa-plus text-white"> Create Keeps</i>
                  </button>
                </div>
              </h2>
            </div>
            <div class="row">
              <KeepProfileCard v-for="k in state.activeKeeps" :key="k.id" :keep="k" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <CreateVaultModal />
  <CreateKeepModal />
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
// import { vaultsService } from '../services/VaultsService'
export default {
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    const state = reactive({
      newKeep: {},
      newVault: {},
      activeProfile: computed(() => AppState.activeProfile),
      activeKeeps: computed(() => AppState.activeKeeps),
      activeVaults: computed(() => AppState.activeVaults),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    onMounted(async() => {
      try {
        await profilesService.getActiveProfile(route.params.id)
        await profilesService.getProfileKeeps(route.params.id)
        await profilesService.getProfileVaults(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      state,
      route
      // async createVault() {
      //   try {
      //     await vaultsService.createVault(state.newVault)
      //     Pop.toast('Successfully Created Vault', 'success')
      //     state.newVault = {}
      //   } catch (error) {
      //     Pop.toast(error, 'error')
      //   }
      // }
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
