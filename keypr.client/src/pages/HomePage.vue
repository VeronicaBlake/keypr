<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <div class="row card-columns">
      <div class="col" v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import Pop from '../utils/Notifier'
import { logger } from '../utils/Logger'
import { keepsService } from '../services/keepsService'
import { AppState } from '../AppState'
export default {
  setup() {
    onMounted(async() => {
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        Pop.toast('Unable to get all keeps', error)
        logger.log(error)
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
