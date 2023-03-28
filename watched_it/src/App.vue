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
      console.log('test')
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