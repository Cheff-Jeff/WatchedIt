export interface User {
  id: number | null,
  name: string | null,
  email: string | null,
  phone: string | null,
  password: string | null,
}

export interface Friend {
  id: number | undefined,
  name: string | null,
  phone: string | null,
}