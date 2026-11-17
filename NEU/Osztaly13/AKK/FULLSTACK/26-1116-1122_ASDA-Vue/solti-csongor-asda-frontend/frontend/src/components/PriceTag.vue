<script setup>
defineProps({
  price: {
    type: [Number, String],
    required: true
  },
  discount_price: {
    type: [Number, String],
    default: null
  }
})
</script>

<template>
  <span class="text-xl">
    <template v-if="discount_price">
      <s>£ {{ price }}</s> £ {{ discount_price }}
    </template>
    <template v-else>
      £ {{ price }}
    </template>
  </span>
</template>
