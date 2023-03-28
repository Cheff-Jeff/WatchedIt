export interface TrendingMovie {
  id: number,
  poster_path: string | null,
  title: string | null,
  release_date: string | null,
}

export interface TrendingShow {
  id: number,
  poster_path: string | null,
  name: string | null,
  first_air_date: string | null,
}

export interface titleDetails {
  id: number,
  genres: {
    id: number, 
    name: string
  }[],
  language: string,
  title: string,
  overview: string,
  vote_average: number,
  poster: string,
  backdrop: string,
  providers: {
    logo: string,
    name: string
  }[],
  cast: {
    name: string,
    photo: string,
  }[],
  first_air_date: string,
  director: string,
}

export interface searchMovieShow {
  id: number,
  title: string,
  media_type: string,
}

export interface collectionList {
  id: number,
  title: string,
  addMovieDeadLine: string,
  voteDeadLine: string,
  watchDateTime: string,
  itemCount: number,
  movies: [],
  users: [],
}

export interface rateMovieShow {
  movielistId: number,
  userId: number,
  externMovieIds: []
}

export interface favorites {
  id:  number,
  name: string,
  poster: string,
  isMovie: boolean
}

export interface genre {id: number, name: string}
export interface provider {logo: string, name: string}
export interface actor {name: string, photo: string}

export interface newList {
  title: string,
  userId: number,
  addMovieDeadLine: string,
  voteDeadLine: string,
  watchDateTime: string,
}

export interface MovieShowToList {
  MovieListId: number,
  externId: number,
  movie: boolean
}

export interface FriendToList {
  MovieListId: number,
  phoneNumber: string,
}

