export const CheckLogin = (): boolean => {
  const id: string | null = localStorage.getItem('user')
  if(id){
    return true
  }
  return false
}