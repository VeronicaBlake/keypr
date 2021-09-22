<template>
  <div
    class="modal fade"
    :id="'keepDetails-'
      +keep.id"
    tabindex="-1"
    role="dialog"
    aria-labelledby="modelTitleId"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-lg" role="document">
      <div class="modal-content modal-height">
        <div class="modal-body modal-overflow">
          <div class="row d-flex px-3 justify-content-end">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="row my-3">
            <!-- keep image -->
            <div class="col-md-6 mb-3">
              <img class="modalImg"
                   :src="keep.img"
                   :alt="keep.name"
              >
            </div>
            <!-- right side -->
            <div class="col-md-6 px-3">
              <!-- eye and key icons -->
              <div class="row d-flex justify-content-center p-0 m-0">
                <div class="col-md-3">
                  <p><i class="fas fa-eye pt-2 pr-md-1 text-info" title="views"></i>{{ keep.views }}</p>
                </div>
                <div class="col-md-3">
                  <p><i class="fas fa-key pr-md-1 text-primary" title="times kept in a vault"></i> {{ keep.keeps }}</p>
                </div>
              </div>
              <!-- nam, trash and desc -->
              <div class="row">
                <div class="col-md-12">
                  <h1>
                    {{ keep.name }}
                  </h1>
                  <div class="col-md-12">
                    <i class="fas fa-trash-alt trashCan fa-lg" title="destroy keep" @click.stop="destroyKeep" v-if="keep.creator.id === state.account.id"></i>
                  </div>
                </div>
                <div class="col-md-12">
                  <p>{{ keep.description }}</p>
                </div>
              </div>
              <div class="row d-flex justify-content-center">
                <!-- dropdown -->
                <div class="col-md-6 my-2 mx-0 p-0">
                  <div class="dropdown">
                    <button class="btn btn-primary dropdown-toggle"
                            type="button"
                            id="dropdownMenuButton"
                            data-toggle="dropdown"
                            aria-haspopup="true"
                            aria-expanded="false"
                    >
                      Add to Vault
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                      <VaultDropdown v-for="v in state.vaults" :key="v.id" :vault="v" :keep="keep" />
                    </div>
                  </div>
                </div>
                <!-- creator -->
                <div class="col-md-6 my-2 mx-0 p-0">
                  <!-- TODO router push to profile page -->
                  <img
                    :src="keep.creator.picture"
                    alt="user photo"
                    height="40"
                    class="rounded"
                    :title="keep.creator.name"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      vaults: computed(() => AppState.activeVaults),
      activeKeep: computed(() => AppState.keep),
      newVaultKeep: { keepId: props.keep.id, vaultId: '' },
      account: computed(() => AppState.account)
    }
    )
    onMounted(async() => {
      try {
        AppState.keep = props.keep
      } catch (error) {
        Pop.toast('Unable to get all keeps', error)
      }
    })
    return {
      state,
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      async destroyKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.destroyKeep(props.keep.id)
            Pop.toast('Deleted Keep', 'success')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }

}
</script>

<style lang="scss" scoped>
.modalImg{
   object-fit: cover;
  width: 100%;
    height: 15vw;
    margin-bottom: 0%;
}
.modal-overflow{
  overflow: auto;
}
.modal-height{
    max-height: 100%;
}
.trashCan{
  color: rgb(94, 96, 98);
}
</style>
