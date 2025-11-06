<?php

require_once '../functions/fuggvenyek.php';

// 1. Hozzon létre egy f2c nevű függvényt, ami a kapott fahrenheit fokot celsius fokká váltja át.
function f2c(float $fahrenheit): float {
    return round(($fahrenheit - 32) / 1.8, 2);
}

// 2. Hozzon létre egy c2f nevű függvényt, ami a kapott celsius fokot fahrenheit fokká váltja át.
function c2f(float $celsius): float {
    return round(($celsius * 1.8) + 32, 2);
}

// 3. Hozzon létre egy fájlt fokvalto.php néven. A fájlnak két bemeneti paramétere van, az átváltandó
// fok és a a mértékegység. A mértékegység függvényében alkalmazza a fuggvenyek.php fájlban
// található c2f és f2c függvényeket a sikeres átváltáshoz. Az eredmény két tizedesre kerekítse!
// Elfogadott mértékegységek: "c", "C", "celsius", Celsius, CELSIUS, "f", "F", "fahrenheit",
// "Fahrenheit", "FAHRENHEIT"

if ($argc < 3) {
    echo "Használat: php fokvalto.php <hőmérséklet> <mértékegység>\n";
    exit(1);
}

$temperature = (float)$argv[1];
$unit = strtolower($argv[2]);

$result = 0;
$outputUnit = '';

switch ($unit) {
    case 'c':
    case 'celsius':
        $result = c2f($temperature);
        $outputUnit = 'fahrenheit';
        echo "{$temperature} celsius = {$result} fahrenheit\n";
        break;
    case 'f':
    case 'fahrenheit':
        $result = f2c($temperature);
        $outputUnit = 'celsius';
        echo "{$temperature} fahrenheit = {$result} celsius\n";
        break;
    default:
        echo "Ismeretlen mértékegység: {$argv[2]}\n";
        exit(1);
}

?>