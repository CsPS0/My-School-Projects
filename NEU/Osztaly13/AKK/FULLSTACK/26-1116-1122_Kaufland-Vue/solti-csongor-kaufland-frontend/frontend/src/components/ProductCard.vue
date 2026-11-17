<script setup>
import { RouterLink } from 'vue-router'
import Price from '@components/Price.vue'

defineProps({
  product: {
    type: Object,
    required: true
  }
})

const categories = {
  'non-perishables': 'Trvanlivé potraviny',
  'soda syrup': 'Sirup do nápojov',
  chocolate: 'Čokoláda',
  cheese: 'Syr',
  sausage: 'Klobása',
  coffee: 'Káva'
}
</script>

<template>
  <div class="border rounded-xl p-4 flex flex-col">
    <img :src="`/images/${product.image}`" :alt="product.name" class="w-full aspect-square object-contain mb-4">
    <h3 class="font-bold text-xl text-ellipsis wrap-break-word break-all overflow-hidden line-clamp-3 mb-1 grow">
      {{ product.name }}
    </h3>
    <p class="mb-2">{{ categories[product.category] ?? product.category }}</p>
    <p class="text-lg font-black mb-2">
      <Price :price="product.price" :discount="product.discount" />
    </p>
    <RouterLink
      :to="{ name: 'products.show', params: { id: product.id } }"
      class="bg-primary py-2 px-4 font-bold text-white w-full text-center rounded-full"
    >
      Viac informácií
    </RouterLink>
  </div>
</template>
