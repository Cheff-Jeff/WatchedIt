<script setup lang="ts">
import { fetchUser } from "@/assets/javascript/api";
import { User } from "@/assets/javascript/Models/UserInterface";
import SimpleHeader from "@/components/SimpleHeader.vue";
import { MDCRipple } from "@material/ripple";
import { onMounted, defineExpose } from "vue";
const id: string | null = localStorage.getItem("user");
let user: User | undefined;

if (id) {
  const result = await fetchUser(id);
  user = result.value?.data;
}

onMounted(() => {
  const buttons = document.getElementsByClassName("mdc-button");
  if (buttons) {
    for (let btn of buttons) {
      const buttonRipple = new MDCRipple(btn);
    }
  }
});

defineExpose({ user });
</script>

<template>
  <SimpleHeader currentTitle="Account" />
  <section class="section-pagewrap">
    <div class="container bg">
      <section class="greed" v-if="user">
        <div class="row">
          <div class="col-8">
            <div class="person">
              <h1>Hello there,</h1>
              <span>{{ user.name }}</span>
            </div>
          </div>
          <div class="col-4">
            <div class="mdc-touch-target-wrapper btn-wrap white">
              <router-link :to="{ name: 'editaccount' }">
                <button class="mdc-button mdc-button--raised">
                  <span class="mdc-button__ripple"></span>
                  <span class="mdc-button__touch"></span>
                  <span class="mdc-button__label">Edit account</span>
                </button>
              </router-link>
            </div>
            <div class="mdc-touch-target-wrapper btn-wrap">
              <button class="mdc-button mdc-button--raised" @click="download">
                <span class="mdc-button__ripple"></span>
                <span class="mdc-button__touch"></span>
                <span class="mdc-button__label">Download app</span>
              </button>
            </div>
          </div>
        </div>
      </section>
      <section class="nav">
        <div class="row justify-content-around">
          <div class="col-5">
            <div class="link-wrapper">
              <a href="#recomandations" class="link">
                Recomandations
              </a>
            </div>
          </div>
          <div class="col-5">
            <div class="link-wrapper">
              <router-link :to="{ name: 'watchlist' }" class="link">
                Watch lists
              </router-link>
            </div>
          </div>
          <div class="col-5">
            <div class="link-wrapper">
              <router-link :to="{ name: 'friends' }" class="link"> Friends </router-link>
            </div>
          </div> 
          <div class="col-5">
            <div class="link-wrapper">
              <router-link :to="{ name: 'favorites' }" class="link">
                Favorites
              </router-link>
            </div>
          </div>
        </div>
      </section>
      <section class="movies" id="recomandations">
        <div class="section-title">
          <h1>Movies for you</h1>
        </div>

        <div class="card-container" v-if="movieRecom" id="movieContainer">
          <div class="card" v-for="movie in movieRecom" :key="movie.id">
            <router-link
              :to="{ name: 'details', params: { type: 'movie', id: movie.id } }"
            >
              <div class="movie-image-container">
                <img
                  v-if="movie.poster_path"
                  class="movie-image"
                  :src="`https://image.tmdb.org/t/p/w500${movie.poster_path}`"
                />
                <img v-else class="movie-image" src="../assets/stockBackground.png" />
              </div>
              <div class="info-box">
                <h1>{{ movie.title }}</h1>
                <hr />
                <p>{{ movie.release_date }}</p>
              </div>
            </router-link>
          </div>
        </div>
      </section>
      <section class="shows">
        <div class="section-title">
          <h1>Shows for you</h1>
        </div>

        <div class="card-container" v-if="showRecom">
          <div class="card" v-for="show in showRecom" :key="show.id">
            <router-link :to="{ name: 'details', params: { type: 'show', id: show.id } }">
              <div class="movie-image-container">
                <img
                  v-if="show.poster_path"
                  class="movie-image"
                  :src="`https://image.tmdb.org/t/p/w500${show.poster_path}`"
                />
                <img v-else class="movie-image" src="../assets/stockBackground.png" />
              </div>
              <div class="info-box">
                <h1>{{ show.name }}</h1>
                <hr />
                <p>{{ show.first_air_date }}</p>
              </div>
            </router-link>
          </div>
        </div>
      </section>
    </div>
  </section>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import {
  checkMovieRecomendations,
  moviewRecomandations,
  checkShowRecomendations,
  showRecomandations,
  getExternTrendingShows,
  getExternTrendingMovies,
} from "@/assets/javascript/api";
import {
  TrendingMovie,
  TrendingShow,
} from "@/assets/javascript/Models/ExternApiInterface";
import { favoriteDto } from "@/assets/javascript/Models/UserInterface";

export default defineComponent({
  data() {
    return {
      page: 1,
      sPage: 1,
      movieRecom: [] as TrendingMovie[],
      showRecom: [] as TrendingShow[],
    };
  },
  async beforeMount() {
    const id: string | null = localStorage.getItem("user");
    if (id) {
      const favRes = await checkMovieRecomendations(id);
      if (favRes.value?.code == 200) {
        const favorite: favoriteDto = favRes.value.data;
        const res = await moviewRecomandations(
          favorite.movieId!.toString(),
          this.page.toString()
        );
        if (res.value?.code == 200) {
          this.movieRecom = res.value.data;
        }
      } else {
        const res = await getExternTrendingMovies();
        if (res.value) {
          this.movieRecom = res.value;
        }
      }
      const showRes = await checkShowRecomendations(id);
      if (showRes.value?.code == 200) {
        const sfavorite: favoriteDto = showRes.value.data;
        const res = await showRecomandations(
          sfavorite.movieId!.toString(),
          this.sPage.toString()
        );
        if (res.value?.code == 200) {
          this.showRecom = res.value.data;
        }
      } else {
        const res = await getExternTrendingShows();
        if (res.value) {
          this.showRecom = res.value;
        }
      }
    }
  },
  methods: {
    download() {
      this.$emit("download");
    },
  },
});
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import "@/assets/styles/components/buttons.scss";
@import "@/assets/styles/pages/account.scss";
@import "@/assets/styles/components/cards.scss";
</style>
