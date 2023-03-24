<script setup lang="ts">

</script>

<template>
    <section>

        <Transition name="bounce">

            <div v-if="show" class="card-container">
                <div class="card" :key="MovieShow.id">
                    <div class="movie-image-container">
                        <img v-if="MovieShow.poster" class="movie-image"
                            :src="'https://image.tmdb.org/t/p/w1280' + MovieShow.poster">
                        <img v-else class="movie-image" src="../assets/stockBackground.png">
                    </div>
                    <div class="info-box">
                        <h1>{{ MovieShow.title }}</h1>
                        <hr />
                        <p>{{ MovieShow.first_air_date }}</p>
                    </div>
                </div>

            </div>
        </Transition>


        <div class="interact-box">

            <div class="desc-container">
                <h1 v-for="item in MovieShow.genres" :key="item.id">{{ item.name }}, </h1>
                <hr />
                <p>{{ MovieShow.overview }}</p>

                <div class="mdc-touch-target-wrapper btn-wrap white">
                    <button class="mdc-button mdc-button--raised">
                        <span class="mdc-button__ripple"></span>
                        <span class="mdc-button__touch"></span>
                        <span class="mdc-button__label">View more</span>
                    </button>
                </div>
            </div>

            <div class="like" v-on:click="LikeItem()">
                <svg xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 512 512"><!--! Font Awesome Pro 6.3.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                    <path
                        d="M313.4 32.9c26 5.2 42.9 30.5 37.7 56.5l-2.3 11.4c-5.3 26.7-15.1 52.1-28.8 75.2H464c26.5 0 48 21.5 48 48c0 18.5-10.5 34.6-25.9 42.6C497 275.4 504 288.9 504 304c0 23.4-16.8 42.9-38.9 47.1c4.4 7.3 6.9 15.8 6.9 24.9c0 21.3-13.9 39.4-33.1 45.6c.7 3.3 1.1 6.8 1.1 10.4c0 26.5-21.5 48-48 48H294.5c-19 0-37.5-5.6-53.3-16.1l-38.5-25.7C176 420.4 160 390.4 160 358.3V320 272 247.1c0-29.2 13.3-56.7 36-75l7.4-5.9c26.5-21.2 44.6-51 51.2-84.2l2.3-11.4c5.2-26 30.5-42.9 56.5-37.7zM32 192H96c17.7 0 32 14.3 32 32V448c0 17.7-14.3 32-32 32H32c-17.7 0-32-14.3-32-32V224c0-17.7 14.3-32 32-32z" />
                </svg>
            </div>
            <div class="dislike" v-on:click="DisLikeItem()">
                <svg xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 512 512"><!--! Font Awesome Pro 6.3.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                    <path
                        d="M313.4 479.1c26-5.2 42.9-30.5 37.7-56.5l-2.3-11.4c-5.3-26.7-15.1-52.1-28.8-75.2H464c26.5 0 48-21.5 48-48c0-18.5-10.5-34.6-25.9-42.6C497 236.6 504 223.1 504 208c0-23.4-16.8-42.9-38.9-47.1c4.4-7.3 6.9-15.8 6.9-24.9c0-21.3-13.9-39.4-33.1-45.6c.7-3.3 1.1-6.8 1.1-10.4c0-26.5-21.5-48-48-48H294.5c-19 0-37.5 5.6-53.3 16.1L202.7 73.8C176 91.6 160 121.6 160 153.7V192v48 24.9c0 29.2 13.3 56.7 36 75l7.4 5.9c26.5 21.2 44.6 51 51.2 84.2l2.3 11.4c5.2 26 30.5 42.9 56.5 37.7zM32 384H96c17.7 0 32-14.3 32-32V128c0-17.7-14.3-32-32-32H32C14.3 96 0 110.3 0 128V352c0 17.7 14.3 32 32 32z" />
                </svg>
            </div>
        </div>
    </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
    data() {
        return {
            show: true,
            movieshowArray: JSON.parse(JSON.stringify(this.votelist)),

            MovieShow: {
                id: '',
                poster: '',
                title: '',
                first_air_date: '',
                overview: '',
                genres: [] as any,
            },



            liked: false,
            likedMovieShow: [] as any,

            disliked: false,
            dislikedMovieShow: [] as any,
        }
    },
    emits: [
        "closeCardSwipePopUp"
    ],
    props: {
        votelist: {
            type: Array as any,
            required: true
        }
    },
    async mounted() {
        const i: number = this.movieshowArray.map((item: { id: any; }) => item.id).indexOf(this.movieshowArray[0].id);
        this.MovieShow = this.movieshowArray[0]

        this.movieshowArray.splice(i, 1);
    },
    methods: {
        LikeItem() {
            if (this.MovieShow.id != null) {
                this.likedMovieShow.push(this.MovieShow.id)

                this.liked = !this.liked

                if (this.movieshowArray.length != 0) {
                    this.NextItem(this.MovieShow.id)
                } else {
                    //api call maken met likedarray
                    this.closeModal()
                }
            }
        },
        DisLikeItem() {
            if (this.MovieShow.id != null) {
                this.dislikedMovieShow.push(this.MovieShow.id)

                this.disliked = !this.disliked

                if (this.movieshowArray.length != 0) {
                    this.NextItem(this.MovieShow.id)
                } else {
                    //api call maken met dislikedarray
                    this.closeModal()
                }
            }
        },
        NextItem(id: string) {
            this.show = !this.show

            const i: number = this.movieshowArray.map((item: { id: any; }) => item.id).indexOf(id);
            const item = this.movieshowArray.splice(i, 1);

            this.MovieShow = item[0]

            var timer = null
            if (timer) {
                clearTimeout(timer);
                timer = null;
            }
            timer = setTimeout(async () => {
                this.show = !this.show
            }, 1000);
        },
        closeModal() {
            this.$emit('closeCardSwipePopUp', true)
        },
        truncateString(str: string) {
            return (str.length > 180) ? str.slice(0, 180 - 1) + '...' : str;
        },
    }
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import '../assets/styles/pages/swipecards.scss';
@import "@/assets/styles/components/buttons.scss";

.bounce-enter-active {
    animation: bounce-in .8s;
}

.bounce-leave-active {
    animation: bounce-in .8s reverse;
}

@keyframes bounce-in {
    0% {
        transform: scale(0);
    }

    50% {
        transform: scale(1.1);
    }

    100% {
        transform: scale(1);
    }
}
</style>