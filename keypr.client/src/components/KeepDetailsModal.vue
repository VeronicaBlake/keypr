<template>
  <!-- Modal -->
  <div
    class="modal fade"
    :id="'keepDetails'
      +keep.id"
    tabindex="-1"
    role="dialog"
    aria-labelledby="modelTitleId"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-lg" role="document">
      <div class="modal-content modal-height">
        <div class="modal-header">
          <h3 class="modal-title">
            {{ keep.name }}
          </h3>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body modal-overflow">
          <div class="row p-3 my-3 d-flex align-items-center">
            <div class="col-md-3 col-12 mb-3">
              <img class="modalImg"
                   :src="keep.img"
                   :alt="keep.name"
              >
            </div>
            <div class="col-md-9 col-12 px-3">
              <p><i class="fas fa-eye pr-1 text-info" title="views"></i>{{ keep.views }}</p>
              <p><i class="fas fa-lock pr-1 text-primary" title="times kept in a vault"></i> {{ keep.keeps }}</p>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">
            Close
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted } from '@vue/runtime-core'
export default {
  props: {
    game: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    onMounted(() => { document.getElementById('description' + props.game.id).innerHTML = props.game.description })
    return {}
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.modalImg{
  max-width: 100%;
}
.modal-overflow{
  overflow: auto;
}
.modal-height{
    max-height: 95vh;
}
</style>
