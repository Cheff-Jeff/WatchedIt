<script setup lang="ts">
import { fetchMovie, fetchShow } from '@/assets/javascript/api';
import { titleDetails } from '@/assets/javascript/Models/ExternApiInterface';
import { ref, Ref } from 'vue';
import { useRoute } from 'vue-router';
import { onMounted, defineExpose } from 'vue';

const route = useRoute();
let details: titleDetails | undefined
// let details:titleDetails | null

if(route.params.type == 'movie'){
  const result = await fetchMovie(route.params.id.toString())
  details = result.value?.data
  // let res = await fetchMovie(route.params.id.toString())
}
else{
  const result = await fetchShow(route.params.id.toString())
  details = result.value?.data
}
defineExpose({details})
</script>

<template>
  <div class="container" v-if="details">
    <img :src="`https://image.tmdb.org/t/p/w500/${details.backdrop}`" alt="backdrop">
    <img :src="`https://image.tmdb.org/t/p/w500/${details.poster}`" alt="poster">
    <h1>{{ details.title }}</h1>
    <p>{{ details.overview }}</p>
    <p>Directed by: {{ details.director }}</p>
    <p>Released at: {{ details.first_air_date }}</p>
    <p>Genre: <span v-for="genre in details.genres" :key="genre.id">{{ genre.name }}</span> </p>
    <p>Language: {{ details.language }}</p>
    <span v-for="provider in details.providers" :key="provider.name">
      <img :src="`https://image.tmdb.org/t/p/w500/${provider.logo}`" alt=""> 
      {{ provider.name }}
    </span>
  </div>
  <h2>Actors</h2>
  <div v-for="actor in details?.cast" :key="actor.name">
    <img :src="`https://image.tmdb.org/t/p/w500/${actor.photo}`">
    <span>{{ actor.name }}</span>
  </div>
</template>