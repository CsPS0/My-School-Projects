<?php

// 1. Hozzon létre egy függvényt fogyasztas néven. A függvénynek két paramétere van:
// • km a legutbbi tankols ta megtett t kilomterben (egsz)
// • liter a jelenlegi tankolsnl hny litert tanoltak (vals)
// A fggvny meghatrozza az tlagfogyaszts, az eredmnyt kt tizedesre kerektve adja vissza. Az
// tlagfogyaszts azt meghatrozza, hogy 100 km megttshez hny liter zemanyagot hasznl fel az
// aut.
function fogyasztas(int $km, float $liter): float {
    if ($km <= 0) {
        return 0.0; // Vagy dobjon kivtelt
    }
    return round(($liter / $km) * 100, 2);
}

// 2. Hozzon ltre egy fjlt fogyasztas.php nven. A fjlnak hrom bemenete van.
// • Mennyi volt a kilomter ra lls a legutbbi tankolskor
// • Mennyi a kilomter ra llsa jelenleg
// • Most hny litert sikerlt tankolni a jrmbe
// Az első kt adatbl kiszmthat, hogy mennyit tett meg az aut, ennek a felhasznlsval s a tankolt
// mennyisgbl megllapthat az tlagfogyasztst. Az eredmnyeket a minta szerint jelentse meg!

if ($argc < 4) {
    echo "Hasznlat: php fogyasztas.php <előz ralls> <mostani ralls> <tankolt zemanyag>\n";
    exit(1);
}

$elozOOrallas = (int)$argv[1];
$mostaniOrallas = (int)$argv[2];
$tankoltUzemanyag = (float)$argv[3];

$megtettUt = $mostaniOrallas - $elozOOrallas;
$atlagFogyasztas = fogyasztas($megtettUt, $tankoltUzemanyag);

echo "Előz ralls: " . number_format($elozOOrallas, 0, '.', ' ') . " km\n";
echo "Mostani ralls: " . number_format($mostaniOrallas, 0, '.', ' ') . " km\n";
echo "Megtett t: " . number_format($megtettUt, 0, '.', ' ') . " km\n";
echo "Tankolt zemanyag: " . number_format($tankoltUzemanyag, 1, '.', '') . " liter\n";
echo "tlagfogyaszts: " . number_format($atlagFogyasztas, 2, '.', '') . " liter/100km\n";

?>