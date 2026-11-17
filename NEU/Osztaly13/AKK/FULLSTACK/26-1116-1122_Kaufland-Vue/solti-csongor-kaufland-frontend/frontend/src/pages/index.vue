<script setup>
import { ref, computed, onMounted } from 'vue'
import { api } from '@utils/http.mjs'
import BaseLayout from '@layouts/BaseLayout.vue'
import ProductCard from '@components/ProductCard.vue'

const products = ref([])
const categories = {
  'non-perishables': 'Trvanlivé potraviny',
  'soda syrup': 'Sirup do nápojov',
  chocolate: 'Čokoláda',
  cheese: 'Syr',
  sausage: 'Klobása',
  coffee: 'Káva'
}

const search = ref({
  name: '',
  category: ''
})

const getProducts = async () => {
  const response = await api.get('/products')
  products.value = response.data.data
}

onMounted(() => {
  getProducts()
})

const filteredProducts = computed(() => {
  return products.value.filter((product) => {
    const matchesName = product.name.toLowerCase().includes(search.value.name.toLowerCase())
    const matchesCategory = search.value.category === '' || product.category === search.value.category
    return matchesName && matchesCategory
  })
})
</script>

<template>
  <BaseLayout>
    <main class="w-11/12 mx-auto max-w-7xl my-6">
      <h2 class="text-3xl font-bold my-4">Najpredávanejšie produkty v kategórii Potraviny</h2>
      <div class="grid sm:grid-cols-2 gap-4">
        <input
          v-model="search.name"
          type="text"
          name="name"
          id="name"
          placeholder="Hľadať"
          aria-label="Hľadať"
          class="py-1 px-2 border rounded"
        >
        <select
          v-model="search.category"
          name="category"
          id="category"
          aria-label="Kategória"
          class="appearance-none py-1 px-2 border rounded"
        >
          <option value="">Všetky kategória</option>
          <option v-for="(label, key) in categories" :key="key" :value="key">
            {{ label }}
          </option>
        </select>
      </div>
      <div class="grid sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6 gap-4">
        <p v-if="filteredProducts.length === 0" class="col-span-full text-center">Nenašli sa žiadne produkty</p>
        <ProductCard
          v-for="product in filteredProducts"
          :key="product.id"
          :product="product"
        />
      </div>
    </main>
  </BaseLayout>
</template>

<route lang="yaml">
name: index
meta:
  title: Objavte našu ponuku a nakupujte za nízke ceny
</route>
