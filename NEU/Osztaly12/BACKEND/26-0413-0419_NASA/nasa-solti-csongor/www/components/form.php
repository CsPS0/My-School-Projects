<form action="index.php" method="GET" class="bg-slate-800 p-5 rounded-xl flex flex-wrap gap-5 items-end mb-8 shadow-lg border border-slate-700">
    <input type="hidden" name="view" value="<?= $view ?>">

    <div class="flex flex-col flex-grow sm:flex-grow-0">
        <label for="program" class="mb-2 text-sm font-medium text-slate-300">Program</label>
        <select name="program" id="program" class="px-3 py-2 rounded-lg bg-slate-900 text-slate-100 border border-slate-600 focus:outline-none focus:ring-2 focus:ring-blue-500">
            <option value="Összes program" <?= $programFilter === 'Összes program' ? 'selected' : '' ?>>Összes program</option>
            <?php foreach (\Nasa\Programs\Mission::getPrograms() as $program): ?>
                <option value="<?= $program ?>" <?= $programFilter === $program ? 'selected' : '' ?>><?= $program ?></option>
            <?php endforeach; ?>
        </select>
    </div>

    <div class="flex flex-col flex-grow sm:flex-grow-0">
        <label for="sort" class="mb-2 text-sm font-medium text-slate-300">Rendezés</label>
        <select name="sort" id="sort" class="px-3 py-2 rounded-lg bg-slate-900 text-slate-100 border border-slate-600 focus:outline-none focus:ring-2 focus:ring-blue-500">
            <option value="launchDate" <?= $sortField === 'launchDate' ? 'selected' : '' ?>>Dátum</option>
            <option value="name" <?= $sortField === 'name' ? 'selected' : '' ?>>Név</option>
            <option value="budget" <?= $sortField === 'budget' ? 'selected' : '' ?>>Költségvetés</option>
        </select>
    </div>

    <div class="flex flex-col flex-grow sm:flex-grow-0">
        <label for="order" class="mb-2 text-sm font-medium text-slate-300">Irány</label>
        <select name="order" id="order" class="px-3 py-2 rounded-lg bg-slate-900 text-slate-100 border border-slate-600 focus:outline-none focus:ring-2 focus:ring-blue-500">
            <option value="ASC" <?= $sortOrder === 'ASC' ? 'selected' : '' ?>>ASC</option>
            <option value="DESC" <?= $sortOrder === 'DESC' ? 'selected' : '' ?>>DESC</option>
        </select>
    </div>

    <button type="submit" class="sm:ml-auto px-6 py-2 bg-blue-600 text-white font-bold rounded-lg cursor-pointer hover:bg-blue-500 transition-colors duration-200 border-none shadow-md w-full sm:w-auto">Keresés</button>
</form>
