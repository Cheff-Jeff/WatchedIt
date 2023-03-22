<script setup lang="ts">
import { defineProps } from 'vue';

const props = defineProps({
    currentTitle: {
        type: String,
        required: true
    }
})
</script>

<template>
    <div class="header-container">

        <div class="icon-left" @click="openMenu">
            <svg xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 448 512"><!--! Font Awesome Pro 6.3.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                <path
                    d="M0 96C0 78.3 14.3 64 32 64H416c17.7 0 32 14.3 32 32s-14.3 32-32 32H32C14.3 128 0 113.7 0 96zM0 256c0-17.7 14.3-32 32-32H416c17.7 0 32 14.3 32 32s-14.3 32-32 32H32c-17.7 0-32-14.3-32-32zM448 416c0 17.7-14.3 32-32 32H32c-17.7 0-32-14.3-32-32s14.3-32 32-32H416c17.7 0 32 14.3 32 32z" />
            </svg>
        </div>

        <div class="header-text">
            <h1>{{ props.currentTitle }}</h1>
        </div>

        <input class="searchbox" type="text" placeholder="movie/show" v-model="searchPhrase" @keyup="getSearchedItems"
            @blur="getSearchedItems">

        <div class="result-container">
            <ul>
                <li v-for="result in searchResult" :key="result.id">
                    <router-link :to="{ name: 'details', params: { type: `${result.media_type}`, id: result.id } }">
                            {{ result.title }}
                    </router-link>
                </li>
            </ul>
        </div>

        <div class="icon-right">
            <svg xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 512 512"><!--! Font Awesome Pro 6.3.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                <path
                    d="M416 208c0 45.9-14.9 88.3-40 122.7L502.6 457.4c12.5 12.5 12.5 32.8 0 45.3s-32.8 12.5-45.3 0L330.7 376c-34.4 25.2-76.8 40-122.7 40C93.1 416 0 322.9 0 208S93.1 0 208 0S416 93.1 416 208zM208 352a144 144 0 1 0 0-288 144 144 0 1 0 0 288z" />
            </svg>
        </div>
    </div>

    <div :class="menuToggle" class="menu-wrapper" @click="closeMenu">
        <div class="menu" @click.stop>
            <div class="close-wrapper" @click="closeMenu">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 320 512">
                    <path
                        d="M310.6 150.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L160 210.7 54.6 105.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L114.7 256 9.4 361.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0L160 301.3 265.4 406.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L205.3 256 310.6 150.6z" />
                </svg>
            </div>
            <div class="inner-wrap">
                <div class="logo-wrap">
                    <img src="@/assets/logo.png" alt="logo">
                </div>
                <nav>
                    <ul>
                        <li>
                            <router-link :to="{ name: 'account' }">
                                Account
                            </router-link>
                        </li>
                        <li>
                            <router-link :to="{ name: 'trending' }">
                                Trending
                            </router-link>
                        </li>
                        <li>
                            <router-link :to="{ name: 'watchlist' }">
                                Watch list
                            </router-link>
                        </li>
                        <li>
                            <router-link to="/test">
                                Comig soon
                            </router-link>
                        </li>
                    </ul>
                </nav>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { searchMovies } from '@/assets/javascript/api';
import { defineComponent } from 'vue';
export default defineComponent({
    data() {
        return {
            menuToggle: '',
            searchPhrase: '',

            searchResult: [] as any,
        }
    },
    methods: {
        async getSearchedItems() {
            if (this.searchPhrase.length >= 1) {
                this.searchResult = await searchMovies(this.searchPhrase);
            }
        },
        closeMenu() {
            this.menuToggle = ''
        },
        openMenu() {
            this.menuToggle = 'open'
        }
    },
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import '@/assets/styles/components/simpleheader.scss';
</style>