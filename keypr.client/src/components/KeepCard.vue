<template>
  <div class="card-container">
    <div class="card bg-dark text-white mt-2 mx-2" style="">
      <img class=" card-img card-img-top" :src="keep.img" :alt="keep.name">
      <div class="card-img-overlay">
        <div class="row d-flex align-content-end fillDiv">
          <div class="col-12">
            <div class="d-flex justify-content-end y-bottom">
              <h1 class="card-title shadowed">
                {{ keep.name }}
                <!-- <router-link :to="{name: 'AccountPage', params: {id:keep.creatorId} }"> -->
                <img class="rounded-circle creator" :src="keep.creator.picture" :alt="keep.creator.name">
                <!-- </router-link> -->
              </h1>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- <KeepDetailsModal :keep="keep" /> -->
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
