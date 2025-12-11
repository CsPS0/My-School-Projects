<?php

require_once __DIR__ . '/src/Space/Exploration/Alien.php';

use Space\Exploration\Alien;

$aliens = [
    new Alien('Zog', 'Zogonia', true),
    new Alien('Blip', 'Blipton', false),
    new Alien('Xar', 'Xarax', true),
    new Alien('Gloop', 'Gloopia', false),
];

foreach ($aliens as $alien) {
    if ($alien->isFriendly()) {
        echo $alien->getSpecies() . ' (' . $alien->getPlanet() . ')' . PHP_EOL;
    } else {
        echo '!' . $alien->getSpecies() . ' (' . $alien->getPlanet() . ')' . PHP_EOL;
    }
}