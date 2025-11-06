<?php

require "jaratok.php";

if ($argc > 3) {
    echo "Túl sok paraméter!";
    exit(4);
}

if ($argc === 1) {
    foreach ($jaratok as $jaratszam => $jarat) {
        echo $jaratszam . "\t" . $jarat["honan"] . "-" . $jarat["hova"] . " " . $jarat["indul"] . "-" . $jarat["erkezik"] . "\t" . $jarat["legitarsasag"] . "\n";
    }
} elseif ($argc === 2) {
    $jaratszam = $argv[1];
    if (array_key_exists($jaratszam, $jaratok)) {
        $jarat = $jaratok[$jaratszam];
        echo $jarat["honan"] . "-" . $jarat["hova"] . " " . $jarat["indul"] . "-" . $jarat["erkezik"] . " (" . $jarat["legitarsasag"] . ")\n";
    } else {
        echo "A keresett járat (" . $jaratszam . ") nem található!";
        exit(7);
    }
} else {
    $keyword = $argv[1];
    $value = $argv[2];

    if ($keyword === "legitarsasag") {
        $count = 0;
        foreach ($jaratok as $jarat) {
            if ($jarat["legitarsasag"] === $value) {
                $count++;
            }
        }
        echo "A(z) " . $value . " légitársaságnak " . $count . " járata van az adatok között.\n";
    } elseif ($keyword === "repter") {
        $count = 0;
        foreach ($jaratok as $jarat) {
            if ($jarat["honan"] === $value) {
                $count++;
            }
            if ($jarat["hova"] === $value) {
                $count++;
            }
        }
        echo "A(z) " . $value . " azonosítójú reptér " . $count . "x szerepel az adatok között.\n";
    } else {
        echo "Ismeretlen paraméter '" . $keyword . "'";
        exit(9);
    }
}
