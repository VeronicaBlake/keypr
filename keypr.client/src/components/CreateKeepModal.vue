<template>
  <div class="modal fade"
       id="create-keep"
       tabindex="-1"
       role="dialog"
       aria-labelledby="create-keep"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">
            New Keep
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createdKeep">
            <div class="form-group">
              <label class="pr-2" for="keep-name">Keep Name</label>
              <input type="text"
                     id="keep-name"
                     class="form-control"
                     placeholder="keep Name..."
                     maxlength="50"
                     required
                     v-model="state.createdKeep.name"
              >
            </div>
            <div class="form-group">
              <label class="pr-2" for="keep-img">Image Url</label>
              <input type="text"
                     id="keep-img"
                     class="form-control"
                     placeholder="URL..."
                     required
                     v-model="state.createdKeep.img"
              >
              <!-- Error: No match for {"name":"Keep","params":{"keepId":"undefined"}} -->
              <div class="form-group">
                <label class="pr-2 mt-3" for="keep-description">Description</label>
                <input type="text"
                       id="keep-description"
                       class="form-control"
                       placeholder="Description..."
                       maxlength="200"
                       required
                       v-model="state.createdKeep.description"
                >
                <div class="mt-4">
                  <button v-if="state.createdKeep" type="submit" class="btn btn-primary mr-3">
                    Save
                  </button>
                </div>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import $ from 'jquery'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'
import { reactive, computed } from 'vue'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
export default {
  name: 'Component',
  setup() {
    const router = useRouter()
    const state = reactive({
      createdKeep: {},
      thisKeep: computed(() => AppState.activeKeep)
    })
    return {
      state,
      async createdKeep() {
        try {
          const id = await keepsService.createKeep(state.createdKeep)
          state.createdKeep = {}
          $('#create-keep').modal('hide')
          Pop.toast('Created Keep Successfully', 'success')
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
