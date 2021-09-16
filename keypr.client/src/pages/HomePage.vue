<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <div class="card-columns">
      <KeepCard v-for="k in state.keeps" :key="k.id" :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import Pop from '../utils/Notifier'
import { logger } from '../utils/Logger'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
export default {
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async() => {
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        Pop.toast('Unable to get all keeps', error)
        logger.log(error)
      }
    })
    return {
      state,
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

body {
  margin: 0;
  padding: 1rem;
}
</style>
