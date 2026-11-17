<script setup>
import PriceTag from '@components/PriceTag.vue'

defineProps({
  item: {
    type: Object,
    required: true
  }
})
</script>

<template>
  <div class="card">
    <img :src="`/items/${item.image}`" :alt="item.name">
    <h2 class="font-bold text-center text-xl my-2">{{ item.name }}</h2>
    <PriceTag :price="item.price" :discount_price="item.discount_price" />
    <div class="text-center">
      <RouterLink
        :to="{ name: 'items.show', params: { id: item.id } }"
        class="bg-green-800 text-white py-2 px-2"
      >
        Show
      </RouterLink>
    </div>
  </div>
</template>

<style scoped>
@reference "@assets/main.css";

.card {
  @apply border border-green-600 shadow-md flex flex-col items-center gap-2 justify-between p-4;
}

img {
  @apply w-full rounded-md;
}
</style>
