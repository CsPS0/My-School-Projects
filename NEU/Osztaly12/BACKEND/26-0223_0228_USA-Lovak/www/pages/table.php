<div class="w-full overflow-x-auto">
    <table class="min-w-full border border-blue-200 overflow-hidden">
        <thead class="bg-blue-700 text-white">
            <tr>
                <th class="px-4 py-3 text-left text-sm font-bold">Állam</th>
                <th class="px-4 py-3 text-left text-sm font-bold">Fajta</th>
                <th class="px-4 py-3 text-left text-sm font-bold">Leírás</th>
                <th class="px-4 py-3 text-left text-sm font-bold">Év</th>
            </tr>
        </thead>
        <tbody class="divide-y divide-blue-100 bg-white">
            <?php foreach ($horses as $horse): ?>
            <tr class="odd:bg-blue-50 transition">
                <td class="px-4 py-3 text-sm text-gray-700"><?php echo $horse->state; ?></td>
                <td class="px-4 py-3 text-sm text-gray-700 font-bold"><?php echo $horse->breed; ?></td>
                <td class="px-4 py-3 text-sm text-gray-700"><?php echo $horse->description; ?></td>
                <td class="px-4 py-3 text-sm text-gray-700"><?php echo $horse->year; ?></td>
            </tr>
            <?php endforeach; ?>
        </tbody>
    </table>
</div>
