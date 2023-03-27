<script setup lang="ts">
import SimpleHeader from '@/components/SimpleHeader.vue';
import Details from '@/components/Title-Details.vue';
</script>

<template>
  <SimpleHeader current-title="Upcoming" />
  <div class="animate-wrap" v-auto-animate>
    <div class="container page-wrap" id="wrapper" :style="{'max-height' : pageHeight}">
      <section class="topBar">
        <h1>Upcoming movies.</h1>
      </section>
      <section class="titles" v-if="movies">
        <div class="row justify-content-center">
          <div class="col-6" v-for="title in movies" :key="title.id">
            <div class="card" @click="showDetails(title.id, 'movie')">
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
          </div>
        </div>
      </section>
    </div>
    <section class="details" id="Overlay">
      <Details v-if="detailsToggle"
        :id="details.id.toString()"
        :banner="details.banner"
        :voteAvrage="Number(details.VoteAvrage)"
        :title="details.title"
        :director="details.director"
        :date="details.date"
        :genres="details.genres"
        :language="details.language"
        :poster="details.poster"
        :overview="details.overview"
        :providers="details.providers"
        :actors="details.actors"
        :title-type="details.titleType"
        @mounted="detailStyle()"
        @close="closeOverlay()"
        @rerender="reRender()"
      />
    </section>  
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { fetchCommingSoon } from '@/assets/javascript/api';
import { TrendingMovie } from '@/assets/javascript/Models/ExternApiInterface';
import { genre, provider, actor } from '@/assets/javascript/Models/ExternApiInterface'
import { fetchMovie } from '@/assets/javascript/api';

export default defineComponent({
  data() {
    return {
      pages: 1,
      totalPages: 1,
      movies: [] as TrendingMovie[],
      detailsToggle: false,
      pageHeight: 'auto',
      scroll: 0 as number,
      scrollHeight: 0,
      details: {
        id: 0,
        banner: '',
        VoteAvrage: '',
        title: '',
        director: '',
        date: '',
        genres: [] as genre[],
        language: '',
        poster: '',
        overview: '',
        providers: [] as provider[],
        actors: [] as actor[],
        titleType: ''
      }
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
    },
    async showDetails(id: number, type: string){
      const result = await fetchMovie(id.toString())
      if(result.value?.code == 200){
        this.details = {
          id: result.value.data.id,
          banner: result.value.data.backdrop,
          VoteAvrage: result.value.data.vote_average.toString(),
          title: result.value.data.title,
          director: result.value.data.director,
          date: result.value.data.first_air_date,
          genres: result.value.data.genres,
          language: result.value.data.language,
          poster: result.value.data.poster,
          overview: result.value.data.overview,
          providers: result.value.data.providers,
          actors: result.value.data.cast,
          titleType: type
        }
      }
      this.scroll = document.documentElement.scrollTop
      this.detailsToggle = true
    },
    detailStyle(){
      if(this.detailsToggle)
      {
        window.scrollTo(0, 0)
        const overlay = document.getElementById('Overlay');
        this.pageHeight = overlay!.offsetHeight.toString() + "px"
      }
    },
    closeOverlay(){
      if(this.detailsToggle)
      {
        this.pageHeight = "none"
        this.detailsToggle = false
        setTimeout(()=>{
          window.scrollTo(0, this.scroll)
        }, 5);
      }
    },
    reRender(){
      this.detailsToggle = false
      this.$nextTick(()=>{
        this.detailsToggle = true
      })
    }
  },
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import "@/assets/styles/pages/favorite.scss";
@import "@/assets/styles/components/cards.scss";
</style>