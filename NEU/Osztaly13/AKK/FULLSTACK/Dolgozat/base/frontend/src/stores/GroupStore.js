import { defineStore } from 'pinia'
import { ref, computed } from 'vue'

export const useGroup = defineStore('group', () => {
  const group = ref("A")

  return { group }
})
