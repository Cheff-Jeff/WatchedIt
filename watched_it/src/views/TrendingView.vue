<script setup lang="ts">
import SimpleHeader from '@/components/SimpleHeader.vue';
</script>

<template>
    <SimpleHeader :currentTitle="title" />
    <section class="wrapper">
        <div class="section-title">
            <h1>Trending movies</h1>
        </div>

        <div class="card-container">
            <div class="card" v-for="movie in trendingMovieList" :key="movie.id">
                <div class="movie-image-container">
                    <img v-if="movie.backdrop_path" class="movie-image"  :src="'https://image.tmdb.org/t/p/w1280' + movie.backdrop_path">
                    <img v-else class="movie-image" src="../assets/stockBackground.png">
                </div>
                <div class="info-box">
                    <h1>{{ movie.title }}</h1>
                    <hr />
                    <p>{{ movie.release_date }}</p>
                </div>
            </div>
        </div>

        <div class="section-title">
            <h1>Trending shows</h1>
        </div>

        <div class="card-container">
            <div class="card" v-for="show in trendingShowList" :key="show.id">
                <div class="movie-image-container">
                    <img v-if="show.backdrop_path" class="movie-image" :src="'https://image.tmdb.org/t/p/w1280' + show.backdrop_path">
                    <img v-else class="movie-image" src="../assets/stockBackground.png">
                </div>
                <div class="info-box">
                    <h1>{{ show.name }}</h1>
                    <hr />
                    <p>{{ show.first_air_date }}</p>
                </div>
            </div>
        </div>
    </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { getExternTrendingMovies, getExternTrendingShows } from '@/assets/javascript/api';
export default defineComponent({
    data() {
        return {
            title: "Trending",

            trendingMovieList: [] as any,
            trendingShowList: [] as any,
        }
    },
    async mounted() {
        this.trendingMovieList = await getExternTrendingMovies();
        this.trendingShowList = await getExternTrendingShows();
    }
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import '../assets/styles/pages/trending.scss';
</style>