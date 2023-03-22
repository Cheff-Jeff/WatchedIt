<script setup lang="ts">
import SimpleHeader from '@/components/SimpleHeader.vue';
</script>

<template>
    <SimpleHeader :currentTitle="'Watch list'" />

    <section class="wrapper">

        <div class="list-container">
            <div class="card">
                <div class="movie-image-container">

                    <div class="folder-info-container">
                        <div class="folder-info">
                            <svg class="folder-icon" xmlns="http://www.w3.org/2000/svg"
                                viewBox="0 0 448 512"><!--! Font Awesome Pro 6.3.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                                <path
                                    d="M240 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H32c-17.7 0-32 14.3-32 32s14.3 32 32 32H176V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H384c17.7 0 32-14.3 32-32s-14.3-32-32-32H240V80z" />
                            </svg>
                            <p>5</p>
                        </div>
                    </div>

                    <div class="movie-image">
                        <svg xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 576 512"><!--! Font Awesome Pro 6.3.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                            <path
                                d="M384 480h48c11.4 0 21.9-6 27.6-15.9l112-192c5.8-9.9 5.8-22.1 .1-32.1S555.5 224 544 224H144c-11.4 0-21.9 6-27.6 15.9L48 357.1V96c0-8.8 7.2-16 16-16H181.5c4.2 0 8.3 1.7 11.3 4.7l26.5 26.5c21 21 49.5 32.8 79.2 32.8H416c8.8 0 16 7.2 16 16v32h48V160c0-35.3-28.7-64-64-64H298.5c-17 0-33.3-6.7-45.3-18.7L226.7 50.7c-12-12-28.3-18.7-45.3-18.7H64C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64H87.7 384z" />
                        </svg>
                    </div>
                </div>
                <div class="info-box">
                    <h1>Test</h1>
                    <hr />
                    <p>deadline: 19/02/2023 (19:00)</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">

                    <div class="folder-info-container">
                        <div class="folder-info">
                            <svg class="folder-icon" xmlns="http://www.w3.org/2000/svg"
                                viewBox="0 0 448 512"><!--! Font Awesome Pro 6.3.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                                <path
                                    d="M240 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H32c-17.7 0-32 14.3-32 32s14.3 32 32 32H176V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H384c17.7 0 32-14.3 32-32s-14.3-32-32-32H240V80z" />
                            </svg>
                            <p>5</p>
                        </div>
                    </div>

                    <img class="movie-image" src="../assets/stockBackground.png">
                </div>
                <div class="info-box">
                    <h1>Test</h1>
                    <hr />
                    <p>test123456778</p>
                </div>
            </div>
            <div class="card">
                <div class="movie-image-container">

                    <div class="folder-info-container">
                        <div class="folder-info">
                            <svg class="folder-icon" xmlns="http://www.w3.org/2000/svg"
                                viewBox="0 0 448 512"><!--! Font Awesome Pro 6.3.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                                <path
                                    d="M240 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H32c-17.7 0-32 14.3-32 32s14.3 32 32 32H176V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H384c17.7 0 32-14.3 32-32s-14.3-32-32-32H240V80z" />
                            </svg>
                            <p>5</p>
                        </div>
                    </div>

                    <img class="movie-image" src="../assets/stockBackground.png">
                </div>
                <div class="info-box">
                    <h1>Test</h1>
                    <hr />
                    <p>test123456778</p>
                </div>
            </div>
        </div>

    </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
export default defineComponent({
    data() {
        return {

        }
    },
    methods: {

    }
})
</script>

<style lang="scss" scoped>@import "@/assets/styles/colors.scss";
@import '../assets/styles/pages/list.scss';</style>