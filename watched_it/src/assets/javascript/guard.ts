import axios from "axios"

export const CheckLogin = (): boolean => {
  const id: string | null = localStorage.getItem('user')
  if(id){
    return true
  }
  return false
}

export const CheckTitle = async (id: string) => {
  let result = false
  try {
    await axios.get(`${process.env.VUE_APP_API_EXTERNHOSTV3}movie/${id}?api_key=${process.env.VUE_APP_API_EXTERNKEYV3}`)
    .then(res => {
      if(!res.data['success']){
        result = false
      }
      else{
        result = true
      }
    })
  } catch (error) {
    console.log(error)
    result = false
  }
  return result
}