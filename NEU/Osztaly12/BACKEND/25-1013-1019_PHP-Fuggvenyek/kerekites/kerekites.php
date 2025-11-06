<?php

// 2. Készítse el az alábbi függvényeket:

// felkerekites ($x): a paraméterül kapott $x számot felfelé kerekíti.
function felkerekites(float $x): int {
    return (int)ceil($x);
}

// leKerekites ($x): a paraméterül kapott $x számot lefelé kerekíti.
function leKerekites(float $x): int {
    return (int)floor($x);
}

// matematikaiKerekites ($x) a paraméterül kapott $x számot a matematikai kerekítési szabá-
// lyok alapján kerekíti. 5-től kezdve felfelé, alatta pedig lefelé kerekítsen.
function matematikaiKerekites(float $x): int {
    return (int)round($x);
}

// ftKerekites ($x): a paraméterül kapott $x számot az 5 Ft-os kerekítés szabályait alkalmazza.
// Amennyiben a szám 1-re vagy 2-re végződik, úgy lefelé kerekítsen, 8-ra vagy 9-re végződő
// számokat felfelé kerekítse, a töbi számjegy esetén 5-re végződő számot eredményezzen!
function ftKerekites(int $x): int {
    $lastDigit = $x % 10;
    if ($lastDigit == 1 || $lastDigit == 2) {
        return $x - $lastDigit;
    } elseif ($lastDigit == 8 || $lastDigit == 9) {
        return $x + (10 - $lastDigit);
    } else {
        return $x - $lastDigit + 5;
    }
}

// bankarKerekites($x): a paraméterül kapott $x számot mindig a hozzá legközelebb eső páros
// számhoz kerekíti. Például a 2,2 esetén egészre kerekítve 2-t ad. Míg 3,2 esetén már 4-et ad
// eredményül. Bár a matematikai kerekítés szerint 3 lenne, de az nem páros szám. Ami szóba
// jöhet páros szám az a 2 és a 4, de ha szmegyenesen vizsgáljuk úgy utóbbihoz áll közelebb.
function bankarKerekites(float $x): int {
    $rounded = round($x);
    if ($rounded % 2 !== 0) {
        // Ha páratlan, kerekítsük a legközelebbi páros számra
        if (($x - floor($x)) === 0.5) {
            // Ha pontosan fél, kerekítsük felfelé, ha a felfelé kerekített páros, különben lefelé
            if (ceil($x) % 2 === 0) {
                return (int)ceil($x);
            } else {
                return (int)floor($x);
            }
        } else if ($x > $rounded) {
            // Ha felfelé kerekítettünk páratlanra, és az eredeti szám nagyobb volt
            return (int)ceil($x);
        } else {
            // Ha lefelé kerekítettünk páratlanra, és az eredeti szám kisebb volt
            return (int)floor($x);
        }
    }
    return (int)$rounded;
}

// 1. Hozzon létre egy fájlt kerekites.php néven. A szkriptnek legfeljebb két bemenete lehetséges
// • Amennyiben túl sok paramétert kapna, úgy "Túl sok paraméter!" hibaüzenettel jelezzen vissza,
// majd lépjen ki.
// • Az első paraméter a kerekítendő szám lesz. Csak egész szám lehet.
// • A második paraméter a kerekítés módja, megadása opcionális, az alábbi értékek egyike:
// fel, ilyenkor felfelé kerekít, egészre (felKerekites())
// le, ilyenkor lefelé kerekít, egészre (leKerekites())
// ft, ilyenkor az 5 Ft-os kerekítést alkalmazza (ftKerekites()).
// bankar, ilyenkor az bankár kerekítést alkalmazza (bankarKerekites())
// • Amennyiben a második paramétert nem adta meg, úgy a matematikai kerekítést alkalmazza
// (matematikaiKerekites()).
// • Egyéb bemenet esetén adjon hibaüzenetet: “Ismeretlen kerekítési mód!", majd lépjen ki
// • Feltételezheti, hogy a felhasználó egész számot adott meg a ft kerekítés esetében, míg az összes
// többi esetében valós számot ad meg.

if ($argc > 3) {
    echo "Túl sok paraméter!\n";
    exit(1);
}

if ($argc < 2) {
    echo "Használat: php kerekites.php <szám> [mód]\n";
    exit(1);
}

$number = (float)$argv[1];
$method = strtolower($argv[2] ?? 'matematikai');

$result = 0;

switch ($method) {
    case 'fel':
        $result = felkerekites($number);
        break;
    case 'le':
        $result = leKerekites($number);
        break;
    case 'ft':
        $result = ftKerekites((int)$number);
        break;
    case 'bankar':
        $result = bankarKerekites($number);
        break;
    case 'matematikai':
        $result = matematikaiKerekites($number);
        break;
    default:
        echo "Ismeretlen kerekítési mód!\n";
        exit(1);
}

echo $result . "\n";

?>
