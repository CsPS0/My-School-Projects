<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8 text-blue-700">
    <?php foreach ($horses as $horse): ?>
    <div class="horse h-full bg-white rounded-xl shadow-md p-4
        flex flex-col border border-blue-200
        transition hover:border-blue-700 hover:shadow-xl">
        
        <h2 class="text-xl font-semibold text-center">
            <?php echo $horse->breed; ?>
        </h2>

        <p class="text-center text-gray-500">
            <?php echo "$horse->state ($horse->year)"; ?>
        </p>

        <img src="<?php echo $horse->path; ?>" alt="<?php echo $horse->image; ?>" class="my-2">

        <p class="text-gray-700">
            <?php echo $horse->description; ?>
        </p>
    </div>
    <?php endforeach; ?>
</div>
