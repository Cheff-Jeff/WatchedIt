<script setup lang="ts">
import SimpleHeader from '@/components/SimpleHeader.vue';
import {MDCRipple} from '@material/ripple';
import { onMounted } from 'vue';
onMounted(() => { 
  const buttons = document.getElementsByClassName('mdc-button');
  if(buttons){
    for(let btn of buttons){
      const buttonRipple = new MDCRipple(btn);
    }
  }
});
</script>

<template>
  <SimpleHeader currentTitle="Error 404" />
  <div class="wrapper">
    <div class="container">
      <section class="content">
        <div class="img-wrapper">
          <div class="hover-box">
            <h1>You seem to be lost.</h1>
            <p>When in doubt, always follow your nose.</p>
          </div>
          <img src="@/assets/adventure.gif">
        </div>
        <div class="btn-wrap">
          <div class="mdc-touch-target-wrapper btn-wrap">
            <button class="mdc-button mdc-button--raised" @click="nav">
              <span class="mdc-button__ripple"></span>
              <span class="mdc-button__touch"></span>
              <span class="mdc-button__label">Go home.</span>
            </button>
          </div>
        </div>
      </section>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
export default defineComponent({
  methods: {
    nav(){
      this.$router.push('account')
    }
  },
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import "@/assets/styles/components/buttons.scss";
@import "@/assets/styles/pages/not-fount.scss";
</style>