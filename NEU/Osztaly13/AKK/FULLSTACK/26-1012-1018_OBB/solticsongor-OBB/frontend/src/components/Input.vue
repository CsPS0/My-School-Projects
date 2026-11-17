<script setup>
const model = defineModel();
</script>

<template>
  <input
    v-model="model"
    class="text-lg p-4 border border-gray-600 rounded focus:outline focus:outline-primary"
  />
</template>
