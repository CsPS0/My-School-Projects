<div class="mx-auto max-w-7xl px-4">

    <h1 class="my-4 text-center text-4xl font-bold text-teal-600">
        <?= $title ?>
    </h1>


    <div class="grid grid-cols-1 gap-8 md:grid-cols-2 lg:grid-cols-3 mb-10">
        <?php foreach ($tablets as $tablet): ?>
            <div class="group rounded-xl border border-gray-200 bg-white p-4 shadow-sm
                       transition hover:-translate-y-1 hover:shadow-lg flex flex-col">

                <img src="<?= $tablet->getImagePath() ?>" alt="<?= $tablet->fullname ?>"
                    class="mb-4 h-48 w-full rounded-lg object-contain transition group-hover:scale-105">

                <h2 class="mb-3 text-l font-semibold text-teal-600 h-12 flex items-center"><?= $tablet->fullname ?></h2>

                <ul class="space-y-1 text-sm text-gray-700 mt-auto">
                    <li><span class="font-semibold text-teal-600">Gyártó:</span> <?= $tablet->manufacturer_name ?></li>
                    <li><span class="font-semibold text-teal-600">Kijelző:</span> <?= number_format($tablet->screen, 1, ',', '') ?>"</li>
                    <li><span class="font-semibold text-teal-600">Tárhely:</span> <?= $tablet->storage ?> GB</li>
                    <li><span class="font-semibold text-teal-600">OS:</span> <?= $tablet->os ?></li>
                    <li class="pt-2 text-lg font-bold text-teal-700">
                        <?= number_format($tablet->price, 0, ',', ' ') ?> Ft
                    </li>
                </ul>

            </div>
        <?php endforeach; ?>
    </div>

</div>
