<script setup>
import { ref, computed, onMounted } from 'vue';
import Input from '@components/Input.vue';
import TextArea from '@components/TextArea.vue';
import Select from '@components/Select.vue';
import Button from '@components/Button.vue';
import { api } from '@utils/http.mjs';


const emit = defineEmits(['submit']);

const organizations = ref([]);

const images = ref([]);

const types = ref([{value: 'mellékszereplő', label: 'Mellékszereplő'}, {value: 'főszereplő', label: 'Főszereplő'}]);

const organizationsMapped = computed(() => organizations.value.map(x => ({value: x.id, label: `${x.name} (${x.area})`})));

const imagesMapped = computed(() => images.value.map(x => ({value: x, label: x})));

const data = ref({
    name: '',
    type: '',
    occupation: '',
    description: '',
    image: '',
    organization_id: '',
});

async function getOrganizations() {
    const response = await api.get('/api/organizations');
    organizations.value = response.data.data;
}

async function getImages() {
    const response = await api.get('/api/images');
    images.value = response.data.data;
}

onMounted(async () => {
    await getOrganizations();
    await getImages();
});

function handleSubmit() {
    emit('submit', data.value);
}
</script>

<template>
    <form @submit.prevent="handleSubmit" class="grid md:grid-cols-6 gap-4 px-6 pt-6 pb-8 rounded bg-gray-900">
        <Input label="Név" type="text" name="name" required :maxlength="50" class="md:col-span-3" v-model="data.name" />
        <Select label="Szervezet" name="organization" :items="organizationsMapped" required class="md:col-span-3" v-model="data.organization_id" />
        <TextArea label="Leírás" name="description" required :maxlength="60000" :rows="4" class="col-span-full" v-model="data.description" />
        <Select label="Típus" name="type" :items="types" required class="md:col-span-2" v-model="data.type" />
        <Input label="Foglalkozása" type="text" name="occupation" required :maxlength="20" class="md:col-span-2" v-model="data.occupation" />
        <Select label="Kép" name="image" :items="imagesMapped" required class="md:col-span-2" v-model="data.image" />
        <Button type="submit" class="col-span-full justify-self-end">Létrehozás</Button>
    </form>
</template>
