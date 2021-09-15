<template>
  <div class="card-container my-5">
    <div class="card text-white my-2 mx-2" style="">
      <img class=" card-img card-img-top" :src="keep.img" :alt="keep.name">
      <div class="card-img-overlay">
        <div class="row d-flex align-content-end fillDiv">
          <div class="col-12">
            <div class="d-flex justify-content-end y-bottom">
              <h1 class="card-title shadowed">
                {{ keep.name }}
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
.card{
max-height: 20vh;
max-width: 20vw;
min-height: 20vh;
min-width: 20vw;
}
.card-img{
  object-fit: cover;
  width: 100%;
    height: 15vw;

}
.shadowed{
  text-shadow: 2px 2px 20px rgb(7, 7, 7)
}
.y-bottom {
  align-self: flex-end;
}
.fillDiv{
  width: 100%;
  height: 100%
}

</style>
