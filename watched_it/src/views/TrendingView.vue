<script setup lang="ts">
import SimpleHeader from '@/components/SimpleHeader.vue';
</script>

<template>
    <SimpleHeader :currentTitle="title" />
    <section>
        <div class="section-title">
            <h1>Trending movies</h1>
        </div>

        <div class="card-container">
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
        </div>

        <div class="section-title">
            <h1>Trending shows</h1>
        </div>

        <div class="card-container">
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">
                    <img class="movie-image" src="https://imgur.com/rYSzWYZ.png">
                </div>
                <div class="info-box">
                    <h1>Halloween Kills</h1>
                    <hr />
                    <p>Released at: 1978-04-17</p>
                </div>
            </div>
        </div>
    </section>
</template>

<script lang="ts">
export default {
    data() {
        return {
            title: "Trending"
        }
    },
}
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import '../assets/styles/pages/trending.scss';
</style>