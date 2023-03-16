import axios from "axios"
import { ref } from 'vue';
import type { Ref } from 'vue'
import { User } from './Models/UserInterface';

export const fetchUsers = async () => {
  const result: Ref<User[] | null> = ref(null)
  const users: User[] = []

  await axios.get(`${process.env.VUE_APP_API_HOST}/Users`,{
    headers: { 'Content-type': 'application/json'}
  }).then(
    response => {
      if(response.status == 200){
        for(let i = 0; i < response.data.length; i++)
        {
          const user: User = {
            id: response.data[i]['id'],
            name: response.data[i]['name'],
            email: response.data[i]['email'],
            phone: response.data[i]['phone'],
            password: response.data[i]['password'],
          }
          users.push(user)
        }
        
        result.value = users
      }
    }
  )

  return result;
}

export const signIn = async (user: User) => {
  let result: {code: number, data: User} | null = null
  console.log(user);

  try {
    await axios.post(`${process.env.VUE_APP_API_HOST}/Users/Login`, user, {
      headers: { 'Content-type': 'application/json'}
    }).then((response) => {
      if(response.status == 200){
        const user: User = {
          id: response.data['id'],
          name: response.data['name'],
          email: response.data['email'],
          phone: response.data['phone'],
          password: response.data['password'],
        }
        result = {
          code: response.status,
          data: user
        }
      }
    }) 
  } catch (error: any) {
    result = {
      code: error.response.status,
      data: error.response.data
    }
  }

  return result
}

export const signUp = async (user: User) => {
  let result: {code: number, data: User} | null = null
  try {
    await axios.post(`${process.env.VUE_APP_API_HOST}/Users`, user, {
      headers: { 'Content-type': 'application/json'}
    }).then((response) => {
      if(response.status == 201){
        const user: User = {
          id: response.data['id'],
          name: response.data['name'],
          email: response.data['email'],
          phone: response.data['phone'],
          password: response.data['password'],
        }
        result = {
          code: response.status,
          data: user
        }
      }
    }) 
  } catch (error: any) {
    result = {
      code: error.response.status,
      data: error.response.data
    }
  }
  return result
}