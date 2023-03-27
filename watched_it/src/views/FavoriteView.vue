<script setup lang="ts">
  import { fetchFavorites } from '@/assets/javascript/api';
  import { favorites } from '@/assets/javascript/Models/ExternApiInterface';
  import { favoriteDto } from '@/assets/javascript/Models/UserInterface';
  import { sortTitles } from '@/assets/javascript/sort';
  import { defineExpose } from 'vue';
  import SimpleHeader from '@/components/SimpleHeader.vue';

  const id: string | null = localStorage.getItem('user')
  let Dtos: favoriteDto[] | undefined
  let favoriteTitles: favorites[] | undefined

  if(id){
    const result = await fetchFavorites(id)
    Dtos = result.value?.data
    if(Dtos){
      favoriteTitles = await sortTitles(Dtos)
    }
  }
  
  defineExpose({favoriteTitles})
</script>

<template>
  <SimpleHeader current-title="Favorites" />
  <div class="container page-wrap">
    <section class="topBar">
      <h1>Your favorite titles.</h1>
    </section>
    <section class="titles" v-if="favoriteTitles && favoriteTitles?.length > 0">
      <div class="row justify-content-center">
        <div class="col-6" v-for="title in favoriteTitles" :key="title.id">
          <router-link v-if="title.isMovie" :to="{name:'details',  params: { type: 'movie', id: title.id }}">
            <div class="card">
              <div class="movie-image-container">
                <img v-if="title.poster" class="movie-image"  :src="`https://image.tmdb.org/t/p/w500${title.poster}`">
                <img v-else class="movie-image" src="../assets/stockBackground.png">
              </div>
              <div class="info-box">
                <h1>{{ title.name }}</h1>
              </div>
            </div>
          </router-link>
          <router-link v-else :to="{name:'details',  params: { type: 'show', id: title.id }}">
            <div class="card">
              <div class="movie-image-container">
                <img v-if="title.poster" class="movie-image"  :src="`https://image.tmdb.org/t/p/w500${title.poster}`">
                <img v-else class="movie-image" src="../assets/stockBackground.png">
              </div>
              <div class="info-box">
                <h1>{{ title.name }}</h1>
              </div>
            </div>
          </router-link>
        </div>
      </div>
    </section>
    <section class="fallback" v-else>
      <div class="container">
        <div class="img-wrap">
          <img src="@/assets/lisa.gif">
        </div>
        <div class="link-wrap">
          <router-link :to="{name: 'trending'}" class="link">
            Trending
          </router-link>
        </div>
      </div>
    </section>
  </div>
</template>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import "@/assets/styles/pages/favorite.scss";
@import "@/assets/styles/components/cards.scss";
</style>