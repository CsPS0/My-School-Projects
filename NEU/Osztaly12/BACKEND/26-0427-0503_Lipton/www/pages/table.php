<?php include __DIR__ . '/../components/formSimple.php'; ?>

<div class="overflow-x-auto bg-white rounded shadow-sm border border-gray-200">
    <table class="min-w-full text-left border-collapse">
        <thead>
            <tr class="border-b border-gray-300">
                <th class="py-4 px-6 font-bold text-gray-900">Megnevezés</th>
                <th class="py-4 px-6 font-bold text-gray-900 text-center">Típus</th>
                <th class="py-4 px-6 font-bold text-gray-900 text-center">Gyártó</th>
                <th class="py-4 px-6 font-bold text-gray-900 text-center">Kiszerelés</th>
                <th class="py-4 px-6 font-bold text-gray-900 text-center">Mennyiség</th>
                <th class="py-4 px-6 font-bold text-gray-900 text-right">Ár</th>
            </tr>
        </thead>
        <tbody>
            <?php foreach ($filteredTeas as $index => $tea): ?>
                <tr class="<?php echo $index % 2 === 0 ? 'bg-white' : 'bg-gray-100'; ?> border-b border-gray-200 hover:bg-gray-200 transition duration-150">
                    <td class="py-4 px-6 text-gray-800"><?php echo htmlspecialchars($tea->name); ?></td>
                    <td class="py-4 px-6 text-center text-gray-700"><?php echo htmlspecialchars($tea->range); ?></td>
                    <td class="py-4 px-6 text-center text-gray-700"><?php echo htmlspecialchars($tea->brand); ?></td>
                    <td class="py-4 px-6 text-center text-gray-700"><?php echo htmlspecialchars($tea->format); ?></td>
                    <td class="py-4 px-6 text-center text-gray-700"><?php echo $tea->qty . ' ' . $tea->unit; ?></td>
                    <td class="py-4 px-6 text-right font-medium text-gray-900">
                        <?php echo $tea->price; ?> Ft
                    </td>
                </tr>
            <?php endforeach; ?>
        </tbody>
    </table>
</div>
