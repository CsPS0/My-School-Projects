<?php

// 1. Hozzon létre egy tömböt szamok néven az 1, 5, 8, 17, 25, 34 értékekkel.
$szamok = [1, 5, 8, 17, 25, 34];

// 2. Határozza meg hány szám található a tömbben!
echo "2. feladat<br>";
echo count($szamok);
echo "<br><br>";

// 3. Jelenítse meg az első és az utolsó számot!
echo "3. feladat<br>";
echo "Első szám: " . $szamok[0] . "<br>";
echo "Utolsó szám: " . $szamok[count($szamok) - 1];
echo "<br><br>";

// 4. Jelenítse meg a számokat egymás után vesszővel felsorolva for ciklus segítségével.
echo "4. feladat<br>";
for ($i = 0; $i < count($szamok); $i++) {
    echo $szamok[$i];
    if ($i < count($szamok) - 1) {
        echo ", ";
    }
}
echo "<br><br>";

// 5. Jelenítse meg a páros számokat soronként foreach ciklus segítségével.
echo "5. feladat<br>";
foreach ($szamok as $szam) {
    if ($szam % 2 == 0) {
        echo $szam . "<br>";
    }
}
echo "<br>";

// 6. Jelenítse meg a számokat soronként, de fordított sorrendben tetszőleges ciklussal.
echo "6. feladat<br>";
for ($i = count($szamok) - 1; $i >= 0; $i--) {
    echo $szamok[$i] . "<br>";
}
echo "<br>";

// 7. Adja meg mennyi a számok összege!
echo "7. feladat<br>";
echo array_sum($szamok);
echo "<br><br>";

// 8. Adja meg mennyi a számok átlaga! Az átlagot két tizedesre kerekítse!
echo "8. feladat<br>";
echo round(array_sum($szamok) / count($szamok), 2);
echo "<br>";
?>
