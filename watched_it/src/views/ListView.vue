<script setup lang="ts">
import SimpleHeader from '@/components/SimpleHeader.vue';
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
            <div class="col-md-12">
                <div class="mdc-touch-target-wrapper btn-wrap">
                    <button class="mdc-button mdc-button--raised">
                        <span class="mdc-button__ripple"></span>
                        <span class="mdc-button__touch"></span>
                        <span class="mdc-button__label">Add list</span>
                    </button>
                </div>
            </div>
        </div>
    </section>

    <component :is="compToRender" v-on:closeListDetails="closeListDetails" :currentList="getCurrentList"></component>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { getCollectionList } from '../assets/javascript/api';
import ListDetails from '@/components/ListDetails.vue';
export default defineComponent({
    data() {
        return {
            compToRender: '',
            collectionList: [] as any,

            getCurrentList: [] as any,
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
    }
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import '../assets/styles/pages/list.scss';
@import "@/assets/styles/components/buttons.scss";
</style>