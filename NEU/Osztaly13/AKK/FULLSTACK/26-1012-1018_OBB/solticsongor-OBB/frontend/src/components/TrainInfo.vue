<script setup>
import TrainIcon from '@components/icons/TrainIcon.vue';

defineProps({
    train: {
        type: Object,
        required: true,
    }
});
</script>

<template>
<div class="flex items-center gap-4 bg-white p-6 border shadow">
    <TrainIcon class="w-12 h-12 text-primary shrink-0" />
    <div class="grow">
        <div class="flex flex-wrap items-center justify-between gap-2 mb-2">
            <span class="font-bold text-xl">{{ train?.train_number }}</span>
            <span class="text-gray-600 font-medium">
                {{ train?.departure_time ? train.departure_time.substring(0, 5) : '' }} - {{ train?.arrival_time ? train.arrival_time.substring(0, 5) : '' }}
            </span>
        </div>
        <div class="flex justify-between text-sm text-gray-700">
            <span>{{ train?.origin }}</span>
            <span>→</span>
            <span>{{ train?.destination }}</span>
        </div>
    </div>
</div>
</template>
