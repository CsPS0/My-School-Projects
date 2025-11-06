<?php

require "versenyzok.php";

if ($argc > 2) {
    echo "Túl sok paraméter!";
    exit(8);
}

if ($argc === 1) {
    echo "rajtsz.	Név			Ország			Születési dátum
";
    foreach ($versenyzok as $rajtszam => $versenyzo) {
        echo $rajtszam . "	" . $versenyzo["nev"] . "		" . $versenyzo["orszag"] . "		" . $versenyzo["szulido"] . "
";
    }
} else {
    $orszag = $argv[1];
    foreach ($versenyzok as $rajtszam => $versenyzo) {
        if ($versenyzo["orszag"] === $orszag) {
            echo $rajtszam . ": " . $versenyzo["nev"] . "
";
        }
    }
}
