<template>
<BaseLayout>
        <div class="container px-4">
            <h1>Items</h1 >

            <form class="">
                <div>
                    <label for="name">Name</label>
                    <input type="text" name="name" id="name" >
                </div>

                <div>
                    <label for="event">Event</label>
                    <select name="gender" id="gender" required>
                        <option value="">All</option>
                    </select>
                </div>

            </form>

            <div class="items">
                <p>No items found.</p>
                
            </div>
        </div>
    </BaseLayout>
</template>

<style scoped>
@reference "@assets/main.css";


form {
    @apply grid grid-cols-2 my-4 gap-4;

    > div {
        @apply flex flex-wrap flex-col lg:flex-row gap-4;

        label {
            @apply content-center
        }

        input, select {
            flex-grow: 1
        }
    }
}

label {
    @apply text-green-800;
}

input {
    @apply border border-green-800 rounded px-2 py-1;
}

select {
    @apply border border-green-800 rounded px-2 py-1;
}
</style>

<route lang="yaml">
name: index
meta:
  title: Homepage
</route>
