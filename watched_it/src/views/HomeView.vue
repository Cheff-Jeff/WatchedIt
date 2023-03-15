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
    
    <br>
    <br>
    <br>
  </div>
</template>

<style lang="scss" scoped>
  @use "@material/button/styles";
  @import "@/assets/styles/colors.scss";
  @import "@/assets/styles/input.scss";

</style>