<template>
  <div class="card bg-dark text-white component-rounded" style="width:18rem">
    <img class="card-img" src="keep.img" alt="Keep Image">
    <div class="card-img-overlay">
      <h5 class="card-title">
        {{ keep.name }}
      </h5>
      <p class="card-text">
        {{ keep.description }}, {{ keep.creator }}
      </p>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
