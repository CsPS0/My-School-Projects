<?php

declare(strict_types=1);

require_once __DIR__ . '/src/Universe/Entities/Superhero.php';

use Universe\Entities\Superhero;

$flash = new Superhero('Flash', 28, ['Speed', 'Time Travel']);
$ironMan = new Superhero('Iron Man', 45, ['Genius Intellect', 'Powered Armor Suit', 'Wealth']);
$spiderMan = new Superhero('Spider-Man', 18, ['Wall-Crawling', 'Spider-Sense', 'Super Agility']);
$superman = new Superhero('Superman', 35, ['Strength', 'Flight', 'X-ray Vision']);

$superheroes = [$flash, $ironMan, $spiderMan, $superman];

foreach ($superheroes as $superhero) {
    echo "Név: " . $superhero->getName() . "\n";
    echo "Kor: " . $superhero->getAge() . "\n";
    echo "Szupererők: " . implode(', ', $superhero->getSuperpowers()) . "\n";
    echo "\n";
}

