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