<a href="index.php?action=create" class="bg-violet-600 text-white text-center p-1 rounded-md inline-block cursor-pointer font-bold">Koncert rögzítése</a>

<table class="w-full mt-4">
    <thead>
        <tr class="bg-violet-600 text-white">
            <th class="p-1 uppercase">Név</th>
            <th class="p-1 uppercase">Helyszín</th>
            <th class="p-1 uppercase">Típus</th>
            <th class="p-1 uppercase">Dátum</th>
            <th class="p-1 uppercase">Ár</th>
        </tr>
    </thead>
    <tbody>
        <?php foreach ($concerts as $concert): ?>
            <tr>
                <td class="p-1 text-center font-bold"><a href="index.php?action=show&id=<?= $concert->id ?>" class="text-violet-600 underline"><?= $concert->name ?></a></td>
                <td class="p-1 text-center font-bold"><?= $concert->location ?></td>
                <td class="p-1 text-center font-bold"><?= $concert->getTypeName() ?></td>
                <td class="p-1 text-center font-bold"><?= $concert->date->format('Y-m-d H:i:s') ?></td>
                <td class="p-1 text-center font-bold">
                    <span class="bg-violet-600 text-white p-1 rounded whitespace-nowrap">
                        <?= $concert->getFormattedPrice() ?>
                    </span>
                </td>
            </tr>
        <?php endforeach; ?>
    </tbody>
</table>
