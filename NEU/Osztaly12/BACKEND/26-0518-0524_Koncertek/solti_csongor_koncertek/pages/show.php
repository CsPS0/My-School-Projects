<a href="index.php?action=index" class="bg-violet-600 text-white text-center p-1 rounded-md inline-block cursor-pointer font-bold">Vissza a főoldalra</a>

<div class="grid sm:grid-cols-2 md:grid-cols-4 gap-4 mt-4">
    <div class="bg-violet-100 p-2 rounded-lg">
        <h2 class="text-xl font-bold mb-2">Típus</h2>
        <p>
            <?= $selectedConcert->getTypeName() ?>
        </p>
    </div>
    <div class="bg-violet-100 p-2 rounded-lg">
        <h2 class="text-xl font-bold mb-2">Helyszín</h2>
        <p>
            <?= $selectedConcert->location ?>
        </p>
    </div>
    <div class="bg-violet-100 p-2 rounded-lg">
        <h2 class="text-xl font-bold mb-2">Dátum</h2>
        <p>
            <?= $selectedConcert->date->format('Y-m-d H:i:s') ?>
        </p>
    </div>
    <div class="bg-violet-100 p-2 rounded-lg">
        <h2 class="text-xl font-bold mb-2">Ár</h2>
        <p>
            <span class="bg-violet-600 text-white p-1 rounded">
                <?= $selectedConcert->getFormattedPrice() ?>
            </span>
        </p>
    </div>
</div>
