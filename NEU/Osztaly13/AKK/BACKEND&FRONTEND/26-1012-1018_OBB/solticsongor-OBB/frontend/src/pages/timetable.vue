<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { api } from '@utils/http.mjs'
import Navbar from '@components/Navbar.vue'
import PlannerForm from '@components/PlannerForm.vue'
import UserAccountCard from '@components/UserAccountCard.vue'
import TrainCard from '@components/TrainCard.vue'
import Alert from '@components/Alert.vue'
import LoadingIcon from '@components/icons/LoadingIcon.vue'

const now = new Date()
const dateStr = now.toISOString().substring(0, 10)
const timeStr = now.toTimeString().substring(0, 5)

const search = ref({
  from: '',
  to: '',
  date: dateStr,
  time: timeStr,
})

const searched = ref(false)
const isLoadingTimetable = ref(false)
const journeys = ref([])

const getJourneys = async () => {
  const fromFormatted = search.value.from.replaceAll('/', '%2F')
  const toFormatted = search.value.to.replaceAll('/', '%2F')
  let url = `/api/timetable/planner/${fromFormatted}/${toFormatted}`
  if (search.value.time) {
    url += `?time=${search.value.time}`
  }
  const response = await api.get(url)
  journeys.value = response.data.data
}

const handleSubmit = async () => {
  searched.value = true
  isLoadingTimetable.value = true
  journeys.value = []
  try {
    await getJourneys()
  } catch (error) {
    console.error(error)
  } finally {
    isLoadingTimetable.value = false
  }
}

onMounted(() => {
  document.body.style.backgroundColor = '#ffffff'
})

onUnmounted(() => {
  document.body.style.backgroundColor = ''
})
</script>

<template>
  <div class="bg-white min-h-screen">
    <Navbar />
    <main class="bg-[url('/images/timetable-background.png')] bg-repeat-x pt-12">
      <PlannerForm v-model="search" @submit.prevent="handleSubmit" />

      <div class="max-w-250 mx-auto grid md:grid-cols-2 my-12 w-[95%]">
        <template v-if="!searched">
          <UserAccountCard />
          <img src="/images/vehicles.png" alt="Vehicles" class="self-center" />
        </template>

        <div
          v-else
          class="grid md:grid-cols-[1fr_auto] gap-4 w-full col-span-full"
        >
          <LoadingIcon
            v-if="isLoadingTimetable"
            class="self-center col-span-full mx-auto"
          />

          <template v-else-if="journeys.length > 0">
            <h2 class="screen-reader-text col-span-full">Verbindungen</h2>
            <TrainCard
              v-for="train in journeys"
              :key="train.id"
              :train="train"
              class="col-span-full"
            />
          </template>

          <Alert
            v-else
            class="col-span-full"
            title="Keine Reise gefunden"
            text="Bitte überprüfen Sie Ihren Abfahrts- und Ankunftsort, sowie das gewählte Datum und die angegebenen Reisenden."
          />
        </div>
      </div>
    </main>
  </div>
</template>
