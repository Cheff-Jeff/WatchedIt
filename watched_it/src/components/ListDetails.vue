<script setup lang="ts">
import Details from './Title-Details.vue';
</script>


<template>
    <section v-auto-animate class="wrapper2" id="background">
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

            <div id="result-container" class="result-container">
                <div class="row">
                    <div class="col-6">
                        <img v-if="resultmovieshow.poster" class="result-img"
                            :src="'https://image.tmdb.org/t/p/w1280' + resultmovieshow.poster">
                        <img v-else class="tesult-img" src="../assets/stockBackground.png">
                    </div>
                    <div class="col-6 flex">
                        <div>
                            <p>{{ resultmovieshow.title }}</p>
                            <hr />
                            <span>{{ resultmovieshow.overview }}</span>
                        </div>
                        <div class="mdc-touch-target-wrapper btn-wrap white details-btn">
                            <button class="mdc-button mdc-button--raised"
                                @click="showDetails(resultmovieshow.id, resultmovieshow.movie)">
                                <span class="mdc-button__ripple"></span>
                                <span class="mdc-button__touch"></span>
                                <span class="mdc-button__label">View more</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-6">
                    <div class="users-container">
                        <p>Contributers:</p>
                        <div class="user">
                            <span v-for="item in contributersList" :key="item.id">{{ item.name }} </span>
                        </div>
                    </div>
                    <div id="add-friend" class="add-friend-btn">
                        <div class="mdc-touch-target-wrapper btn-wrap details-btn">
                            <button id="add-friend" class="mdc-button mdc-button--raised" v-on:click="togglePopup()">
                                <span class="mdc-button__ripple"></span>
                                <span class="mdc-button__touch"></span>
                                <span class="mdc-button__label">Add friend</span>
                            </button>
                        </div>
                    </div>
                </div>
                <div class="col-6">
                    <div class="dates">
                        <span>Add movie till: {{ currentList.addMovieDeadLine }}</span> <br />
                        <span>Vote deadline: {{ currentList.voteDeadLine }}</span> <br />
                        <span>Watch date: {{ currentList.watchDateTime }}</span>
                    </div>
                    <br />
                    <div class="mdc-touch-target-wrapper btn-wrap">
                        <button id="vote" class="mdc-button mdc-button--raised" v-on:click="openVotePopUp()">
                            <span class="mdc-button__ripple"></span>
                            <span class="mdc-button__touch"></span>
                            <span class="mdc-button__label">Vote</span>
                        </button>
                        <p class="error">{{ votedError }}</p>
                    </div>
                </div>
            </div>
        </div>

        <div class="movie-count">
            <span>movies/shows count: {{ currentList.itemCount }}</span>
        </div>

        <div class="movies-container">
            <div class="movie-info-box" v-for="item in movieshowdetails" :key="item.id">
                <div class="moviedetails">
                    <div class="row">
                        <div class="col-4">
                            <img v-if="item.poster" class="movie-img"
                                :src="'https://image.tmdb.org/t/p/w1280' + item.poster">
                            <img v-else class="movie-img" src="../assets/stockBackground.png">
                        </div>
                        <div class="col-8 flex">
                            <div class="movie-details">
                                <p>{{ item.title }}</p>
                                <span>{{ item.overview }}</span>
                            </div>
                            <div class="mdc-touch-target-wrapper btn-wrap white details-btn">
                                <button class="mdc-button mdc-button--raised" @click="showDetails(item.id, item.movie)">
                                    <span class="mdc-button__ripple"></span>
                                    <span class="mdc-button__touch"></span>
                                    <span class="mdc-button__label">View more</span>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div v-if="modalToggle" class="modal-wrap">
            <div class="modal-delete">
                <div class="head">
                    <h5>Add friend to list</h5>
                </div>
                <div class="body">
                    <div class="row user-wrapper" v-for="item in friendDetails" :key="item.id"
                        v-on:click="addfriendToList(item)">
                        <div class="col-3">
                            <span class="profile" v-html="avatar(item.name)" />
                        </div>
                        <div class="col-9">
                            <span>{{ item.name }}</span>
                        </div>
                    </div>
                </div>
                <div class="buttons">
                    <div class="mdc-touch-target-wrapper btn-wrap white" @click="togglePopup()">
                        <button class="mdc-button mdc-button--raised">
                            <span class="mdc-button__ripple"></span>
                            <span class="mdc-button__touch"></span>
                            <span class="mdc-button__label">Cancel</span>
                        </button>
                    </div>
                </div>
            </div>
        </div>



    </section>

    <section class="details" id="Overlay">
        <Details v-if="detailsToggle" :id="details.id.toString()" :banner="details.banner"
            :voteAvrage="Number(details.VoteAvrage)" :title="details.title" :director="details.director"
            :date="details.date" :genres="details.genres" :language="details.language" :poster="details.poster"
            :overview="details.overview" :providers="details.providers" :actors="details.actors"
            :title-type="details.titleType" @mounted="detailStyle()" @close="closeOverlay()" @rerender="reRender()" v-on:alert="alert"/>
    </section>

    <component :is="compToRender" v-on:alert="alert" v-on:closeCardSwipePopUp="closeCardSwipePopUp" :votelist="movieshowdetails"
        :movielistId="currentList.id">
    </component>
</template>


<script lang="ts">
import { defineComponent } from 'vue';
import { fetchMovie, fetchShow, getHasUserVoted, getMovieVotesResult, fetchFriends, addFriendToList } from '@/assets/javascript/api';
import CardSwipePopUp from './CardSwipePopUp.vue';
import { createAvatar } from '@dicebear/core';
import { avataaars } from '@dicebear/collection';
import { FriendToList } from '../assets/javascript/Models/ExternApiInterface';
import { genre, provider, actor } from '@/assets/javascript/Models/ExternApiInterface'

