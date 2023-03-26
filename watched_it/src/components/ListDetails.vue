<script setup lang="ts">

</script>


<template>
    <section class="wrapper2">
        <div class="top-info-box">
            <div class="close-icon">
                <svg v-on:click="closeModal()" class="icon-right" xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 448 512"><!--! Font Awesome Pro 6.3.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                    <path
                        d="M9.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.2 288 416 288c17.7 0 32-14.3 32-32s-14.3-32-32-32l-306.7 0L214.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z" />
                </svg>

                <div class="title">
                    <p>{{ currentList.title }}</p>
                </div>

                <svg v-on:click="closeModal()" class="icon-left" xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 320 512"><!--! Font Awesome Pro 6.3.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                    <path
                        d="M310.6 150.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L160 210.7 54.6 105.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L114.7 256 9.4 361.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0L160 301.3 265.4 406.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L205.3 256 310.6 150.6z" />
                </svg>
            </div>
            <hr />


            <div class="row">
                <div class="col-7">
                    <div class="users-container">
                        <p>Contributers:</p>
                        <div class="user">
                            <span v-for="item in userdetails" :key="item.id">{{ item.name }} </span>
                        </div>
                    </div>
                </div>
                <div class="col-5">
                    <div class="dates">
                        <span>Vote deadline: {{ currentList.voteDeadLine }}</span> <br />
                        <span>Watch date: {{ currentList.watchDateTime }}</span>
                    </div>
                </div>
            </div>



            <div class="swipe-btn">
                <div class="row">
                    <div class="col-7">

                    </div>
                    <div class="col-5">
                        <div class="mdc-touch-target-wrapper btn-wrap">
                            <button id="vote" class="mdc-button mdc-button--raised" v-on:click="openVotePopUp()">
                                <span class="mdc-button__ripple"></span>
                                <span class="mdc-button__touch"></span>
                                <span class="mdc-button__label">Vote</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="movie-count">
                <span>Amount of movies: {{ currentList.itemCount }}</span>
            </div>
        </div>

        <div class="movie-info-box" v-for="item in movieshowdetails" :key="item.id">
            <div class="moviedetails">
                <div class="row">
                    <div class="col-2">
                        <img v-if="item.poster" class="movie-img" :src="'https://image.tmdb.org/t/p/w1280' + item.poster">
                        <img v-else class="movie-img" src="../assets/stockBackground.png">
                    </div>
                    <div class="col-10">
                        <div class="movie-details">
                            <p>{{ item.title }}</p>
                            <span>{{ item.overview }}</span>
                        </div>
                    </div>
                </div>

                <div class="details-btn-container">
                    <div class="details-btn">
                        <div class="mdc-touch-target-wrapper btn-wrap white">
                            <button class="mdc-button mdc-button--raised">
                                <span class="mdc-button__ripple"></span>
                                <span class="mdc-button__touch"></span>
                                <span class="mdc-button__label">View more</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <component :is="compToRender" v-on:closeCardSwipePopUp="closeCardSwipePopUp" :votelist="movieshowdetails"
        :movielistId="currentList.id">
    </component>
</template>


<script lang="ts">
import { defineComponent } from 'vue';
import { fetchMovie, fetchShow, fetchUser, getHasUserVoted } from '@/assets/javascript/api';
import CardSwipePopUp from './CardSwipePopUp.vue';

export default defineComponent({
    data() {
        return {
            compToRender: '',

            movieshowdetails: [] as any,

            userdetails: [] as any,
        }
    },
    components: {
        CardSwipePopUp
    },
    emits: [
        "closeListDetails"
    ],
    props: {
        currentList: [] as any,
    },
    async mounted() {
        await this.btnDisplayCheck();

        this.userdetails = this.currentList.users;
        this.userdetails.splice(0, 1);
        let user = await fetchUser(JSON.parse(localStorage.getItem('user') || '{}'));
        this.userdetails.push(user.value?.data);

        let details = [] as any;

        for (let i = 0; i < this.currentList.movies.length; i++) {
            if (this.currentList.movies[i].movie) {
                details = await fetchMovie(this.currentList.movies[i].externId);
            } else {
                details = await fetchShow(this.currentList.movies[i].externId);
            }

            //movieId geven aan voteaverige, wist niet hoe beter kon.
            details._value.data.vote_average = this.currentList.movies[i].id

            details._value.data.overview = this.truncateString(details._value.data.overview)
            this.movieshowdetails.push(details._value.data)
        }
    },
    methods: {
        openVotePopUp() {
            if (this.currentList.movies.length >= 1) {
                this.compToRender = 'CardSwipePopUp'
            } else {
                //melding dat er niks is om te voten
            }
        },
        async btnDisplayCheck() {
            const result = await getHasUserVoted(this.currentList.id, JSON.parse(localStorage.getItem('user') || '{}'));

            if (this.currentList.voteDeadLine > new Date().toLocaleString() || result?.data == true) {
                const btn = (document.getElementById("vote") as HTMLButtonElement)!
                btn.disabled = true;
                btn.style.opacity = '0.4';
            }
        },
        truncateString(str: string) {
            return (str.length > 145) ? str.slice(0, 145 - 1) + '...' : str;
        },
        closeModal() {
            this.$emit('closeListDetails', true)
        },
        closeCardSwipePopUp() {
            this.compToRender = ''

            this.btnDisplayCheck();
        }
    }
})
</script>

<style scoped lang="scss">
@import '../assets/styles/components/listdetails.scss';
@import "@/assets/styles/components/buttons.scss";
</style>