<script setup>
const props = defineProps({
  type: {
    type: String,
    required: false,
    default: 'submit',
  },
  variant: {
    type: String,
    required: false,
    default: 'primary',
  },
})

const classes = {
  primary: 'bg-primary text-white hover:bg-white hover:text-primary hover:outline outline-primary',
  empty: 'bg-white text-black',
}
</script>

<template>
  <button :type="type" :class="['px-4 py-3 cursor-pointer', classes[variant]]">
    <slot></slot>
  </button>
</template>
