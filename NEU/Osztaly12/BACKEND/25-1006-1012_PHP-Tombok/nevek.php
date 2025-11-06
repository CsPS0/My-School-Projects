<?php

// 1. Hozzon létre egy tömböt nevek néven a következő értékekkel:
$nevek = [
    "Robert Downey Jr.",
    "Chris Hemsworth",
    "Scarlett Johansson",
    "Karen Gillan",
    "Benedict Cumberbatch"
];

// 2. Jelenítse meg az első nevet.
echo "2. feladat<br>";
echo "Első: " . $nevek[0];
echo "<br><br>";

// 3. Jelenítse meg az utolsó nevet.
echo "3. feladat<br>";
echo "Utolsó: " . $nevek[count($nevek) - 1];
echo "<br><br>";

// 4. Készítsen felsorolást foreach segítségével a nevekből!
echo "4. feladat<br>";
foreach ($nevek as $nev) {
    echo $nev . "<br>";
}

?>
