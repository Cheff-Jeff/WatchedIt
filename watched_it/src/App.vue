<template>
  <Suspense>
    <!-- component with nested async dependencies -->
    <router-view @download="downloadApp" />

    <!-- loading state via #fallback slot -->
    <template #fallback>
      Loading...
    </template>
  </Suspense>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  data() {
    return {
      deferredPrompt: null as any
    }
  },
  created() {
    window.addEventListener('beforeinstallprompt', (e) => {
      // Prevent Chrome 67 and earlier from automatically showing the prompt
      e.preventDefault();
      // Stash the event so it can be triggered later.
      this.deferredPrompt = e;

      this.deferredPrompt.prompt();
    });
  },
  methods: {
    async downloadApp(){
      if(this.deferredPrompt){
        this.deferredPrompt.prompt();
      }
    }
  },
})
</script>