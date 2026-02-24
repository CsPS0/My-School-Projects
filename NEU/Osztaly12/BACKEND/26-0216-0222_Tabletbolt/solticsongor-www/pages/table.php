<div class="mx-auto max-w-7xl px-4">

    <h1 class="my-4 text-center text-4xl font-bold text-teal-600">
     <?= $title ?>
    </h1>

    <div class="overflow-x-auto rounded-xl border border-teal-200 shadow-sm">
        <table class="min-w-full overflow-hidden rounded-xl">
            <thead class="bg-teal-600 text-white">
                <tr>
                    <?php
                    $headers = [
                        ['text' => 'Gyártó', 'align' => 'text-left'],
                        ['text' => 'Méret', 'align' => 'text-center'],
                        ['text' => 'Tárhely', 'align' => 'text-center'],
                        ['text' => 'OS', 'align' => 'text-center'],
                        ['text' => 'Ár', 'align' => 'text-right']
                    ];
                    foreach ($headers as $header): ?>
                        <th class="px-4 py-3 text-sm font-semibold <?= $header['align'] ?>">
                            <?= $header['text'] ?>
                        </th>
                    <?php endforeach; ?>
                </tr>
            </thead>

            <tbody class="divide-y divide-teal-100">
                <?php foreach ($tablets as $tablet): ?>
                    <tr class="odd:bg-white even:bg-teal-50 hover:bg-teal-100 transition">
                        <td class="px-4 py-2 text-left">
                            <?= $tablet->manufacturer_name ?> </td>
                        <td class="px-4 py-2 text-center">
                            <?= number_format($tablet->screen, 1, ',', '') ?>" </td>
                        <td class="px-4 py-2 text-center">
                            <?= $tablet->storage ?> GB </td>
                        <td class="px-4 py-2 text-center">
                            <?= $tablet->os ?> </td>
                        <td class="px-4 py-2 text-right font-bold text-teal-700">
                            <?= number_format($tablet->price, 0, ',', ' ') ?> Ft </td>
                    </tr>
                <?php endforeach; ?>
            </tbody>
        </table>
    </div>
</div>
