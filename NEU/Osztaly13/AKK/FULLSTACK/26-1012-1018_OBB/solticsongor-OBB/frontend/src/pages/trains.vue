<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { api } from '@utils/http.mjs'
import Navbar from '@components/Navbar.vue'
import TrainInfo from '@components/TrainInfo.vue'
import TicketCard from '@components/TicketCard.vue'
import LoadingIcon from '@components/icons/LoadingIcon.vue'

const route = useRoute()
const train = ref(null)
const selectedTicket = ref(-1)
const isLoading = ref(true)

const getTrain = async () => {
  try {
    const response = await api.get(`/api/trains/${route.params.id}`)
    train.value = response.data.data
  } catch (error) {
    console.error(error)
  }
}

const reservationPrefixes = ['RJ', 'RJX', 'NJ', 'EN', 'IC', 'IR', 'EC', 'ICE', 'D']

const canReserveSeat = computed(() => {
  if (!train.value?.train_number) return false
  const prefix = train.value.train_number.split(' ')[0]
  return reservationPrefixes.includes(prefix)
})

onMounted(async () => {
  await getTrain()
  isLoading.value = false
})
</script>

<template>
  <div>
    <Navbar />
    <main class="max-w-340 mx-auto pt-12 w-[95%] bg-white min-h-[calc(100dvh-3rem)]">
      <LoadingIcon v-if="isLoading" class="self-center mx-auto my-12" />

      <template v-else-if="train">
        <h1 class="text-3xl font-bold mt-12 mb-4">Reisedetails</h1>
        <TrainInfo :train="train" />

        <div class="grid md:grid-cols-[repeat(auto-fit,minmax(250px,1fr))] gap-4 justify-between mt-8">
          <h2 class="screen-reader-text">Ticketangebot</h2>

          <TicketCard
            v-if="train.second_class_price != null"
            title="2. Klasse"
            :price="train.second_class_price"
            :class="{ 'outline-2': selectedTicket === 0 }"
            @selected="selectedTicket = 0"
          />

          <TicketCard
            v-if="train.first_class_price != null"
            title="1. Klasse"
            :price="train.first_class_price"
            :class="{ 'outline-2': selectedTicket === 1 }"
            @selected="selectedTicket = 1"
          />

          <TicketCard
            v-if="train.business_class_price != null"
            title="Business Class"
            :price="train.business_class_price"
            :class="{ 'outline-2': selectedTicket === 2 }"
            @selected="selectedTicket = 2"
          />

          <TicketCard
            v-if="canReserveSeat"
            title="Nur Sitzplatz reservieren"
            :price="2"
            :reservation-only="true"
            :class="{ 'outline-2': selectedTicket === 3 }"
            @selected="selectedTicket = 3"
          />
        </div>
      </template>
    </main>
  </div>
</template>
