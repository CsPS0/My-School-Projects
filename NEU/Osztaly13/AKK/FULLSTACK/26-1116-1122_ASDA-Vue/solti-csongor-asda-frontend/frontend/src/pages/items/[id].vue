<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { api } from '@utils/http.mjs'
import BaseLayout from '@layouts/BaseLayout.vue'
import Badge from '@components/Badge.vue'
import PriceTag from '@components/PriceTag.vue'

const route = useRoute()
const item = ref(null)
const loading = ref(true)

const colorClasses = computed(() => {
  const colors = {
    Christmas: 'text-red-600 border-red-600',
    Halloween: 'text-orange-600 border-orange-600',
    'Tickled Pink': 'text-pink-600 border-pink-600'
  }
  return item.value ? (colors[item.value.event] ?? '') : ''
})

const getItems = async () => {
  const response = await api.get(`/items/${route.params.id}`)
  item.value = response.data.data
  loading.value = false
}

onMounted(() => {
  getItems()
})
</script>

<template>
  <BaseLayout>
    <p v-if="loading">Loading items...</p>
    <div
      v-if="!loading && item"
      class="item grid md:grid-cols-2 gap-4 border mx-auto w-2xl p-4 shadow-xl"
      :class="colorClasses"
    >
      <img class="w-full" :src="`/items/${item.image}`" :alt="item.name">
      <div class="info w-full">
        <h2 class="text-lg font-bold mb-4">{{ item.name }}</h2>
        <Badge :color="item.event">{{ item.event }}</Badge>
        <ul class="mt-4">
          <li>{{ item.qty }} {{ item.unit }}</li>
          <li><PriceTag :price="item.price" :discount_price="item.discount_price" /></li>
        </ul>
        <RouterLink :to="{ name: 'index' }" class="button block mt-12">Back</RouterLink>
      </div>
    </div>
  </BaseLayout>
</template>

<route lang="yaml">
name: items.show
meta:
  title: Termék
</route>
