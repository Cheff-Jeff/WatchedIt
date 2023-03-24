<script setup lang="ts">
import SimpleHeader from '@/components/SimpleHeader.vue';
</script>

<template>
  <SimpleHeader current-title="Favorites" />
  <div class="container page-wrap">
    <section class="topBar">
      <h1>Your favorite titles.</h1>
    </section>
    <section class="titles" v-if="movies">
      <div class="row justify-content-center">
        <div class="col-6" v-for="title in movies" :key="title.id">
          <router-link :to="{name:'details',  params: { type: 'movie', id: title.id }}">
            <div class="card">
              <div class="movie-image-container">
                <img v-if="title.poster_path" class="movie-image"  :src="`https://image.tmdb.org/t/p/w500${title.poster_path}`">
                <img v-else class="movie-image" src="../assets/stockBackground.png">
              </div>
              <div class="info-box">
                <h1>{{ title.title }}</h1>
                <hr>
                <p>{{ title.release_date }}</p>
              </div>
            </div>
          </router-link>
        </div>
      </div>
    </section>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { fetchCommingSoon } from '@/assets/javascript/api';
import { TrendingMovie } from '@/assets/javascript/Models/ExternApiInterface';

export default defineComponent({
  data() {
    return {
      pages: 1,
      totalPages: 1,
      movies: [] as TrendingMovie[]
    }
  },
  async beforeMount() {
    const response = await fetchCommingSoon(this.pages.toString())
    if(response.value?.code == 200){
      this.movies = response.value.data
      this.totalPages = response.value.pages
    }
  },
  created () {
    window.addEventListener('scroll', this.scrollCheck);
  },
  unmounted () {
    window.removeEventListener('scroll', this.scrollCheck);
  },
  methods: {
    async nextPage(){
      if(this.pages <= this.totalPages){
        const response = await fetchCommingSoon(this.pages.toString())
        if(response.value?.code == 200){
          const newMovies: TrendingMovie[] = response.value.data
          for(let i = 0; i < newMovies.length; i++){
            this.movies.push(newMovies[i]);
          }
        }
      }
    },
    scrollCheck() {
      if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
        if(this.pages == 1)
        {
          this.pages++
        }
        this.nextPage()
        this.pages++
      }
    }
  },
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import "@/assets/styles/pages/favorite.scss";
@import "@/assets/styles/components/cards.scss";
</style>