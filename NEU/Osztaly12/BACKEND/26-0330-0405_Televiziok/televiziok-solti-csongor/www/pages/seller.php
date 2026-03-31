<?php
$headers = ["Kép", "Viszonylat", "Útvonal", "Hossz", "Első üzemnap", "Hálózat"];
?>
<h1 class="text-4xl font-bold mb-4">Viszonylatok - <?= count($filteredLines) ?> találat</h1>

<div class="overflow-auto">
    <table class="w-full">
        <thead>
            <tr class="hover:bg-gray-100 border-b border-b-zinc-400">
                <?php foreach ($headers as $header) : ?>
                    <th class="px-2 py-1 align-middle font-semibold whitespace-nowrap"><?= $header ?></th>
                <?php endforeach; ?>
            </tr>
        </thead>
        <tbody>
            <?php foreach ($filteredLines as $line) : ?>
                <tr class="hover:bg-gray-100 border-b border-b-zinc-400">
                    <td class="px-2 py-1 text-center">
                        <img src="<?= $line->getImagePath() ?>" alt="<?= $line->number ?>" class="h-10 w-10 mx-auto rounded-full object-cover">
                    </td>
                    <td class="px-2 py-1 text-center"><?= $line->number ?></td>
                    <td class="px-2 py-1"><?= $line->route ?></td>
                    <td class="px-2 py-1 text-center"><?= $line->length ?> km</td>
                    <td class="px-2 py-1 text-center"><?= $line->since ?></td>
                    <td class="px-2 py-1 text-center"><?= $line->getInterconnected() ?></td>
                </tr>
            <?php endforeach; ?>
        </tbody>
    </table>
</div>
