export interface TrendingMovie {
  id: number,
  backdrop_path: string | null,
  title: string | null,
  release_date: string | null,
}

export interface TrendingShow {
  id: number,
  backdrop_path: string | null,
  name: string | null,
  first_air_date: string | null,
}