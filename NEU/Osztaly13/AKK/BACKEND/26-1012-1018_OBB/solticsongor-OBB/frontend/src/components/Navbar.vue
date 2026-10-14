<script setup>
import { ref, computed } from 'vue'
import { RouterLink } from 'vue-router'
import BurgerIcon from '@components/icons/BurgerIcon.vue'

const navItems = [
  { title: 'Tickets & Ermäßigungen', page: { name: 'home' } },
  { title: 'Fahrplan', page: { name: 'timetable.index' } },
  { title: 'Reiseplanung & Services', page: { name: 'home' } },
  { title: 'Regionale Angebote', page: { name: 'home' } },
]

const open = ref(false)

const openClass = computed(() => {
  return open.value ? 'max-h-300' : 'max-h-12 md:max-h-auto'
})
</script>

<template>
  <nav
    :class="[
      'flex p-3 flex-col gap-2 md:justify-center items-start md:flex-row md:items-center transition-all duration-300 h-ful bg-white shadow fixed w-full z-50 overflow-y-hidden',
      openClass
    ]"
  >
    <div class="flex gap-2 items-center">
      <BurgerIcon class="md:hidden cursor-pointer" @click="open = !open" />
      <RouterLink :to="{ name: 'home' }">
        <img src="/images/oebb-logo.svg" alt="ÖBB" />
      </RouterLink>
    </div>
    <RouterLink
      v-for="(item, index) in navItems"
      :key="index"
      :to="item.page"
      class="text-center grow flex-grow"
    >
      {{ item.title }}
    </RouterLink>
  </nav>
</template>
