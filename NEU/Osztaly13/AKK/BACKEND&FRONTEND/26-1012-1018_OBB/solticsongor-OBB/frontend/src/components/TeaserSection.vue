<script setup>
defineProps({
  title: {
    type: String,
    required: false,
  },
})
</script>

<template>
  <div class="grid grid-cols-1 gap-4 p-4 my-4 md:p-8 bg-white">
    <h2 v-if="title" class="text-3xl mb-[5px] col-span-full">{{ title }}</h2>
    <slot></slot>
  </div>
</template>
