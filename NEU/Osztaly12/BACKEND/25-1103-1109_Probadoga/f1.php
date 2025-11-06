<?php

require_once 'adatok.php';

function vb() {
    global $versenyzok;
    $totalVb = 0;
    foreach ($versenyzok as $versenyzo) {
        $totalVb += $versenyzo['vb'];
    }
    return $totalVb;
}

function dobogo($rajtszam) {
    global $versenyzok;
    foreach ($versenyzok as $versenyzo) {
        if ($versenyzo['rajtszam'] == $rajtszam) {
            return $versenyzo['dobogo'];
        }
    }
    return null;
}

if ($argc > 2) {
    exit(1);
}

$parameter = $argv[1] ?? null;

if ($parameter === null) {
    foreach ($versenyzok as $versenyzo) {
        $nev = explode(" ", $versenyzo['nev']);
        echo strtoupper($nev[1]) . " (" . $versenyzo['rajtszam'] . ") [" . $versenyzo['csapat'] . "]\n";
    }
} elseif ($parameter === 'vb') {
    echo vb() . "\n";
} elseif (is_numeric($parameter) && $parameter >= 1 && $parameter <= 100) {
    $dobogosHelyezesek = dobogo($parameter);
    if ($dobogosHelyezesek !== null) {
        echo $dobogosHelyezesek . "\n";
    }
} else {
    //ha nem megy, hát nem megy
}