import axios from "axios"
import { ref } from 'vue';
import type { Ref } from 'vue'
import { User } from './Models/UserInterface';
import { TrendingMovie, TrendingShow, titleDetails, searchMovieShow } from "./Models/ExternApiInterface";

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

export const fetchUser = async (id: string) => {
  const result: Ref<{ code: number, data: User } | null> = ref(null)
  try {
    await axios.get(`${process.env.VUE_APP_API_HOST}/Users/${id}`, {
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
        result.value = {
          code: response.status,
          data: user
        }
      }
    })
  } catch (error: any) {
    result.value = {
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
              poster_path: response.data.results[i]["poster_path"],
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
    console.log(process.env.VUE_APP_API_EXTERNHOSTV3)
    await axios.get(`${process.env.VUE_APP_API_EXTERNHOSTV3}tv/popular?api_key=${process.env.VUE_APP_API_EXTERNKEYV3}&language=en-US&page=1`, {
      headers: { 'Content-type': 'application/json' }
    }).then(
      response => {
        if (response.status == 200) {
          for (let i = 0; i < response.data.results.length; i++) {
            const show: TrendingShow = {
              id: response.data.results[i]["id"],
              poster_path: response.data.results[i]["poster_path"],
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

export const fetchMovie = async (id: string) => {
  const result: Ref<{code: number, data: titleDetails} | null> = ref(null)
  const details: titleDetails = {id: 0, genres: [{id: 0, name: 'string'}], language: 'string', title: 'string',
    overview: 'string', vote_average: 0, poster: 'string', backdrop: 'string', providers: [{logo: 'string', name: 'string'}],
    cast: [{name: 'string', photo: 'string'}], first_air_date: 'string', director: 'string'
  }

  try {
    await axios.get(`${process.env.VUE_APP_API_EXTERNHOSTV3}/movie/${id}?api_key=${process.env.VUE_APP_API_EXTERNKEYV3}`, {
      headers: { 'Content-type': 'application/json' }
    }).then(response => {
      if(response.status == 200){
        const genres: {id: number, name: string}[] = Array<{id: number, name: string}>()

        for(let i = 0; i < response.data['genres'].length; i++){
          const genre = {id: response.data['genres'][i]['id'], name: response.data['genres'][i]['name']}
          genres.push(genre)
        }

        details.id = response.data['id']
        details.genres = genres
        details.language = response.data['original_language']
        details.title = response.data['original_title']
        details.overview = response.data['overview']
        details.vote_average = response.data['vote_average']
        details.poster = response.data['poster_path']
        details.backdrop = response.data['backdrop_path']
        details.first_air_date = response.data['release_date']
      }
    }).then(async () => {
      const providers = await axios.get(`${process.env.VUE_APP_API_EXTERNHOSTV3}/movie/${id}/watch/providers?api_key=${process.env.VUE_APP_API_EXTERNKEYV3}`, {
        headers: { 'Content-type': 'application/json' }
      })
      if(providers.status == 200){
        const streamers: {logo: string,name: string}[] = Array<{logo: string,name: string}>()
        if(providers.data['results']['NL'] !== undefined && providers.data['results']['NL'] !== 'undefined' && providers.data['results']['NL']['flatrate']){
          for(let i = 0; i < providers.data['results']['NL']['flatrate'].length; i++){
            streamers.push({
              logo: providers.data['results']['NL']['flatrate'][i]['logo_path'], 
              name: providers.data['results']['NL']['flatrate'][i]['provider_name']
            })
          }
        }
        details.providers = streamers
      }
    }).then(async () => {
      const crew = await axios.get(`${process.env.VUE_APP_API_EXTERNHOSTV3}/movie/${id}/credits?api_key=${process.env.VUE_APP_API_EXTERNKEYV3}`, {
        headers: { 'Content-type': 'application/json' }
      })
      if(crew.status == 200){
        const cast: {name: string,photo: string}[] = Array<{name: string,photo: string}>()
        for(let i = 0; i < crew.data['cast'].length; i++){
          if(crew.data['cast'][i]['known_for_department'] == "Acting"){
            cast.push({
              name: crew.data['cast'][i]['name'],
              photo: crew.data['cast'][i]['profile_path']
            })
          }
        }

        details.cast = cast

        for(let i = 0; i < crew.data['crew'].length; i++){
          if(crew.data['crew'][i]['job'] == "Director"){
            details.director = crew.data['crew'][i]['name']
            break;
          }
        }
      }
    }).then(()=>{
      result.value = {
        code: 200,
        data: details
      }
      console.log(details)
    })
  } catch (error: any) {
    result.value = {
      code: error.response.status,
      data: error.response.data
    }
  }
  return result
}

export const fetchShow = async (id: string) => {
  const result: Ref<{code: number, data: titleDetails} | null> = ref(null)
  const details: titleDetails = {id: 0, genres: [{id: 0, name: 'string'}], language: 'string', title: 'string',
    overview: 'string', vote_average: 0, poster: 'string', backdrop: 'string', providers: [{logo: 'string', name: 'string'}],
    cast: [{name: 'string', photo: 'string'}], first_air_date: 'string', director: 'string'
  }

  try {
    await axios.get(`${process.env.VUE_APP_API_EXTERNHOSTV3}/tv/${id}?api_key=${process.env.VUE_APP_API_EXTERNKEYV3}`, {
      headers: { 'Content-type': 'application/json' }
    }).then(response => {
      if(response.status == 200){
        const genres: {id: number, name: string}[] = Array<{id: number, name: string}>()

        for(let i = 0; i < response.data['genres'].length; i++){
          const genre = {id: response.data['genres'][i]['id'], name: response.data['genres'][i]['name']}
          genres.push(genre)
        }

        details.id = response.data['id']
        details.genres = genres
        details.language = response.data['original_language']
        details.title = response.data['original_name']
        details.overview = response.data['overview']
        details.vote_average = response.data['vote_average']
        details.poster = response.data['poster_path']
        details.backdrop = response.data['backdrop_path']
        details.first_air_date = response.data['first_air_date']
      }
    }).then(async () => {
      const providers = await axios.get(`${process.env.VUE_APP_API_EXTERNHOSTV3}tv/${id}/watch/providers?api_key=${process.env.VUE_APP_API_EXTERNKEYV3}`, {
        headers: { 'Content-type': 'application/json' }
      })
      if(providers.status == 200){
        const streamers: {logo: string,name: string}[] = Array<{logo: string,name: string}>()
        if(providers.data['results']['NL'] !== undefined && providers.data['results']['NL'] !== 'undefined' && providers.data['results']['NL']['flatrate']){
          for(let i = 0; i < providers.data['results']['NL']['flatrate'].length; i++){
            streamers.push({
              logo: providers.data['results']['NL']['flatrate'][i]['logo_path'], 
              name: providers.data['results']['NL']['flatrate'][i]['provider_name']
            })
          }
        }
        details.providers = streamers
      }
    }).then(async () => {
      const crew = await axios.get(`${process.env.VUE_APP_API_EXTERNHOSTV3}tv/${id}/aggregate_credits?api_key=${process.env.VUE_APP_API_EXTERNKEYV3}`, {
        headers: { 'Content-type': 'application/json' }
      })
      if(crew.status == 200){
        const cast: {name: string,photo: string}[] = Array<{name: string,photo: string}>()
        for(let i = 0; i < crew.data['cast'].length; i++){
          if(crew.data['cast'][i]['known_for_department'] == "Acting"){
            cast.push({
              name: crew.data['cast'][i]['name'],
              photo: crew.data['cast'][i]['profile_path']
            })
          }
        }

        details.cast = cast

        for(let i = 0; i < crew.data['crew'].length; i++){
          if(crew.data['crew'][i]['jobs'][0]['job'] == "Director"){
            details.director = crew.data['crew'][i]['name']
            break;
          }
        }
      }
    }).then(()=>{
      result.value = {
        code: 200,
        data: details
      }
      console.log(details)
    })
  } catch (error: any) {
    console.log(error)
    result.value = {
      code: error.response.status,
      data: error.response.data
    }
  }
  return result
}

export const updateUser = async (user: User) => {
  let result: { code: number } | null = null

  try {
    await axios.put(`${process.env.VUE_APP_API_HOST}/Users/updateuser`, user, {
      headers: { 'Content-type': 'application/json' }
    }).then((response) => {
      if (response.status == 200) {
        result = {
          code: response.status,
        }
      }
    })
  } catch (error: any) {
    result = {
      code: error.response.status,
    }
  }
  return result
}

export const searchMovies = async (searchPhrase: string) => {
  const result: Ref<searchMovieShow[] | null> = ref(null)
  const moviesshows: searchMovieShow[] = []
  try {
    await axios.get(`${process.env.VUE_APP_API_EXTERNHOSTV3}search/multi?api_key=${process.env.VUE_APP_API_EXTERNKEYV3}&query=${searchPhrase}&page=1&include_adult=false`, {
      headers: { 'Content-type': 'application/json' }
    }).then(
      response => {
        if (response.status == 200) {
            for (let i = 0; i < response.data.results.length; i++) {
              
              let title: string;
              if(response.data.results[i]["name"]){
                title = response.data.results[i]["name"]
              } else {
                title = response.data.results[i]["title"]
              }

              let media_type: string;
              if(response.data.results[i]["media_type"] == 'tv'){
                media_type = 'show'
              } else {
                media_type = response.data.results[i]["media_type"]
              }

              const searchmovieshow: searchMovieShow = {
                id: response.data.results[i]["id"],
                title: title,
                media_type: media_type,
              }
              moviesshows.push(searchmovieshow)
            }

            result.value = moviesshows
        }
      })
  } catch (error: any) {
    console.log("error", error)
  }
  return result
}