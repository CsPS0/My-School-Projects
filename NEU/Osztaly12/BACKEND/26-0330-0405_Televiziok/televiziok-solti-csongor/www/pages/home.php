<h1 class="text-4xl font-bold mb-4">Viszonylatok - <?= count($filteredLines) ?> találat</h1>

<div class="grid grid-cols-1 md:grid-cols-3 lg:grid-cols-6 gap-4">
    <?php
    $cardTemplate = file_get_contents('components/card.html');
    foreach ($filteredLines as $line) {
        $card = $cardTemplate;
        $card = str_replace('{{Kép}}', $line->getImagePath(), $card);
        $card = str_replace('{{Szám}}', $line->number, $card);
        $card = str_replace('{{Hossz}}', $line->length, $card);
        $card = str_replace('{{Útvonal}}', $line->route, $card);
        $card = str_replace('{{Fonódó}}', $line->getInterconnected(), $card);
        echo $card;
    }
    ?>
</div>
