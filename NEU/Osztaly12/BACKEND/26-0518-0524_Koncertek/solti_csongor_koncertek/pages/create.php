<?php use Event\Party\Concert; ?>
<form action="index.php?action=store" method="post" class="w-11/12 max-w-100 mx-auto grid md:grid-cols-[auto_1fr] gap-2">
    <div class="grid grid-cols-subgrid col-span-2">
        <label for="name" class="font-bold">Név</label>
        <input type="text" name="name" id="name" value="<?= $old['name'] ?? '' ?>" class="border rounded border-gray-500 focus:border-violet-600 px-1">
        <p class="col-span-full text-red-600 mt-2">
            <?= $errors['name'] ?? '' ?>
        </p>
    </div>
    <div class="grid grid-cols-subgrid col-span-2">
        <label for="location" class="font-bold">Helyszín</label>
        <input type="text" name="location" id="location" value="<?= $old['location'] ?? '' ?>" class="border rounded border-gray-500 focus:border-violet-600 px-1">
        <p class="col-span-full text-red-600 mt-2">
            <?= $errors['location'] ?? '' ?>
        </p>
    </div>
    <div class="grid grid-cols-subgrid col-span-2">
        <label for="type" class="font-bold">Típus</label>
        <select name="type" id="type" class="border rounded border-gray-500 focus:border-violet-600">
            <?php foreach (Concert::getTypes() as $key => $value): ?>
                <option value="<?= $key ?>" <?= (isset($old['type']) && $old['type'] === $key) ? 'selected' : '' ?>><?= $value ?></option>
            <?php endforeach; ?>
        </select>
        <p class="col-span-full text-red-600 mt-2">
            <?= $errors['type'] ?? '' ?>
        </p>
    </div>
    <div class="grid grid-cols-subgrid col-span-2">
        <label for="date" class="font-bold">Dátum</label>
        <input type="date" name="date" id="date" value="<?= $old['date'] ?? '' ?>" class="border rounded border-gray-500 focus:border-violet-600 px-1"> 
        <p class="col-span-full text-red-600 mt-2">
            <?= $errors['date'] ?? '' ?>
        </p>
    </div>
    <div class="grid grid-cols-subgrid col-span-2">
        <label for="price" class="font-bold">Ár</label>
        <input type="number" name="price" id="price" value="<?= $old['price'] ?? '' ?>" class="border rounded border-gray-500 focus:border-violet-600 px-1"> 
        <p class="col-span-full text-red-600 mt-2">
            <?= $errors['price'] ?? '' ?>
        </p>
    </div>
    <input type="submit" value="Rögzítés" class="bg-violet-600 text-white text-center p-1 rounded-md cursor-pointer font-bold col-span-full">
</form>
