<script setup lang="ts">
import SimpleHeader from '@/components/SimpleHeader.vue';

import { MDCTextField } from '@material/textfield';
import { MDCRipple } from '@material/ripple';
import { onMounted } from 'vue';

onMounted(() => {
    const inputs = document.getElementsByClassName('mdc-text-field');
    const btn = document.querySelector('.mdc-button');
    if (inputs) {
        for (let input of inputs) {
            let textField = new MDCTextField(input);
        }
    }
    if (btn) {
        const buttonRipple = new MDCRipple(btn);
    }
});
</script>

<template>
    <SimpleHeader :currentTitle="'Watch list'" />

    <section class="wrapper">
        <div class="list-container">
            <div class="card" v-for="list in collectionList" :key="list.id" v-on:click="getClickedList(list)">
                <div class="movie-image-container">
                    <div class="folder-info-container">
                        <div class="folder-info">
                            <svg class="folder-icon" xmlns="http://www.w3.org/2000/svg"
                                viewBox="0 0 448 512"><!--! Font Awesome Pro 6.3.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                                <path
                                    d="M240 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H32c-17.7 0-32 14.3-32 32s14.3 32 32 32H176V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H384c17.7 0 32-14.3 32-32s-14.3-32-32-32H240V80z" />
                            </svg>
                            <p>{{ list.itemCount }}</p>
                        </div>
                    </div>

                    <div class="movie-image">
                        <svg xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 512 512"><!--! Font Awesome Pro 6.3.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                            <path
                                d="M64 480H448c35.3 0 64-28.7 64-64V160c0-35.3-28.7-64-64-64H288c-10.1 0-19.6-4.7-25.6-12.8L243.2 57.6C231.1 41.5 212.1 32 192 32H64C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64z" />
                        </svg>
                    </div>
                </div>
                <div class="info-box">
                    <h1>{{ list.title }}</h1>
                    <hr />
                    <p>WatchIt: {{ list.watchDateTime }}</p>
                </div>
            </div>
        </div>

        <div class="btn-stick-bottom">
            <div class="row">
                <div class="col-md-12">
                    <div class="mdc-touch-target-wrapper btn-wrap" v-on:click="togglePopup()">
                        <button class="mdc-button mdc-button--raised">
                            <span class="mdc-button__ripple"></span>
                            <span class="mdc-button__touch"></span>
                            <span class="mdc-button__label">Add list</span>
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <div v-if="modalToggle" class="modal-wrap">
            <div class="modal-delete">
                <div class="head">
                    <h5>Add list</h5>
                </div>
                <div class="body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="input-wrapper">
                                <label class="mdc-text-field mdc-text-field--outlined">
                                    <span class="mdc-notched-outline">
                                        <span class="mdc-notched-outline__leading"></span>
                                        <span class="mdc-notched-outline__notch">
                                            <span class="mdc-floating-label" id="my-label-id">Your Name</span>
                                        </span>
                                        <span class="mdc-notched-outline__trailing"></span>
                                    </span>
                                    <input type="text" class="mdc-text-field__input" aria-labelledby="my-label-id" required
                                        v-model="listDetails.title">
                                </label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="input-wrapper">
                                <label class="mdc-text-field mdc-text-field--outlined">
                                    <span class="mdc-notched-outline">
                                        <span class="mdc-notched-outline__leading"></span>
                                        <span class="mdc-notched-outline__notch">
                                            <span class="mdc-floating-label" id="my-label-id">Add movie/show till</span>
                                        </span>
                                        <span class="mdc-notched-outline__trailing"></span>
                                    </span>
                                    <input type="date" class="mdc-text-field__input" aria-labelledby="my-label-id" required
                                        v-model="listDetails.addmovieDate">
                                </label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="input-wrapper">
                                <label class="mdc-text-field mdc-text-field--outlined">
                                    <span class="mdc-notched-outline">
                                        <span class="mdc-notched-outline__leading"></span>
                                        <span class="mdc-notched-outline__notch">
                                            <span class="mdc-floating-label" id="my-label-id">Vote deadline</span>
                                        </span>
                                        <span class="mdc-notched-outline__trailing"></span>
                                    </span>
                                    <input type="date" class="mdc-text-field__input" aria-labelledby="my-label-id" required
                                        v-model="listDetails.voteDeadline">
                                </label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="input-wrapper">
                                <label class="mdc-text-field mdc-text-field--outlined">
                                    <span class="mdc-notched-outline">
                                        <span class="mdc-notched-outline__leading"></span>
                                        <span class="mdc-notched-outline__notch">
                                            <span class="mdc-floating-label" id="my-label-id">Watch time</span>
                                        </span>
                                        <span class="mdc-notched-outline__trailing"></span>
                                    </span>
                                    <input type="date" class="mdc-text-field__input" aria-labelledby="my-label-id" required
                                        v-model="listDetails.watchDate">
                                </label>
                            </div>
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
                    <div class="mdc-touch-target-wrapper btn-wrap">
                        <button class="mdc-button mdc-button--raised" v-on:click="submitNewList()">
                            <span class="mdc-button__ripple"></span>
                            <span class="mdc-button__touch"></span>
                            <span class="mdc-button__label">Save</span>
                        </button>
                    </div>
                </div>
            </div>
        </div>

    </section>

    <component :is="compToRender" v-on:closeListDetails="closeListDetails" :currentList="getCurrentList"></component>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { getCollectionList, addNewList } from '../assets/javascript/api';
import ListDetails from '@/components/ListDetails.vue';
import { newList } from '../assets/javascript/Models/ExternApiInterface';

export default defineComponent({
    data() {
        return {
            compToRender: '',
            collectionList: [] as any,

            getCurrentList: [] as any,

            modalToggle: false,

            listDetails: {
                title: '',
                addmovieDate: '',
                voteDeadline: '',
                watchDate: ''
            }
        }
    },
    components: {
        ListDetails
    },
    async mounted() {
        this.collectionList = await getCollectionList(JSON.parse(localStorage.getItem('user') || '{}'));
    },
    methods: {
        getClickedList(list: []) {
            this.getCurrentList = list;

            this.compToRender = 'ListDetails'
        },
        closeListDetails() {
            this.compToRender = ''
        },
        togglePopup() {
            this.modalToggle = !this.modalToggle;
        },
        async submitNewList() {
            
            const newList: newList = {
                title: this.listDetails.title,
                userId: JSON.parse(localStorage.getItem('user') || '{}'),
                addMovieDeadLine: this.convertStringToLocaleTime(this.listDetails.addmovieDate),
                voteDeadLine: this.convertStringToLocaleTime(this.listDetails.voteDeadline),
                watchDateTime: this.convertStringToLocaleTime(this.listDetails.watchDate)
            }

            const result = await addNewList(newList)

            if (result?.code == 200) {
                window.location.reload()
            }
            else {
                //melding er ging iets mis
            }
        },
        convertStringToLocaleTime(input: string) {
            return input.split("-").reverse().join("-");
        }
    }
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import "@/assets/styles/components/buttons.scss";
@import "@/assets/styles/components/input.scss";
@import "@/assets/styles/pages/list.scss";
</style>