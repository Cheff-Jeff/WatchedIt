import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import { CheckLogin } from '@/assets/javascript/guard'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'login',
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
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
