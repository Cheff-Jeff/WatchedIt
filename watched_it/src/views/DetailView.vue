<script setup lang="ts">
import { fetchMovie, fetchShow } from '@/assets/javascript/api';
import { titleDetails } from '@/assets/javascript/Models/ExternApiInterface';
import { useRoute } from 'vue-router';
import { onMounted, defineExpose } from 'vue';
import {MDCRipple} from '@material/ripple';

const route = useRoute();
let details: titleDetails | undefined

if(route.params.type == 'movie'){
  const result = await fetchMovie(route.params.id.toString())
  details = result.value?.data
}
else{
  const result = await fetchShow(route.params.id.toString())
  details = result.value?.data
}
onMounted(() => { 
  const buttons = document.getElementsByClassName('mdc-button');
  if(buttons){
    for(let btn of buttons){
      const buttonRipple = new MDCRipple(btn);
    }
  }
});
defineExpose({details})
</script>

<template>
  <div class="page-wrap" v-if="details">
    <section class="banner" :style="{ 'background-image': `url(https://image.tmdb.org/t/p/w500/${details.backdrop})` }">
      <div class="container d-flex flex-column justify-content-between setHeight">
        <div class="row">
          <div class="top-bar">
            <div class="back" @click="()=>{$router.back()}">
              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512">
                <path d="M9.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.2 288 416 288c17.7 0 32-14.3 32-32s-14.3-32-32-32l-306.7 0L214.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z"/>
              </svg>
            </div>
            <div class="rating">
              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512">
                <path d="M316.9 18C311.6 7 300.4 0 288.1 0s-23.4 7-28.8 18L195 150.3 51.4 171.5c-12 1.8-22 10.2-25.7 21.7s-.7 24.2 7.9 32.7L137.8 329 113.2 474.7c-2 12 3 24.2 12.9 31.3s23 8 33.8 2.3l128.3-68.5 128.3 68.5c10.8 5.7 23.9 4.9 33.8-2.3s14.9-19.3 12.9-31.3L438.5 329 542.7 225.9c8.6-8.5 11.7-21.2 7.9-32.7s-13.7-19.9-25.7-21.7L381.2 150.3 316.9 18z"/>
              </svg>
              <p>
                {{ Math.round(details.vote_average * 10) / 10 }}
                <span>
                  ({{ details.vote_average }})
                </span>
              </p>
            </div>
          </div>
        </div>
        <div class="row justify-content-start bottom-wrap">
          <div class="col-7">
            <div class="icon">
              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path d="M244 84L255.1 96L267.1 84.02C300.6 51.37 347 36.51 392.6 44.1C461.5 55.58 512 115.2 512 185.1V190.9C512 232.4 494.8 272.1 464.4 300.4L283.7 469.1C276.2 476.1 266.3 480 256 480C245.7 480 235.8 476.1 228.3 469.1L47.59 300.4C17.23 272.1 0 232.4 0 190.9V185.1C0 115.2 50.52 55.58 119.4 44.1C164.1 36.51 211.4 51.37 244 84C243.1 84 244 84.01 244 84L244 84zM255.1 163.9L210.1 117.1C188.4 96.28 157.6 86.4 127.3 91.44C81.55 99.07 48 138.7 48 185.1V190.9C48 219.1 59.71 246.1 80.34 265.3L256 429.3L431.7 265.3C452.3 246.1 464 219.1 464 190.9V185.1C464 138.7 430.4 99.07 384.7 91.44C354.4 86.4 323.6 96.28 301.9 117.1L255.1 163.9z"/></svg>
            </div>
            <div class="title-wrap">
              <h1>{{ details.title }}</h1>
            </div>
          </div>
        </div>
      </div>
    </section>
    <section class="details">
      <div class="container">
        <div class="row">
          <div class="col-7">
            <div class="detail-wrap">
              <div class="top">
                <p>Directed by: {{ details.director }}</p>
                <p>Released at: {{ details.first_air_date }}</p>
              </div>
              <div class="bottom">
                <p>Genre: <span v-for="genre in details.genres" :key="genre.id">{{ genre.name }},</span> </p>
                <p>Language: {{ details.language }}</p>
                <hr>
              </div>
            </div>
          </div>
          <div class="col-5">
            <div class="poster">
              <img :src="`https://image.tmdb.org/t/p/w500/${details.poster}`" alt="poster">
            </div>
          </div>
        </div>
        <div class="overview">
          <p>{{ details.overview }}</p>
        </div>
        <div class="providers">
          <div class="mdc-touch-target-wrapper btn-wrap" v-for="provider in details.providers" :key="provider.name">
            <button class="mdc-button mdc-button--raised">
              <span class="mdc-button__ripple"></span>
              <span class="mdc-button__touch"></span>
              <i class="material-icons mdc-button__icon" aria-hidden="true">
                <img :src="`https://image.tmdb.org/t/p/w500/${provider.logo}`" alt="">                 
              </i>
              <span class="mdc-button__label">{{ provider.name }}</span>
            </button>
          </div>
        </div>
      </div>
    </section>
    <section class="cast">
      <div class="container">
        <h2>Actors</h2>
        <div class="cast-wrap">
          <div v-for="actor in details?.cast" :key="actor.name" class="cast-member">
            <img v-if="actor.photo" :src="`https://image.tmdb.org/t/p/w500/${actor.photo}`">
            <img v-else src="@/assets/profilePicture.png">
            <span>{{ actor.name }}</span>
          </div>
        </div>
      </div>
    </section>
    <section class="addList">
      <div class="container">
        <div class="mdc-touch-target-wrapper btn-wrap">
          <button class="mdc-button mdc-button--raised">
            <span class="mdc-button__ripple"></span>
            <span class="mdc-button__touch"></span>
            <span class="mdc-button__label">Add to list</span>
          </button>
        </div>
      </div>
    </section>
  </div>
</template>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import "@/assets/styles/components/buttons.scss";
@import "@/assets/styles/pages/details.scss";
</style>