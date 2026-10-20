import { createRouter, createWebHistory } from 'vue-router'
import { setTitle } from '@/router/guards/SetTitleGuard.mjs'

export const routes = [
  {
    path: '/',
    name: 'home',
    component: () => import('@/pages/index.vue'),
    meta: { title: 'Startseite' }
  },
  {
    path: '/timetable',
    name: 'timetable.index',
    component: () => import('@/pages/timetable.vue'),
    meta: { title: 'Fahrplan' }
  },
  {
    path: '/trains/:id',
    name: 'trains.show',
    component: () => import('@/pages/trains.vue'),
    meta: { title: 'Reisedetails' }
  }
]

export const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach(setTitle)
