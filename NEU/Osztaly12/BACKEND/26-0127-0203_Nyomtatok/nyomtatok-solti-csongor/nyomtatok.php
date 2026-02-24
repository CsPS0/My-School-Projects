<?php

declare(strict_types=1);

use Neu\Eszkozok\Nyomtato;
use Neu\Eszkozok\LezerNyomtato;
use Neu\Eszkozok\TintasugarasNyomtato;

require_once 'vendor/autoload.php';

ini_set('display_errors', '1');
ini_set('display_startup_errors', '1');
error_reporting(E_ALL);

if ($argc < 2) {
    echo "Túl kevés paraméter!\n";
    exit(7);
}

if ($argc > 3) {
    echo "Túl sok paraméter!\n";
    exit(9);
}

$tipusArg = $argv[1];
if ($tipusArg !== 'lezer' && $tipusArg !== 'tintasugaras') {
    echo "Hibás típus! Csak 'lezer' vagy 'tintasugaras' lehet.\n";
    exit(29);
}

$nyomtatok = [];
$gyartok = Nyomtato::getGyartok();

for ($i = 0; $i < 5; $i++) {
    $gyarto = $gyartok[array_rand($gyartok)];
    
    $betu = chr(rand(65, 90));
    $szam = rand(100, 999);
    $tipus = $betu . $szam;
    
    $szines = (bool)rand(0, 1);
    
    if ($tipusArg === 'tintasugaras') {
        $patronok = $szines ? 4 : 1;
        $ar = rand(20000, 250000);
        $nyomtatok[] = new TintasugarasNyomtato($gyarto, $tipus, $szines, $ar, $patronok);
    } else {
        $tonerek = $szines ? 4 : 1;
        $ar = rand(40000, 300000);
        $nyomtatok[] = new LezerNyomtato($gyarto, $tipus, $szines, $ar, $tonerek);
    }
}

if ($argc === 2) {
    foreach ($nyomtatok as $nyomtato) {
        echo $nyomtato . PHP_EOL;
    }
} elseif ($argc === 3) {
    $masodikArg = $argv[2];
    
    if ($masodikArg === 'fajlba') {
        $filename = "out/{$tipusArg}.csv";
        $fp = fopen($filename, 'w');
        foreach ($nyomtatok as $nyomtato) {
            fputcsv($fp, $nyomtato->toArray(), ';', '"', '\\');
        }
        fclose($fp);
        echo "Fájl kiírva: $filename\n";
    } elseif (is_numeric($masodikArg)) {
        $index = (int)$masodikArg;
        if (isset($nyomtatok[$index])) {
            echo $nyomtatok[$index] . PHP_EOL;
        } else {
            echo $nyomtatok[$index] . PHP_EOL;
        }
    }
}