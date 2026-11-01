<script setup>
import { ref, computed, onMounted } from 'vue'
import { api } from '@utils/http.mjs'
import Input from '@components/Input.vue'
import Button from '@components/Button.vue'

const model = defineModel({
  type: Object,
  required: true,
})

const today = new Date().toISOString().substring(0, 10)

const origins = ref([])
const destinations = ref([])

const getOrigins = async () => {
  try {
    const response = await api.get('/api/timetable/stations/origins')
    origins.value = response.data.data
  } catch (error) {
    console.error(error)
  }
}

const getDestinations = async (origin) => {
  try {
    const url = origin
      ? `/api/timetable/stations/destinations?origin=${encodeURIComponent(origin)}`
      : '/api/timetable/stations/destinations'
    const response = await api.get(url)
    destinations.value = response.data.data
  } catch (error) {
    console.error(error)
  }
}

onMounted(() => {
  getOrigins()
  getDestinations()
})

const filteredOrigins = computed(() => {
  const searchVal = (model.value.from || '').toLowerCase()
  return origins.value.filter((o) => o.toLowerCase().includes(searchVal))
})

const filteredDestinations = computed(() => {
  const searchVal = (model.value.to || '').toLowerCase()
  return destinations.value.filter((d) => d.toLowerCase().includes(searchVal))
})
</script>

<template>
  <form class="w-[95%] p-12 bg-white max-w-250 mx-auto my-12 grid md:grid-cols-2 gap-4">
    <h1 class="text-3xl font-bold mb-4 col-span-full">Fahrplan & Tickets</h1>
    
    <div class="relative">
      <Input
        v-model="model.from"
        type="text"
        name="from"
        placeholder="Von"
        aria-label="Von"
        required
        autocomplete="off"
        class="w-full peer focus:rounded-b-none"
        @focusout="getDestinations(model.from)"
      />
      <ul
        v-if="filteredOrigins.length > 0"
        class="absolute bg-white p-2 w-full border hidden peer-focus:flex flex-col gap-2 max-h-42 overflow-scroll peer-focus:border-red-400 z-40"
      >
        <li
          v-for="origin in filteredOrigins"
          :key="origin"
          class="cursor-pointer hover:bg-gray-100 p-1"
          @mousedown="model.from = origin"
        >
          {{ origin }}
        </li>
      </ul>
    </div>

    <div class="relative">
      <Input
        v-model="model.to"
        type="text"
        name="to"
        placeholder="Nach"
        aria-label="Nach"
        required
        autocomplete="off"
        class="w-full peer focus:rounded-b-none"
      />
      <ul
        v-if="filteredDestinations.length > 0"
        class="absolute bg-white p-2 w-full border hidden peer-focus:flex flex-col gap-2 max-h-42 overflow-scroll peer-focus:border-red-400 z-40"
      >
        <li
          v-for="destination in filteredDestinations"
          :key="destination"
          class="cursor-pointer hover:bg-gray-100 p-1"
          @mousedown="model.to = destination"
        >
          {{ destination }}
        </li>
      </ul>
    </div>

    <div class="flex gap-4 flex-wrap">
      <Input
        v-model="model.date"
        type="date"
        name="date"
        aria-label="Datum"
        required
        :min="today"
        autocomplete="off"
        class="grow"
      />
      <Input
        v-model="model.time"
        type="time"
        name="time"
        aria-label="Reisezeitpunkt"
        required
        autocomplete="off"
      />
    </div>

    <Button type="submit" class="justify-self-auto md:justify-self-end md:px-12">
      Suchen
    </Button>
  </form>
</template>
