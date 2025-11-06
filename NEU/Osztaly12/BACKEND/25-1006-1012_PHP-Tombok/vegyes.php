<?php

// 1. Hozza létre az alábbi tömböt:
$vegyes = [5, 9, "Hello", 11.2, "Béla", 33, "Márta", 48.98, 7];

// 2. A szkript egy paramétert kaphat, ha többet, vagy kevesebbet kapna azt hibaüzenettel jutalmazza és lépjen ki!
if ($argc != 2) {
    echo "A szkript pontosan egy paramétert vár!";
    exit;
}

// 3. Az az egy paraméter az alábbiak egyik lehet csak: szamok, egesz, valos, szoveg.
$allowed_params = ["szamok", "egesz", "valos", "szoveg"];
if (!in_array($argv[1], $allowed_params)) {
    echo "A paraméter csak 'szamok', 'egesz', 'valos', vagy 'szoveg' lehet.";
    exit;
}

$parameter = $argv[1];

// 4. Amennyiben a paraméter szamok, úgy hozzon létre egy $szamok tömböt és válogassa ki a számokat, majd jelenítse is meg a minta szerint!
if ($parameter == "szamok") {
    $szamok = [];
    foreach ($vegyes as $elem) {
        if (is_numeric($elem)) {
            $szamok[] = $elem;
        }
    }
    echo "php vegyes.php szamok<br>";
    $i = 1;
    foreach ($szamok as $szam) {
        echo $i . ". szám: " . $szam . "<br>";
        $i++;
    }
}

// 5. Amennyiben a paraméter egesz, úgy hozzon létre egy $egeszSzamok tömböt és válogassa ki az egész számokat, majd jelenítse is meg a minta szerint!
if ($parameter == "egesz") {
    $egeszSzamok = [];
    foreach ($vegyes as $elem) {
        if (is_int($elem)) {
            $egeszSzamok[] = $elem;
        }
    }
    echo "php vegyes.php egesz<br>";
    $i = 1;
    foreach ($egeszSzamok as $szam) {
        echo $i . ". egész szám: " . $szam . "<br>";
        $i++;
    }
}

// 6. Amennyiben a paraméter valos, úgy hozzon létre egy $valosSzamok tömböt és válogassa ki az valós számokat, majd jelenítse is meg a minta szerint!
if ($parameter == "valos") {
    $valosSzamok = [];
    foreach ($vegyes as $elem) {
        if (is_float($elem)) {
            $valosSzamok[] = $elem;
        }
    }
    echo "php vegyes.php valos<br>";
    $i = 1;
    foreach ($valosSzamok as $szam) {
        echo $i . ". valós szám: " . $szam . "<br>";
        $i++;
    }
}

// 7. Amennyiben a paraméter szoveg, úgy hozzon létre egy $szovegek tömböt és válogassa ki a szövegeket, majd jelenítse is meg a minta szerint!
if ($parameter == "szoveg") {
    $szovegek = [];
    foreach ($vegyes as $elem) {
        if (is_string($elem)) {
            $szovegek[] = $elem;
        }
    }
    echo "php vegyes.php szoveg<br>";
    $i = 1;
    foreach ($szovegek as $szoveg) {
        echo $i . ". szöveg: " . $szoveg . "<br>";
        $i++;
    }
}

?>
