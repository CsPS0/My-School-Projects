<?php

require_once 'fuggvenyek.php';

echo "<h2>Egyszerű függvények tesztelése</h2>";

// 1. hetNapja
echo "<h3>hetNapja(int \$napSorszam)</h3>";
echo "hetNapja(1): " . hetNapja(1) . " (elvárás: hétfő)<br>";
echo "hetNapja(5): " . hetNapja(5) . " (elvárás: péntek)<br>";
echo "hetNapja(8): " . hetNapja(8) . " (elvárás: Érvénytelen nap sorszám)<br>";

// 2. napSorszama
echo "<h3>napSorszama(string \$napNev)</h3>";
echo "napSorszama(\"hétfő\"): " . napSorszama("hétfő") . " (elvárás: 1)<br>";
echo "napSorszama(\"péntek\"): " . napSorszama("péntek") . " (elvárás: 5)<br>";
echo "napSorszama(\"ismeretlen\"): " . napSorszama("ismeretlen") . " (elvárás: 0)<br>";

// 3. parosE
echo "<h3>parosE(int \$szam)</h3>";
echo "parosE(5): "; var_dump(parosE(5)); echo " (elvárás: bool(false))<br>";
echo "parosE(8): "; var_dump(parosE(8)); echo " (elvárás: bool(true))<br>";

// 4. paratlanE
echo "<h3>paratlanE(int \$szam)</h3>";
echo "paratlanE(5): "; var_dump(paratlanE(5)); echo " (elvárás: bool(true))<br>";
echo "paratlanE(8): "; var_dump(paratlanE(8)); echo " (elvárás: bool(false))<br>";

// 5. oszthatoE
echo "<h3>oszthatoE(int \$szam1, int \$szam2)</h3>";
echo "oszthatoE(5,5): "; var_dump(oszthatoE(5,5)); echo " (elvárás: bool(true))<br>";
echo "oszthatoE(8,5): "; var_dump(oszthatoE(8,5)); echo " (elvárás: bool(false))<br>";
echo "oszthatoE(10,0): "; var_dump(oszthatoE(10,0)); echo " (elvárás: bool(false))<br>";

// 6. negativE
echo "<h3>negativE(float \$szam)</h3>";
echo "negativE(-3): "; var_dump(negativE(-3)); echo " (elvárás: bool(true))<br>";
echo "negativE(96): "; var_dump(negativE(96)); echo " (elvárás: bool(false))<br>";

// 7. szignum
echo "<h3>szignum(float \$szam)</h3>";
echo "szignum(-836): " . szignum(-836) . " (elvárás: -1)<br>";
echo "szignum(0): " . szignum(0) . " (elvárás: 0)<br>";
echo "szignum(1024): " . szignum(1024) . " (elvárás: 1)<br>";

// 8. datumIdo
echo "<h3>datumIdo(string \$resz)</h3>";
echo "datumIdo(\"óra\"): " . datumIdo("óra") . " (elvárás: aktuális óra, pl. 08)<br>";
echo "datumIdo(\"perc\"): " . datumIdo("perc") . " (elvárás: aktuális perc, pl. 11)<br>";
echo "datumIdo(\"másodperc\"): " . datumIdo("másodperc") . " (elvárás: aktuális másodperc, pl. 08)<br>";
echo "datumIdo(\"év\"): " . datumIdo("év") . " (elvárás: aktuális év, pl. 2022)<br>";
echo "datumIdo(\"hónap\"): " . datumIdo("hónap") . " (elvárás: aktuális hónap, pl. 09)<br>";
echo "datumIdo(\"nap\"): " . datumIdo("nap") . " (elvárás: aktuális nap, pl. 06)<br>";
echo "datumIdo(\"ismeretlen\"): " . datumIdo("ismeretlen") . " (elvárás: Érvénytelen idő rész.)<br>";

echo "<h2>Függvények tömbökön tesztelése</h2>";

// 1. utolso
echo "<h3>utolso(array \$tomb)</h3>";
echo "utolso([5,11,76,3]): " . utolso([5,11,76,3]) . " (elvárás: 3)<br>";
echo "utolso([]): "; var_dump(utolso([])); echo " (elvárás: NULL)<br>";

// 2. osszeg
echo "<h3>osszeg(array \$tomb)</h3>";
echo "osszeg([5,11,76,3]): " . osszeg([5,11,76,3]) . " (elvárás: 95)<br>";
echo "osszeg([]): " . osszeg([]) . " (elvárás: 0)<br>";

// 3. szorzat
echo "<h3>szorzat(array \$tomb)</h3>";
echo "szorzat([5,11,76,3]): " . szorzat([5,11,76,3]) . " (elvárás: 12540)<br>";
echo "szorzat([]): " . szorzat([]) . " (elvárás: 1)<br>";

// 4. parosDb
echo "<h3>parosDb(array \$tomb)</h3>";
echo "parosDb([]): " . parosDb([]) . " (elvárás: 0)<br>";
echo "parosDb([5,11,76,3]): " . parosDb([5,11,76,3]) . " (elvárás: 1)<br>";
echo "parosDb([37,74,3,71,54]): " . parosDb([37,74,3,71,54]) . " (elvárás: 2)<br>";

// 5. parosOsszeg
echo "<h3>parosOsszeg(array \$tomb)</h3>";
echo "parosOsszeg([]): " . parosOsszeg([]) . " (elvárás: 0)<br>";
echo "parosOsszeg([5,11,76,3]): " . parosOsszeg([5,11,76,3]) . " (elvárás: 76)<br>";
echo "parosOsszeg([37,74,3,71,54]): " . parosOsszeg([37,74,3,71,54]) . " (elvárás: 128)<br>";

// 6. elsoNOsszeg
echo "<h3>elsoNOsszeg(array \$tomb, int \$n)</h3>";
echo "elsoNOsszeg([5,11,76,3],2): " . elsoNOsszeg([5,11,76,3],2) . " (elvárás: 16)<br>";
echo "elsoNOsszeg([37,74,3,71,54],3): " . elsoNOsszeg([37,74,3,71,54],3) . " (elvárás: 114)<br>";
echo "elsoNOsszeg([1,2,3], 5): " . elsoNOsszeg([1,2,3], 5) . " (elvárás: 6 - túlindexelés teszt)<br>";

?>
