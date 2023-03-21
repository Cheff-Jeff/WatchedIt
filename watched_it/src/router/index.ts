import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import { CheckLogin, CheckTitle, Checkshow } from '@/assets/javascript/guard'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'login',
    beforeEnter: (to,from,next) => {
      if(CheckLogin()){
        next({ name: 'account' })
        return false
      }
      else{
        next();
        return true
      }
    },
    component: LoginView
  },
  {
    path: '/sign-up',
    name: 'signUp',
    component: () => import('../views/SignUpView.vue')
  },
  {
    path: '/account',
    name: 'account',
    beforeEnter: (to,from,next) => {
      if(!CheckLogin()){
        next({ name: 'login' })
        return false
      }
      else{
        next();
        return true
      }
    },
    component: () => import('../views/AccountView.vue')
  },
  {
    path: '/title/:type/:id',
    name: 'details',
    beforeEnter: async (to,from,next) => {
      if(!CheckLogin()){
        next({ name: 'login' })
        return false
      }
      else{
        if(to.params.type == 'movie'){
          if(await CheckTitle(to.params.id.toString())){
            next();
            return true
          }
          else{
            next({ name: 'login' })
            return false
          }
        }
        else if(to.params.type == 'show'){
          if(await Checkshow((to.params.id.toString())) ){
            next();
            return true
          }
          else{
            next({ name: 'login' })
            return false
          }
        }
        else{
          next({ name: 'login' })
          return false
        }
      }
    },
    component: () => import('../views/DetailView.vue')
  },
  {
    path: '/trending',
    name: 'trending',
    beforeEnter: (to,from,next) => {
      if(!CheckLogin()){
        next({ name: 'login' })
        return false
      }
      else{
        next();
        return true
      }
    },
    component: () => import('../views/TrendingView.vue')
  },
  {
    path: '/friends',
    name: 'friends',
    beforeEnter: (to,from,next) => {
      if(!CheckLogin()){
        next({ name: 'login' })
        return false
      }
      else{
        next();
        return true
      }
    },
    component: () => import('../views/FriensView.vue')
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
