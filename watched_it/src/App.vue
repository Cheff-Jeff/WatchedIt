<template>
  <Suspense>
    <!-- component with nested async dependencies -->
    <div v-auto-animate>
      <div :class="notifyType" class="notify" v-if="notify">
        <span>{{ msg }}</span>
      </div>
      <router-view 
        @download="downloadApp"
        @notify="notifyUser" 
      />
    </div>

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
      deferredPrompt: null as any,
      notify: false,
      notifyType: '',
      msg: ''
    }
  },
  created() {
    window.addEventListener('beforeinstallprompt', (e) => {
      // Prevent Chrome 67 and earlier from automatically showing the prompt
      e.preventDefault();
      // Stash the event so it can be triggered later.
      this.deferredPrompt = e;
    });
    // First we get the viewport height and we multiple it by 1% to get a value for a vh unit
    let vh = window.innerHeight * 0.01;
    // Then we set the value in the --vh custom property to the root of the document
    document.documentElement.style.setProperty('--vh', `${vh}px`);

    // We listen to the resize event
    window.addEventListener('resize', () => {
      // We execute the same script as before
      let vh = window.innerHeight * 0.01;
      document.documentElement.style.setProperty('--vh', `${vh}px`);
    });
  },
  methods: {
    async downloadApp(){
      if(this.deferredPrompt){
        console.log(this.deferredPrompt)
        this.deferredPrompt.prompt();
      }
      else{
        this.notifyUser('The app can not be installed. You possibly already have it installed.', 'warning')
      }
    },
    notifyUser(message: string, type: string){
      this.notify = true
      this.msg = message
      this.notifyType = type

      setTimeout(()=>{
        this.notify = false,
        this.msg = '',
        this.notifyType = ''
      }, 2000);
    }
  },
})
</script>