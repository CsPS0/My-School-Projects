<script setup>
import { ref, onMounted } from 'vue'
import { api } from '@utils/http.mjs'
import Navbar from '@components/Navbar.vue'
import Hero from '@components/Hero.vue'
import TeaserSection from '@components/TeaserSection.vue'
import TeaserCard from '@components/TeaserCard.vue'
import FromTheStatesSection from '@components/FromTheStatesSection.vue'

const heroContents = ref([])
const newFromObbTeasers = ref([])
const currentOffersTeasers = ref([])
const fromTheStatesTeasers = ref([])

const getHeroContents = async () => {
  try {
    const response = await api.get('/api/hero-contents')
    heroContents.value = response.data.data
  } catch (error) {
    console.error(error)
  }
}

const getNewFromObbTeasers = async () => {
  try {
    const response = await api.get('/api/teasers?section=new_from_obb')
    newFromObbTeasers.value = response.data.data
  } catch (error) {
    console.error(error)
  }
}

const getCurrentOffersTeasers = async () => {
  try {
    const response = await api.get('/api/teasers?section=current_offers')
    currentOffersTeasers.value = response.data.data
  } catch (error) {
    console.error(error)
  }
}

const getFromTheStatesTeasers = async () => {
  try {
    const response = await api.get('/api/teasers?section=from_the_states')
    fromTheStatesTeasers.value = response.data.data
  } catch (error) {
    console.error(error)
  }
}

onMounted(() => {
  getHeroContents()
  getNewFromObbTeasers()
  getCurrentOffersTeasers()
  getFromTheStatesTeasers()
})
</script>

<template>
  <div>
    <Navbar />
    <main class="max-w-340 mx-auto pt-12">
      <h1 class="screen-reader-text">ÖBB</h1>
      <Hero :items="heroContents" />
      <div class="w-[95%] max-w-250 mx-auto">
        <TeaserSection title="Neues von den ÖBB" class="md:grid-cols-6">
          <TeaserCard
            v-for="item in newFromObbTeasers.slice(0, 2)"
            :key="item.id"
            :item="item"
            class="md:col-span-3"
          />
          <TeaserCard
            v-for="item in newFromObbTeasers.slice(2)"
            :key="item.id"
            :item="item"
            class="md:col-span-2"
          />
        </TeaserSection>

        <TeaserSection title="Aktuelle Angebote" class="md:grid-cols-3">
          <TeaserCard
            v-for="item in currentOffersTeasers"
            :key="item.id"
            :item="item"
          />
        </TeaserSection>

        <FromTheStatesSection :items="fromTheStatesTeasers" />
      </div>
    </main>
  </div>
</template>
