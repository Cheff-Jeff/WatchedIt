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
          <p>temp</p>
        </div>
        <div class="row justify-content-start">
          <div class="col-7">
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