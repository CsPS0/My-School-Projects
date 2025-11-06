<?php

require "en.php";

$en["jelszo"] = "alma";
$en["titok"] = "Minden nap egy alma az orvost távol tartja.";

if ($argc !== 2) {
    echo "A szkript pontosan 1 paramétert vár!";
    exit(1);
}

$parameter = $argv[1];

if (array_key_exists($parameter, $en) && ($parameter === "nev" || $parameter === "szuletesi_datum" || $parameter === "kor" || $parameter === "kedvenc_szin")) {
    echo $en[$parameter];
} elseif ($parameter === $en["jelszo"]) {
    echo $en["titok"];
} else {
    echo "Ismeretlen paraméter!";
    exit(2);
}
