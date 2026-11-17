<script setup>
import { computed } from 'vue'

const props = defineProps({
  price: {
    type: Number,
    required: true
  },
  discount: {
    type: Number,
    default: 0
  }
})

const discountedPrice = computed(() => {
  const d = props.discount ?? 0
  const result = Number(props.price) - (Number(props.price) * d)
  return result.toFixed(2)
})
</script>

<template>
  <span>{{ discountedPrice }} €</span>
</template>
