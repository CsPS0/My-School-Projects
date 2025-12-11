<?php

require_once 'data.php';

if ($argc > 2) {
    echo "Túl sok paraméter!" . PHP_EOL;
    exit(4);
}

$param = $argv[1] ?? null;

if ($param === null) {
    for ($i = 0; $i < count($nba); $i += 2) {
        echo $nba[$i] . " vs " . $nba[$i + 1] . PHP_EOL;
    }
    exit(0);
}

switch ($param) {
    case 'nyertes':
        for ($i = 0; $i < count($nba); $i += 2) {
            echo $nba[$i] . PHP_EOL;
        }
        break;
    case 'vesztes':
        for ($i = 1; $i < count($nba); $i += 2) {
            echo $nba[$i] . PHP_EOL;
        }
        break;
    case 'meccsek':
        echo count($nba) / 2 . PHP_EOL;
        break;
    case 'first':
        echo "Az első meccs a " . $nba[0] . " és " . $nba[1] . " között zajlott le." . PHP_EOL;
        break;
    case 'finals':
        $lastIndex = count($nba) - 2;
        echo "Az utolsó meccs a " . $nba[$lastIndex] . " és " . $nba[$lastIndex + 1] . " között zajlott le." . PHP_EOL;
        break;
    default:
        echo "Ismeretlen paraméter!" . PHP_EOL;
        exit(3);
}

exit(0);
