<script setup>
import HeroItem from '@components/HeroItem.vue'
import Button from '@components/Button.vue'
import ArrowDownIcon from '@components/icons/ArrowDownIcon.vue'

defineProps({
  items: {
    type: Array,
    required: true,
  },
})
</script>

<template>
  <div class="overflow-x-hidden">
    <div class="flex hero-carousel-animate">
      <HeroItem
        v-for="item in items"
        :key="item.id"
        :image="item.image_url"
      >
        <h3 class="text-shadow-gray-800/50 text-shadow-md text-center md:text-left text-2xl md:text-4xl font-bold mb-3">
          {{ item.title }}
        </h3>
        <p class="text-shadow-gray-800/50 text-shadow-md text-center md:text-left text-sm md:text-base mb-2">
          {{ item.description }}
        </p>
        <Button
          type="button"
          class="w-full max-w-95 hidden md:block"
        >
          {{ item.button_text }}
          <ArrowDownIcon class="inline -rotate-90 rotate-270 ml-2" />
        </Button>
      </HeroItem>
    </div>
  </div>
</template>

<style scoped>
@keyframes hero-carousel {
  0%,
  15% {
    transform: translate(0);
  }
  18%,
  32% {
    transform: translate(-100%);
  }
  34%,
  49% {
    transform: translate(-200%);
  }
  51%,
  67% {
    transform: translate(-300%);
  }
  69%,
  82% {
    transform: translate(-400%);
  }
  84%,
  98% {
    transform: translate(-500%);
  }
}

.hero-carousel-animate {
  animation: hero-carousel 40s infinite;
}
</style>
