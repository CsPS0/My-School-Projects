<?php
declare(strict_types=1);

require_once 'src/Culture/Movie/Quotation.php';

use Culture\Movie\Quotation;

$quotes = [
    new Quotation('Az Erő legyen veled.', 'Obi-Wan Kenobi', 'Csillagok háborúja'),
    new Quotation('Az új magyar narancs. Kicsit sárgább, kicsit savanyúbb, de a mienk!', 'Pelikán', 'A tanú'),
    new Quotation('Tigris van a fürdőszobában!', 'Stu Price', 'Másnaposok'),
];

foreach ($quotes as $quote) {
    echo $quote->getText() . "\n";
    echo $quote->getPerson() . ' - ' . $quote->getTitle() . "\n";
    echo "\n";
}
