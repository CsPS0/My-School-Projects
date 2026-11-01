<script setup>
import StepperButton from '@components/StepperButton.vue';
import ArrowDownIcon from '@components/icons/ArrowDownIcon.vue';

const emit = defineEmits(['previous', 'next']);
</script>

<template>
    <div class="grid grid-cols-[auto_1fr_auto] items-center">
        <StepperButton class="rotate-90" @click="emit('previous')">
            <ArrowDownIcon class="md:scale-200" />
            <span class="screen-reader-text">Rückwärts navigieren</span>
        </StepperButton>
        <slot></slot>
        <StepperButton class="rotate-270" @click="emit('next')">
            <ArrowDownIcon class="md:scale-200" />
            <span class="screen-reader-text">Vorwärts navigieren</span>
        </StepperButton>
    </div>
</template>
