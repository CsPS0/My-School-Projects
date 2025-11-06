<?php

$hero = $argv[1];

echo "Hős neve: " . strtoupper($hero) . "\n";

echo "3. feladat:\n";
echo "Hős karaktereinek a száma: " . strlen($hero) . "\n";

echo "4. feladat:\n";
if (isset($argv[2])) {
    echo "A hősnek van társa.\n";
} else {
    echo "A hősnek nincs társa.\n";
}

echo "5. feladat:\n";
if (str_contains(strtolower($hero), 'man')) {
    echo "A hős nevében szerepel a 'man' angol szó.\n";
} else {
    echo "A hős nevében NEM szerepel a 'man' angol szó.\n";
}

echo "6. feladat:\n";
if (strtolower($hero) == strrev(strtolower($hero))) {
    echo "A hős neve palindrom szó.\n";
} else {
    echo "A hős neve NEM palindrom szó.\n";
}

echo "7. feladat:\n";
if (str_starts_with(strtoupper($hero), 'S')) {
    echo "A hős neve 'S' betűvel kezdődik.\n";
} else {
    echo "A hős neve NEM 'S' betűvel kezdődik.\n";
}

echo "8. feladat:\n";
if (str_ends_with(strtolower($hero), 'n')) {
    echo "A hős neve 'n' betűvel végződik.\n";
} else {
    echo "A hős neve NEM 'n' betűvel végződik.\n";
}

?>