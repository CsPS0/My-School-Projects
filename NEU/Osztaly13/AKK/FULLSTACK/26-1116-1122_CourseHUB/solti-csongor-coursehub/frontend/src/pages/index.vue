<script setup>
import { ref, reactive, computed, onMounted } from 'vue'
import BaseLayout from '@layouts/BaseLayout.vue'
import CourseCard from '@components/CourseCard.vue'
import Box from '@components/Box.vue'
import { api } from '@utils/http.mjs'
import { ucfirst } from '@utils/helper.js'

const courses = ref([])
const loading = ref(true)
const levels = ['beginner', 'intermediate', 'advanced']
const search = reactive({
  name: '',
  level: ''
})

async function loadCourses() {
  loading.value = true
  try {
    const response = await api.get('/api/courses')
    courses.value = response.data.data
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  loadCourses()
})

const filteredItems = computed(() => {
  return courses.value.filter(course => {
    const matchesName = course.name.toLowerCase().includes(search.name.toLowerCase().trim())
    const matchesLevel = !search.level || course.level === search.level
    return matchesName && matchesLevel
  })
})
</script>

<template>
  <BaseLayout>
    <form action="" class="grid md:grid-cols-2 gap-4 max-w-2xl mx-auto space-y-5 md:space-y-0 mt-6" @submit.prevent>
      <div class="flex flex-col space-y-1">
        <label for="name" class="text-sm font-medium text-gray-700">
          Name
        </label>
        <input
          type="text"
          name="name"
          id="name"
          v-model="search.name"
          placeholder="Enter course name"
          class="w-full rounded-lg border border-gray-300 px-3 py-2 text-gray-900 placeholder-gray-400 focus:border-orange-500 focus:ring-2 focus:ring-orange-500/30 focus:outline-none transition"
        />
      </div>

      <div class="flex flex-col space-y-1">
        <label for="level" class="text-sm font-medium text-gray-700">
          Level
        </label>
        <select
          name="level"
          id="level"
          v-model="search.level"
          class="w-full rounded-lg border border-gray-300 px-3 py-2 text-gray-900 bg-white appearance-none focus:border-orange-500 focus:ring-2 focus:ring-orange-500/30 focus:outline-none transition"
        >
          <option value="">All level</option>
          <option v-for="level in levels" :key="level" :value="level">
            {{ ucfirst(level) }}
          </option>
        </select>
      </div>
    </form>

    <div class="mt-8">
      <p v-if="loading">Page loading...</p>
      <Box v-else-if="filteredItems.length === 0" type="warning">
        No course found.
      </Box>
      <div v-else class="courses grid grid-cols-1 md:grid-cols-4 gap-4">
        <CourseCard
          v-for="course in filteredItems"
          :key="course.id"
          :course="course"
        />
      </div>
    </div>
  </BaseLayout>
</template>

<route lang="yaml">
name: index
meta:
  title: Főoldal
</route>
