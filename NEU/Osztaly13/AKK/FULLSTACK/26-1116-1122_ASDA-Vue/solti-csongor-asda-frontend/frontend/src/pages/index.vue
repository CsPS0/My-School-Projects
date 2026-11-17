<script setup>
import { ref, computed, onMounted } from 'vue'
import { api } from '@utils/http.mjs'
import BaseLayout from '@layouts/BaseLayout.vue'
import ItemCard from '@components/ItemCard.vue'

const items = ref([])
const events = ['Tickled Pink', 'Halloween', 'Christmas']
const search = ref({
  name: '',
  event: ''
})

const getItems = async () => {
  const response = await api.get('/items')
  items.value = response.data.data
}

const filteredItems = computed(() => {
  return items.value.filter((item) => {
    const matchesName = item.name.toLowerCase().includes(search.value.name.toLowerCase())
    const matchesEvent = search.value.event === '' || item.event === search.value.event
    return matchesName && matchesEvent
  })
})

onMounted(() => {
  getItems()
})
</script>

<template>
  <BaseLayout>
    <div class="container px-4">
      <h1 class="text-3xl font-bold text-center text-green-800 my-4 uppercase">Items</h1>

      <form>
        <div>
          <label for="name">Name</label>
          <input v-model="search.name" type="text" name="name" id="name">
        </div>

        <div>
          <label for="event">Event</label>
          <select v-model="search.event" name="event" id="event">
            <option value="">All</option>
            <option v-for="event in events" :key="event" :value="event">
              {{ event }}
            </option>
          </select>
        </div>
      </form>

      <div class="items grid grid-cols-1 md:grid-cols-3 xl:grid-cols-5 gap-6">
        <p v-if="filteredItems.length === 0">No items found.</p>
        <ItemCard v-for="item in filteredItems" :key="item.id" :item="item" />
      </div>
    </div>
  </BaseLayout>
</template>

<style scoped>
@reference "@assets/main.css";

form {
  @apply grid grid-cols-2 my-4 gap-4;

  > div {
    @apply flex flex-wrap flex-col lg:flex-row gap-4;

    label {
      @apply content-center;
    }

    input, select {
      flex-grow: 1;
    }
  }
}

label {
  @apply text-green-800;
}

input {
  @apply border border-green-800 rounded px-2 py-1;
}

select {
  @apply border border-green-800 rounded px-2 py-1;
}
</style>

<route lang="yaml">
name: index
meta:
  title: Főoldal
</route>
