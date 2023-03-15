<script setup lang="ts">
import {MDCTextField} from '@material/textfield';
import {MDCRipple} from '@material/ripple';
import { onMounted } from 'vue';
onMounted(() => { 
  const inputs = document.getElementsByClassName('.mdc-text-field');
  const btn = document.querySelector('.mdc-button');
  if(inputs){
    for(let input of inputs){
      new MDCTextField(input);
    }
  }
  if(btn){
    const buttonRipple = new MDCRipple(btn);
  }
});
</script>

<template>
  <section class="login">
    <div class="comtainer">
      <div class="row">
        <div class="col-md-12">
          <div class="logo-wrapper">
            <img src="" alt="logo">
          </div>
          <div class="title-wrap">
            <h1>Sign in</h1>
          </div>
        </div>
        <div class="col-md-12">
          <div class="input-wrapper">
            <label class="mdc-text-field mdc-text-field--outlined">
              <span class="mdc-notched-outline">
                <span class="mdc-notched-outline__leading"></span>
                <span class="mdc-notched-outline__notch">
                  <span class="mdc-floating-label" id="my-label-id">Email</span>
                </span>
                <span class="mdc-notched-outline__trailing"></span>
              </span>
              <input type="email" class="mdc-text-field__input" aria-labelledby="my-label-id">
            </label>
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
              <input type="password" class="mdc-text-field__input" aria-labelledby="my-label-id">
            </label>
          </div>
        </div>
        <div class="col-md-12">
          <div class="mdc-touch-target-wrapper">
            <button class="mdc-button mdc-button--raised">
              <span class="mdc-button__ripple"></span>
              <span class="mdc-button__touch"></span>
              <span class="mdc-button__label">Sign in</span>
            </button>
          </div>
        </div>
        <div class="col-md-12">
          <div class="link-wrapper">
            <a href="#">Sign up</a>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style lang="scss" scoped>
@use "@material/button/styles";
@import "@/assets/styles/colors.scss";
@import "@/assets/styles/input.scss";
</style>