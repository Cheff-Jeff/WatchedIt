<script setup lang="ts">
import {MDCTextField} from '@material/textfield';
import {MDCRipple} from '@material/ripple';
import { onMounted } from 'vue';

onMounted(() => { 
  const inputs = document.getElementsByClassName('mdc-text-field');
  const btn = document.querySelector('.mdc-button');
  if(inputs){
    for(let input of inputs){
      let textField = new MDCTextField(input);
    }
  }
  if(btn){
    const buttonRipple = new MDCRipple(btn);
  }
});
</script>

<template>
  <section class="login">
    <div class="container" v-auto-animate>
      <div class="row">
        <div class="col-md-12">
          <div class="logo-wrapper">
            <img src="@/assets/logo.png" alt="logo">
          </div>
          <div class="title-wrap">
            <h1>Sign in</h1>
          </div>
        </div>
        <form @submit.prevent="submit">
          <div class="col-md-12">
            <div class="input-wrapper">
              <label :class="errEmailClass" class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                  <span class="mdc-notched-outline__leading"></span>
                  <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="my-label-id">Email</span>
                  </span>
                  <span class="mdc-notched-outline__trailing"></span>
                </span>
                <input 
                  type="email" 
                  class="mdc-text-field__input" 
                  required 
                  aria-labelledby="my-label-id"
                  v-model="email">
              </label>
              <div class="mdc-text-field-helper-line">
                <div class="mdc-text-field-helper-text mdc-text-field-helper-text--validation-msg">
                  {{ emailError }}
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-12">
            <div class="input-wrapper">
              <label class="mdc-text-field mdc-text-field--outlined">
                <span class="mdc-notched-outline">
                  <span class="mdc-notched-outline__leading"></span>
                  <span class="mdc-notched-outline__notch">
                    <span class="mdc-floating-label" id="my-label-id">Password</span>
                  </span>
                  <span class="mdc-notched-outline__trailing"></span>
                </span>
                <input 
                  type="password" 
                  class="mdc-text-field__input" 
                  required 
                  aria-labelledby="my-label-id"
                  v-model="password">
              </label>
              <div class="mdc-text-field-helper-line">
                <div class="mdc-text-field-helper-text mdc-text-field-helper-text--validation-msg">
                  {{ passwordError }}
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-12" v-if="error">
            <span class="small error">
              {{ error }}
            </span>
          </div>
          <div class="col-md-12">
            <div class="mdc-touch-target-wrapper btn-wrap">
              <button class="mdc-button mdc-button--raised">
                <span class="mdc-button__ripple"></span>
                <span class="mdc-button__touch"></span>
                <span class="mdc-button__label loading-icon" v-if="loading">
                  <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
                    <circle cx="50" cy="50" r="32" stroke-width="8" stroke="#fff" stroke-dasharray="50.26548245743669 50.26548245743669" fill="none" stroke-linecap="round">
                      <animateTransform attributeName="transform" type="rotate" dur="1s" repeatCount="indefinite" keyTimes="0;1" values="0 50 50;360 50 50"></animateTransform>
                    </circle>
                    <circle cx="50" cy="50" r="23" stroke-width="8" stroke="#fff" stroke-dasharray="36.12831551628262 36.12831551628262" stroke-dashoffset="36.12831551628262" fill="none" stroke-linecap="round">
                      <animateTransform attributeName="transform" type="rotate" dur="1s" repeatCount="indefinite" keyTimes="0;1" values="0 50 50;-360 50 50"></animateTransform>
                    </circle>
                  </svg>
                </span>
                <span v-else class="mdc-button__label">Sign in</span>
              </button>
            </div>
          </div>
        </form>
        <div class="col-md-12">
          <div class="link-wrapper">
            <router-link :to="{name: 'signUp'}" class="link">
              Sign up
            </router-link>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { validateEmail, errEmailEmp, errEmail, errPassEmp } from '@/assets/javascript/validation';
import { signIn } from '@/assets/javascript/api';
import { User } from '@/assets/javascript/Models/UserInterface';

export default defineComponent({
  data() {
    return {
      email: '',
      emailError: 'Invalid email.',
      errEmailClass: '',
      password: '',
      passwordError: 'Invalid password',
      errPasswordClass: '',
      error: '',
      loading: false
    }
  },
  methods: {
    async submit(){
      if(!this.loading){
        this.loading = true
        this.checkEmail()
        this.checkPassword()
        if(this.emailError == '', this.passwordError == ''){
          const user: User = {
            id: null,
            name: null,
            email: this.email,
            password: this.password,
            phone: null
          }

          const result = await signIn(user)

          if(result?.code == 200 && result.data.id){
            this.loading = false
            localStorage.setItem('user', result.data.id?.toString())
            this.$router.push('account');
          }
          else{
            this.loading = false
            this.error = "Something went wrong. Please try again."
          }
        }
      }
    },
    checkEmail(){
      this.emailError = this.email.length == 0 ? errEmailEmp() : 
      (validateEmail(this.email) ? '' : errEmail(this.email))
      if(this.emailError){
        this.errEmailClass = 'field-error'
      }
      else{
        this.errEmailClass = ''
      }
    },
    checkPassword(){
      this.passwordError = this.password.length == 0 ? errPassEmp(): ''
    }
  },
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import "@/assets/styles/components/buttons.scss";
@import "@/assets/styles/components/input.scss";
@import "@/assets/styles/pages/login.scss";
</style>