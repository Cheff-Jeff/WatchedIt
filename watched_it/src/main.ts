import { createApp } from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import { autoAnimatePlugin } from '@formkit/auto-animate/vue'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap'
import './assets/styles/main.scss'

createApp(App).use(router).use(autoAnimatePlugin).mount('#app')