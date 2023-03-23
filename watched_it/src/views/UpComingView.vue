<template>
<pre>
  {{ movies }}
</pre>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { fetchCommingSoon } from '@/assets/javascript/api';
import { TrendingMovie } from '@/assets/javascript/Models/ExternApiInterface';

export default defineComponent({
  data() {
    return {
      pages: 1,
      totalPages: 1,
      movies: [] as TrendingMovie[]
    }
  },
  async beforeMount() {
    const response = await fetchCommingSoon(this.pages.toString())
    if(response.value?.code == 200){
      this.movies = response.value.data
      this.totalPages = response.value.pages
    }
  },
  created () {
    window.addEventListener('scroll', this.scrollCheck);
  },
  unmounted () {
    window.removeEventListener('scroll', this.scrollCheck);
  },
  methods: {
    async nextPage(){
      if(this.pages <= this.totalPages){
        const response = await fetchCommingSoon(this.pages.toString())
        if(response.value?.code == 200){
          const newMovies: TrendingMovie[] = response.value.data
          for(let i = 0; i < newMovies.length; i++){
            this.movies.push(newMovies[i]);
          }
        }
      }
    },
    scrollCheck(event: any) {
      console.log("scroll")
      if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
        console.log('bottom!')
        this.nextPage()
        this.pages++
      }
    }
  },
})
</script>