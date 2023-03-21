<script setup lang="ts">
import { fetchUser } from '@/assets/javascript/api';
import { User } from '@/assets/javascript/Models/UserInterface';
import SimpleHeader from '@/components/SimpleHeader.vue';
import {MDCRipple} from '@material/ripple';
import { onMounted, defineExpose } from 'vue';
const id: string | null = localStorage.getItem('user')
let user: User | undefined

if(id){
    const result = await fetchUser(id)
    console.log(result)
    user = result.value?.data
  }

onMounted(() => { 
  const buttons = document.getElementsByClassName('mdc-button');
  if(buttons){
    for(let btn of buttons){
      const buttonRipple = new MDCRipple(btn);
    }
  }
});

defineExpose({user})
</script>

<template>
  <SimpleHeader currentTitle="Account" />
  <section class="section-pagewrap" >
    <div class="container">
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
              <button class="mdc-button mdc-button--raised">
                <span class="mdc-button__ripple"></span>
                <span class="mdc-button__touch"></span>
                <span class="mdc-button__label">Edit account</span>
              </button>
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
              <router-link :to="{name: 'signUp'}" class="link">
                Recomandations
              </router-link>
            </div>
          </div>
          <div class="col-5">
            <div class="link-wrapper">
              <router-link :to="{name: 'signUp'}" class="link">
                Watch lists
              </router-link>
            </div>
          </div>
          <div class="col-5">
            <div class="link-wrapper">
              <router-link :to="{name: 'friends'}" class="link">
                Friends
              </router-link>
            </div>
          </div>
          <div class="col-5">
            <div class="link-wrapper">
              <router-link :to="{name: 'signUp'}" class="link">
                Favorites
              </router-link>
            </div>
          </div>
        </div>
      </section>
      <section class="movies">
        <div class="section-title">
            <h1>Movies for you</h1>
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
      <section class="shows">
        <div class="section-title">
            <h1>Shows for you</h1>
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
    </div>
  </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  methods: {
    download(){
      this.$emit('download')
    }
  },
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import "@/assets/styles/components/buttons.scss";
@import "@/assets/styles/pages/account.scss";
@import "@/assets/styles/components/cards.scss";
</style>