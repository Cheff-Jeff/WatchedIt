import { favorites, titleDetails } from "./Models/ExternApiInterface";
import { favoriteDto } from "./Models/UserInterface";
import { fetchMovie, fetchShow } from "./api";

export const sortTitles = async (dtos: favoriteDto[]) => {
  const titles: favorites[] = []

  for(let i = 0; i < dtos.length; i++){
    if(dtos[i].movie){
      let details: titleDetails | undefined;
      if(dtos[i].movieId){
        const response = await fetchMovie(dtos[i].movieId!.toString())
        details = response.value?.data

        const struct: favorites = {
          id: details!.id,
          name: details!.title,
          poster: details!.poster,
          isMovie: true
        }

        titles.push(struct)
      }
    }
    else{
      let details: titleDetails | undefined;
      if(dtos[i].movieId){
        const response = await fetchShow(dtos[i].movieId!.toString())
        details = response.value?.data

        const struct: favorites = {
          id: details!.id,
          name: details!.title,
          poster: details!.poster,
          isMovie: false
        }

        titles.push(struct)
      }
    }
  }

  return titles
}