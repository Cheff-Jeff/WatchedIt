import axios from "axios"
import { ref } from 'vue';
import type { Ref } from 'vue'
import { User } from './Models/UserInterface';
import { TrendingMovie, TrendingShow } from "./Models/ExternApiInterface";

export const fetchUsers = async () => {
  const result: Ref<User[] | null> = ref(null)
  const users: User[] = []

  await axios.get(`${process.env.VUE_APP_API_HOST}/Users`, {
    headers: { 'Content-type': 'application/json' }
  }).then(
    response => {
      if (response.status == 200) {
        for (let i = 0; i < response.data.length; i++) {
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
  let result: { code: number, data: User } | null = null
  console.log(user);

  try {
    await axios.post(`${process.env.VUE_APP_API_HOST}/Users/Login`, user, {
      headers: { 'Content-type': 'application/json' }
    }).then((response) => {
      if (response.status == 200) {
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
  let result: { code: number, data: User } | null = null
  try {
    await axios.post(`${process.env.VUE_APP_API_HOST}/Users`, user, {
      headers: { 'Content-type': 'application/json' }
    }).then((response) => {
      if (response.status == 201) {
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

export const getExternTrendingMovies = async () => {
  const result: Ref<TrendingMovie[] | null> = ref(null)
  const movies: TrendingMovie[] = []
  try {
    await axios.get(`${process.env.VUE_APP_API_EXTERNHOSTV3}movie/popular?api_key=${process.env.VUE_APP_API_EXTERNKEYV3}&language=en-US&page=1`, {
      headers: { 'Content-type': 'application/json' }
    }).then(
      response => {
        if (response.status == 200) {
          for (let i = 0; i < response.data.results.length; i++) {
            const movie: TrendingMovie = {
              id: response.data.results[i]["id"],
              backdrop_path: response.data.results[i]["backdrop_path"],
              title: response.data.results[i]["title"],
              release_date: response.data.results[i]["release_date"],
            }
            movies.push(movie)
          }

          result.value = movies
        }
      })
  } catch (error: any) {
    console.log("error", error)
  }
  return result
}

export const getExternTrendingShows = async () => {
  const result: Ref<TrendingShow[] | null> = ref(null)
  const shows: TrendingShow[] = []
  try {
    await axios.get(`${process.env.VUE_APP_API_EXTERNHOSTV3}tv/popular?api_key=${process.env.VUE_APP_API_EXTERNKEYV3}&language=en-US&page=1`, {
      headers: { 'Content-type': 'application/json' }
    }).then(
      response => {
        if (response.status == 200) {
          for (let i = 0; i < response.data.results.length; i++) {
            const show: TrendingShow = {
              id: response.data.results[i]["id"],
              backdrop_path: response.data.results[i]["backdrop_path"],
              name: response.data.results[i]["name"],
              first_air_date: response.data.results[i]["first_air_date"],
            }
            shows.push(show)
          }

          result.value = shows
        }
      })
  } catch (error: any) {
    console.log("error", error)
  }
  return result
}