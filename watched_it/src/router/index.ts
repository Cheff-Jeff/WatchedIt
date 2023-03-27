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
    path: '/editaccount',
    name: 'editaccount',
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
    component: () => import('../views/EditAccountView.vue')
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
            next({ name: 'NotFount' })
            return false
          }
        }
        else if(to.params.type == 'show'){
          if(await Checkshow((to.params.id.toString())) ){
            next();
            return true
          }
          else{
            next({ name: 'NotFount' })
            return false
          }
        }
        else{
          next({ name: 'NotFount' })
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
  {
    path: '/watchlist',
    name: 'watchlist',
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
    component: () => import('../views/ListView.vue')
  },
  {
    path: '/favorites',
    name: 'favorites',
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
    component: () => import('../views/FavoriteView.vue')
  },
  {
    path: '/commingsoon',
    name: 'soon',
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
    component: () => import('../views/UpComingView.vue')
  },
  {
    //error 404 page
    path: '/:pathMatch(.*)*',
    name: 'NotFount',
    component: () => import('../views/NotFount.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
