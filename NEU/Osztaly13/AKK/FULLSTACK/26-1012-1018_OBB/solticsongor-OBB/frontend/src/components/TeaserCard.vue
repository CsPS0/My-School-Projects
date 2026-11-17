<script setup>
import Card from '@components/Card.vue'

defineProps({
  item: {
    type: Object,
    required: true,
  },
})
</script>

<template>
  <Card :image="item.image_url" :alt="item.title">
    <h3 class="text-lg mb-2">{{ item.title }}</h3>
    <p class="font-light text-sm text-ellipsis overflow-hidden whitespace-nowrap">
      {{ item.description }}
    </p>
  </Card>
</template>
