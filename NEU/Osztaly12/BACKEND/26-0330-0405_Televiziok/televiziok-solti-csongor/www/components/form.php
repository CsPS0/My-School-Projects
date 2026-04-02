<form action="index.php" method="GET" class="mb-8 p-6 rounded-xl border border-gray-200 bg-white shadow-sm">
    <input type="hidden" name="page" value="<?= htmlspecialchars($page) ?>">
    
    <div class="grid grid-cols-1 md:grid-cols-3 gap-6 items-end">
        <div class="space-y-2">
            <label for="manufacturer_id" class="block text-sm font-semibold text-cyan-700">Gyártó</label>
            <select name="manufacturer_id" id="manufacturer_id" 
                class="block w-full rounded-lg border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-cyan-500 focus:ring-cyan-500">
                <option value="">Válasszon gyártót</option>
                <?php foreach (MyShop\Multimedia\Television::getManufacturers() as $id => $name): ?>
                    <option value="<?= $id ?>" <?= $manufacturer_id == $id ? 'selected' : '' ?>>
                        <?= $name ?>
                    </option>
                <?php endforeach; ?>
            </select>
        </div>

        <div class="space-y-2">
            <label for="minimum_size" class="block text-sm font-semibold text-cyan-700">Méret (minimum)</label>
            <input type="number" name="minimum_size" id="minimum_size" value="<?= htmlspecialchars($minimum_size) ?>"
                class="block w-full rounded-lg border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-cyan-500 focus:ring-cyan-500"
                placeholder="Pl. 32">
        </div>

        <div class="space-y-2">
            <label for="maximum_size" class="block text-sm font-semibold text-cyan-700">Méret (maximum)</label>
            <input type="number" name="maximum_size" id="maximum_size" value="<?= htmlspecialchars($maximum_size) ?>"
                class="block w-full rounded-lg border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 focus:border-cyan-500 focus:ring-cyan-500"
                placeholder="Pl. 98">
        </div>
    </div>

    <div class="mt-6 flex justify-end">
        <button type="submit" 
            class="rounded-lg bg-cyan-600 px-8 py-2.5 text-center text-sm font-medium text-white hover:bg-cyan-700 focus:outline-none focus:ring-4 focus:ring-cyan-300 transition">
            Szűrés
        </button>
    </div>
</form>
