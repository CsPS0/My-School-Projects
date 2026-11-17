<script setup>
import { computed } from 'vue'

const props = defineProps({
  type: {
    type: String,
    required: true
  }
})

const boxClass = computed(() => {
  const base = 'px-6 py-6 text-base font-semibold rounded-sm w-full text-center'
  if (props.type === 'info') {
    return `${base} bg-sky-200 border border-blue-700 text-blue-700`
  }
  if (props.type === 'warning') {
    return `${base} bg-orange-200 border border-orange-700 text-orange-700`
  }
  return base
})
</script>

<template>
  <div :class="boxClass">
    <slot />
  </div>
</template>
