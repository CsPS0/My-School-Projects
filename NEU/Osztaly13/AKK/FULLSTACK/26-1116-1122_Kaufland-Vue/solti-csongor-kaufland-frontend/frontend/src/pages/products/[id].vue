<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { api } from '@utils/http.mjs'
import BaseLayout from '@layouts/BaseLayout.vue'
import Badge from '@components/Badge.vue'
import Price from '@components/Price.vue'

const route = useRoute()
const product = ref(null)
const loading = ref(true)

const getProduct = async () => {
  const response = await api.get(`/products/${route.params.id}`)
  product.value = response.data.data
  loading.value = false
}

onMounted(() => {
  getProduct()
})
</script>

<template>
  <BaseLayout>
    <main class="w-11/12 mx-auto max-w-7xl my-6">
      <p v-if="loading" class="text-center">Načítava sa...</p>
      <div v-if="!loading && product" class="grid md:grid-cols-3 gap-4">
        <img
          :src="`/images/${product.image}`"
          :alt="product.name"
          class="aspect-square object-contain p-4 border rounded-lg"
        >
        <div class="md:col-span-2">
          <h2 class="text-3xl font-bold my-4">{{ product.name }}</h2>
          <div class="p-4 rounded-lg border shadow">
            <p v-if="product.discount">
              <span class="line-through">{{ product.price.toFixed(2) }} €</span>
              <Badge class="ml-2">-{{ Math.round(product.discount * 100) }}%</Badge>
            </p>
            <p class="text-3xl font-black mb-2">
              <Price :price="product.price" :discount="product.discount" />
            </p>
            <p v-if="product.free_shipping" class="font-bold text-lime-600 mb-4">
              Doprava zdarma
            </p>
            <button class="bg-primary py-2 px-4 font-bold text-white w-full text-center rounded-full cursor-pointer">
              Pridať do košíka
            </button>
          </div>
        </div>
      </div>
    </main>
  </BaseLayout>
</template>

<route lang="yaml">
name: products.show
meta:
  title: Produkt
</route>
