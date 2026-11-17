<script setup>
import { computed } from 'vue'

const props = defineProps({
  color: {
    type: String,
    default: ''
  }
})

const colorClass = computed(() => {
  const colors = {
    Christmas: 'bg-green-800 text-white',
    Halloween: 'bg-black text-white',
    'Tickled Pink': 'bg-pink-300 text-pink-900'
  }
  return colors[props.color] ?? ''
})
</script>

<template>
  <span class="badge p-1 rounded" :class="colorClass">
    <slot />
  </span>
</template>