export default defineComponent({
    data() {
        return {
            compToRender: '',

            movieshowdetails: [] as any,

            contributersList: [] as any,

            friendDetails: [] as any,

            resultmovieshow: {
                id: 0 as number,
                movie: '',
                poster: '',
                title: '',
                overview: ''
            },

            votedError: '',
            modalToggle: false,


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
    components: {
        CardSwipePopUp
    },
    props: {
        currentList: [] as any,
    },
    async mounted() {
        await this.btnDisplayCheck();

        var date = new Date(this.currentList.voteDeadLine.split("-").reverse().join("-"))

        if (date < new Date()) {
            const result = await getMovieVotesResult(this.currentList.id);

            let resultitem = [] as any;

            if (result?.data.movie) {
                resultitem = await fetchMovie(result?.data.externId);
                this.resultmovieshow.movie = 'movie'

            } else {
                resultitem = await fetchShow(result?.data.externId);
                this.resultmovieshow.movie = 'tv'
            }

            this.resultmovieshow = resultitem._value.data

            const btn = (document.getElementById("vote") as HTMLButtonElement)!
            btn.style.display = 'none'
            this.votedError = ''

            const resultContainer = (document.getElementById("result-container") as HTMLButtonElement)!
            resultContainer.style.display = 'block'
        }

        this.contributersList = this.currentList.users;

        let details = [] as any;

        for (let i = 0; i < this.currentList.movies.length; i++) {
            if (this.currentList.movies[i].movie) {
                details = await fetchMovie(this.currentList.movies[i].externId);
                details.value.data.movie = "movie"
            } else {
                details = await fetchShow(this.currentList.movies[i].externId);
                details.value.data.movie = "tv"
            }

            details._value.data.overview = this.truncateString(details._value.data.overview)
            this.movieshowdetails.push(details._value.data)
        }
    },
    methods: {
        alert(alertitem: any){
            this.$emit('alert', alertitem);
        },
        openVotePopUp() {
            if (this.currentList.movies.length >= 1) {
                this.compToRender = 'CardSwipePopUp'
            } else {
                var alertitem: any = {
                    msg: 'No movie/show in list',
                    type: 'error'
                }
                this.$emit('alert', alertitem);
            }
        },
        async btnDisplayCheck() {
            if (this.currentList.userId != JSON.parse(localStorage.getItem('user') || '{}')) {
                const btn = (document.getElementById("add-friend") as HTMLButtonElement)!
                btn.style.display = 'none'
            }

            const result = await getHasUserVoted(this.currentList.id, JSON.parse(localStorage.getItem('user') || '{}'));

            if (result?.data == true) {
                const btn = (document.getElementById("vote") as HTMLButtonElement)!
                btn.disabled = true;
                btn.style.opacity = '0.4';

                this.votedError = 'you have already voted'
            }
        },
        truncateString(str: string) {
            return (str.length > 90) ? str.slice(0, 90 - 1) + '...' : str;
        },
        closeModal() {
            this.$emit('closeListDetails', true)
        },
        closeCardSwipePopUp() {
            var timer = null
            if (timer) {
                clearTimeout(timer);
                timer = null;
            }

            timer = setTimeout(async () => {
                this.compToRender = ''
                this.btnDisplayCheck();
            }, 500);
        },
        async togglePopup() {
            this.modalToggle = !this.modalToggle;

            const result = await fetchFriends(JSON.parse(localStorage.getItem('user') || '{}'));
            this.friendDetails = result.value!.data;

            const background = (document.getElementById("background") as HTMLButtonElement)!
            background.style.overflow = 'hidden'
            background.scrollTo(0, 0)

            if (this.modalToggle == false) {
                this.$forceUpdate();
                background.style.overflow = 'auto'
            }

        },
        async addfriendToList(item: any) {
            const friend: FriendToList = {
                MovieListId: this.currentList.id,
                phoneNumber: item.phone
            };

            const result = await addFriendToList(friend);

            if (result?.code == 200) {
                this.contributersList.push(item)

            } else {
                var alertitem: any = {
                    msg: 'Already in list',
                    type: 'error'
                }
                this.$emit('alert', alertitem);
            }
        },
        avatar(name: string | null) {
            let avatar: any = ''

            if (name) {
                avatar = createAvatar(avataaars, {
                    seed: name,
                    randomizeIds: true,
                    backgroundColor: ['5DE9F4']
                });
            }
            else {
                avatar = createAvatar(avataaars, {
                    randomizeIds: true,
                    backgroundColor: ['5DE9F4']
                });
            }
            return avatar.toString();
        },
        async showDetails(id: number, type: string) {
            const result = await fetchMovie(id.toString())
            if (result.value?.code == 200) {
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
        detailStyle() {
            if (this.detailsToggle) {
                window.scrollTo(0, 0)
                const overlay = document.getElementById('Overlay');
                this.pageHeight = overlay!.offsetHeight.toString() + "px"
            }
        },
        closeOverlay() {
            if (this.detailsToggle) {
                this.pageHeight = "none"
                this.detailsToggle = false
                setTimeout(() => {
                    window.scrollTo(0, this.scroll)
                }, 5);
            }
        },
        reRender() {
            this.detailsToggle = false
            this.$nextTick(() => {
                this.detailsToggle = true
            })
        }
    }
})
</script>

<style scoped lang="scss">
@import '../assets/styles/components/listdetails.scss';
@import "@/assets/styles/components/buttons.scss";
</style>