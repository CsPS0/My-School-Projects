<?php
require_once __DIR__ . '/../data.php';
?>
<main class="mx-auto max-w-7xl px-4 py-6">
    <h1 class="mb-6 text-4xl font-bold text-center"><?php echo $title; ?></h1>
    <section id="games" class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
        <?php foreach ($games as $game): ?>
            <div class="grid grid-rows-[auto_1fr_auto] rounded-2xl bg-white p-4 shadow-lg">
                <img src="<?php echo $game->image; ?>" class="w-full" alt="<?php echo $game->title; ?>">
                <h2 class="text-base font-semibold my-[0.75rem]" lang="en"><?php echo $game->title; ?></h2>
                <p class="flex items-center justify-between flex-wrap text-sm">
                    <span class="whitespace-nowrap"> Ár: <?php echo number_format($game->price, 0, ',', ' '); ?> Ft </span>
                    <span class="<?php echo $game->platformClass; ?> rounded-full px-4 py-1 text-xs font-semibold whitespace-nowrap"> <?php echo $game->platform; ?> </span>
                </p>
            </div>
        <?php endforeach; ?>
    </section>
</main>
