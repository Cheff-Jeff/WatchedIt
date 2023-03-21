<script setup lang="ts">
  import { fetchFriends } from '@/assets/javascript/api';
  import { Friend } from '@/assets/javascript/Models/UserInterface';
  import SimpleHeader from '@/components/SimpleHeader.vue';
  const id: string | null = localStorage.getItem('user')
  let friends: Friend[] | undefined
  
  if(id){
    const result = await fetchFriends(id)
    friends = result.value?.data
  }
</script>

<template>
  <SimpleHeader current-title="Friends"/>
  <div class="wrapper">
    <section class="top-bar pt-5">
      <div class="comtianer">
        <p>
          back
        </p>
      </div>
    </section>
    <section class="friends" v-if="friends">
      <div class="container">
        <div class="user-wrapper" v-for="friend in friends" :key="friend.id">
          <span class="w-50 d-block profile" v-html="avatar(friend.name)"/>
          <div class="info">
            <p>{{ friend.name }}</p>
            <p>{{ friend.phone }}</p>
          </div>
          <div class="icon-wrap">

          </div>
        </div>
      </div>
    </section>
    <section class="newFriend">
      <div class="container">
        <p>btn</p>
      </div>
    </section>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { createAvatar } from '@dicebear/core';
import { avataaars } from '@dicebear/collection';

export default defineComponent({
  methods: {
    avatar(name: string | null){
      let avatar: any = ''
      
      if(name){
        avatar = createAvatar(avataaars, {
          seed: name,
          randomizeIds: true
        });
      }
      
      if(!name){
        avatar = createAvatar(avataaars, {
          randomizeIds: true
        });
      }
      return avatar.toString();
    }
  },
})
</script>