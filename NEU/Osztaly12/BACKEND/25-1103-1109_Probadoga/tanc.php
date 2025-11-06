<?php

require_once 'adatok.php';

if ($argc > 2) {
    echo "Túl sok paraméter!";
    exit(4);
}

$parameter = $argv[1] ?? null;

if ($parameter === null) {
    for ($i = 0; $i < count($parok); $i += 2) {
        echo $parok[$i] . " - " . $parok[$i + 1] . "\n";
    }
} elseif ($parameter === 'fiuk') {
    for ($i = 1; $i < count($parok); $i += 2) {
        $nev = explode(" ", $parok[$i]);
        echo $nev[1] . "\n";
    }
} elseif ($parameter === 'lanyok') {
    $lanyok = [];
    for ($i = 0; $i < count($parok); $i += 2) {
        $nev = explode(" ", $parok[$i]);
        $lanyok[] = $nev[1];
    }
    echo implode(", ", $lanyok);
} elseif ($parameter === 'utolso') {
    echo $parok[count($parok) - 2] . " - " . $parok[count($parok) - 1] . "\n";
} elseif ($parameter === 'letszam') {
    echo count($parok) / 2 . "\n";
} else {
    echo "Ismeretlen paraméter!";
    exit(3);
}