<script setup lang="ts">
import HelloWorld from '@/components/HelloWorld.vue'; // @ is an alias to /src
import {MDCTextField} from '@material/textfield';
import {MDCRipple} from '@material/ripple';
import { onMounted } from 'vue';

onMounted(() => { 
  const input = document.querySelector('.mdc-text-field');
  const btn = document.querySelector('.mdc-button');
  if(input){
    const textField = new MDCTextField(input);
  }
  if(btn){
    const buttonRipple = new MDCRipple(btn);
  }
});
</script>


<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js + TypeScript App"/>
    <div class="mdc-touch-target-wrapper">
      <button class="mdc-button mdc-button--raised">
        <span class="mdc-button__ripple"></span>
        <span class="mdc-button__touch"></span>
        <span class="mdc-button__label">My Accessible Button</span>
      </button>
    </div>
    <br>
    <br>
    <br>
    <label class="mdc-text-field mdc-text-field--outlined">
      <span class="mdc-notched-outline">
        <span class="mdc-notched-outline__leading"></span>
        <span class="mdc-notched-outline__notch">
          <span class="mdc-floating-label" id="my-label-id">Your Name</span>
        </span>
        <span class="mdc-notched-outline__trailing"></span>
      </span>
      <input type="text" class="mdc-text-field__input" aria-labelledby="my-label-id">
    </label>
  </div>
</template>

<style lang="scss" scoped>
  @use "@material/button/styles";
  @import "@/assets/styles/colors.scss";
  @import "@/assets/styles/input.scss";

</style>