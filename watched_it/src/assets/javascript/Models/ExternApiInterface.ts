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
  voteDeadLine: string,
  watchDateTime: string,
  itemCount: number,
  movies: [],
  users: [],
}