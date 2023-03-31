<script setup lang="ts">
  import { fetchFriends } from '@/assets/javascript/api';
  import { Friend } from '@/assets/javascript/Models/UserInterface';
  import SimpleHeader from '@/components/SimpleHeader.vue';
  import {MDCRipple} from '@material/ripple';
  import { onMounted } from 'vue';
  const id: string | null = localStorage.getItem('user')
  let friends: Friend[] | undefined
  
  if(id){
    const result = await fetchFriends(id)
    friends = result.value?.data
  }

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
  <SimpleHeader current-title="Friends"/>
  <div class="wrapper">
    <section class="top-bar">
      <div class="container">
        <div class="back" @click="()=>{$router.back()}">
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512">
            <path d="M9.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.2 288 416 288c17.7 0 32-14.3 32-32s-14.3-32-32-32l-306.7 0L214.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z"/>
          </svg>
          <p>Back</p>
        </div>
      </div>
    </section>
    <section class="friends" v-if="friends && friends?.length > 0">
      <div class="container">
        <span class="small error" v-if="err">
          {{ err }}
        </span>
        <div class="user-wrapper" v-for="friend in friends" :key="friend.id">
          <span class="profile" v-html="avatar(friend.name)"/>
          <div class="info">
            <p>{{ friend.name }}</p>
            <span>{{ friend.phone }}</span>
          </div>
          <div class="icon-wrap" @click="togglePopup(friend.id?.toString(), friend.name)">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512">
              <path d="M170.5 51.6L151.5 80h145l-19-28.4c-1.5-2.2-4-3.6-6.7-3.6H177.1c-2.7 0-5.2 1.3-6.7 3.6zm147-26.6L354.2 80H368h48 8c13.3 0 24 10.7 24 24s-10.7 24-24 24h-8V432c0 44.2-35.8 80-80 80H112c-44.2 0-80-35.8-80-80V128H24c-13.3 0-24-10.7-24-24S10.7 80 24 80h8H80 93.8l36.7-55.1C140.9 9.4 158.4 0 177.1 0h93.7c18.7 0 36.2 9.4 46.6 24.9zM80 128V432c0 17.7 14.3 32 32 32H336c17.7 0 32-14.3 32-32V128H80zm80 64V400c0 8.8-7.2 16-16 16s-16-7.2-16-16V192c0-8.8 7.2-16 16-16s16 7.2 16 16zm80 0V400c0 8.8-7.2 16-16 16s-16-7.2-16-16V192c0-8.8 7.2-16 16-16s16 7.2 16 16zm80 0V400c0 8.8-7.2 16-16 16s-16-7.2-16-16V192c0-8.8 7.2-16 16-16s16 7.2 16 16z"/>
            </svg>
          </div>
        </div>
      </div>
    </section>
    <section class="fallback" v-else>
      <div class="container">
        <div class="img-wrap">
          <img src="@/assets/gollum-friends.gif">
        </div>
      </div>
    </section>
    <section class="newFriend">
      <div class="container">
        <div class="mdc-touch-target-wrapper btn-wrap">
          <button class="mdc-button mdc-button--raised" @click="contactPicker">
            <span class="mdc-button__ripple"></span>
            <span class="mdc-button__touch"></span>
            <span class="mdc-button__label">Add new friend</span>
          </button>
        </div>
      </div>
    </section>
  </div>
  <div class="modal-wrap" :class="modalToggle" @click="togglePopup('','')">
    <div class="modal-delete" @click.stop>
      <div class="head">
        <h5>Delete friend</h5>
      </div>
      <div class="body">
        <p>You are about to delete friend: {{ deleteName }}</p>
        <p>Are you sure?</p>
      </div>
      <div class="buttons">
        <div class="mdc-touch-target-wrapper btn-wrap white">
          <button class="mdc-button mdc-button--raised" @click="togglePopup('','')">
            <span class="mdc-button__ripple"></span>
            <span class="mdc-button__touch"></span>
            <span class="mdc-button__label">Cancel</span>
          </button>
        </div>
        <div class="mdc-touch-target-wrapper btn-wrap red">
          <button class="mdc-button mdc-button--raised" @click="deleteFriendShip">
            <span class="mdc-button__ripple"></span>
            <span class="mdc-button__touch"></span>
            <span class="mdc-button__label">Delete</span>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { createAvatar } from '@dicebear/core';
import { avataaars } from '@dicebear/collection';
import {NavigatorWithContacts} from '@/assets/javascript/Models/Navigator';
import { addFriend, deleteFriend } from '@/assets/javascript/api';
import { User } from '@/assets/javascript/Models/UserInterface';

export default defineComponent({
  data() {
    return {
      err: '',
      deleteId: '',
      deleteName: '',
      modalToggle: '',
    }
  },
  methods: {
    avatar(name: string | null){
      let avatar: any = ''

      if(name){
        avatar = createAvatar(avataaars, {
          seed: name,
          randomizeIds: true,
          backgroundColor: ['5DE9F4']
        });
      }
      else{
        avatar = createAvatar(avataaars, {
          randomizeIds: true,
          backgroundColor: ['5DE9F4']
        });
      }
      return avatar.toString();
    },
    async contactPicker(){
      if('contacts' in navigator && 'ContactsManager' in window){
        const properties = ['tel'];
        const options = { multiple: false };
        try {
          const navigatorWithContacts = navigator as NavigatorWithContacts;
          const contact = await navigatorWithContacts.contacts.select(properties, options);
          await this.newFriend(contact[0]['tel'][0])
        } catch (err) {
          console.error(err);
        }
      }
      else{
        this.$emit('notify', 'Cannot access contacts on device', 'error')
      }
    },
    async newFriend(phoneNumber: string){
      const id = localStorage.getItem('user')
      let dto: User = {
        id: Number(id),
        name: null,
        email: null,
        phone: phoneNumber,
        password: null,
      }
      const respone = await addFriend(dto)
      if(respone.value?.code == 201){
        this.$router.go(0)
      }else{
        dto.phone = phoneNumber.replace(/\s+/g, "")
        const res = await addFriend(dto)
        if(res.value?.code == 201){
          this.$router.go(0)
        }
        else{
          this.err = 'Your friend does not yet have an account.'
        }
      }
    },
    togglePopup(id: string | undefined, name: string | null){
      if(!this.modalToggle && this.modalToggle == ''){
        if(id && name){
          this.deleteId = id
          this.deleteName = name
          this.modalToggle = 'open'
        }
      }
      else{
        this.deleteId = ''
        this.deleteName = ''
        this.modalToggle = ''
      }
    },
    async deleteFriendShip(){
      if(this.deleteId){
        const response = await deleteFriend(this.deleteId)
        if(response.value?.code == 204){
          this.$router.go(0)
        }
        else{
          this.err = 'Your friend was not deleted.'
        }
      }
    }
  },
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import "@/assets/styles/components/buttons.scss";
@import "@/assets/styles/pages/friends.scss";
</style>