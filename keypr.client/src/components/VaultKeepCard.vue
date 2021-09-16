<template>
  <div class="card-container">
    <div class="card bg-dark text-white mt-2 mx-2" style="">
      <img class=" card-img card-img-top" :src="vaultKeep.img" :alt="vaultKeep.name">
      <div class="card-img-overlay">
        <div class="row d-flex align-content-end fillDiv">
          <div class="col-12">
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
export default {
  props: {
    vaultKeep: {
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
