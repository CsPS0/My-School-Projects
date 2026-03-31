<form action="index.php" method="GET" class="grid grid-cols-1 md:grid-cols-4 gap-4 items-end">
    <input type="hidden" name="page" value="<?= $page ?>">
    <div>
        <label for="terminus" class="block text-sm font-medium text-gray-700">Végállomás/Útvonal</label>
        <input type="text" name="terminus" id="terminus" value="<?= htmlspecialchars($terminusFilter) ?>" class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm">
    </div>
    <div>
        <label for="interconnected" class="block text-sm font-medium text-gray-700">Fonódó hálózat</label>
        <select name="interconnected" id="interconnected" class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm">
            <option value="">Válasszon...</option>
            <?php foreach (\Budapest\Transport\TramLine::getInterconnecteds() as $key => $value) : ?>
                <option value="<?= $key ?>" <?= $interconnectedFilter === $key ? 'selected' : '' ?>><?= $value ?></option>
            <?php endforeach; ?>
        </select>
    </div>
    <div>
        <label for="since" class="block text-sm font-medium text-gray-700">Első üzemnap (minimum)</label>
        <input type="number" name="since" id="since" value="<?= htmlspecialchars($sinceFilter) ?>" class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm">
    </div>
    <div>
        <button type="submit" class="w-full inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-black hover:bg-gray-800 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-500">Szűrés</button>
    </div>
</form>
