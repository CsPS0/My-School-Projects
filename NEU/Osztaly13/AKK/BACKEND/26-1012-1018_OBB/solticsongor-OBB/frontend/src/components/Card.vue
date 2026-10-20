<script setup>
defineProps({
  image: {
    type: String,
    required: false,
  },
  alt: {
    type: String,
    required: false,
  },
})
</script>

<template>
  <div>
    <img
      v-if="image"
      :src="image.startsWith('/') ? image : '/images/' + image"
      :alt="alt"
      class="w-full"
    />
    <div class="p-4 bg-white">
      <slot></slot>
    </div>
  </div>
</template>
