<?php
function hetNapja(int $napSorszam): string {
    $napok = [
        1 => "hétfő",
        2 => "kedd",
        3 => "szerda",
        4 => "csütörtök",
        5 => "péntek",
        6 => "szombat",
        7 => "vasárnap"
    ];
    return $napok[$napSorszam] ?? "Érvénytelen nap sorszám";
}


function napSorszama(string $napNev): int {
    $napok = [
        "hétfő" => 1,
        "kedd" => 2,
        "szerda" => 3,
        "csütörtök" => 4,
        "péntek" => 5,
        "szombat" => 6,
        "vasárnap" => 7
    ];
    return $napok[strtolower($napNev)] ?? 0;
}


function parosE(int $szam): bool {
    return $szam % 2 === 0;
}


function paratlanE(int $szam): bool {
    return $szam % 2 !== 0;
}


function oszthatoE(int $szam1, int $szam2): bool {
    if ($szam2 === 0) {
        return false;
    }
    return $szam1 % $szam2 === 0;
}


function negativE(float $szam): bool {
    return $szam < 0;
}


function szignum(float $szam): int {
    if ($szam < 0) {
        return -1;
    } elseif ($szam === 0.0) {
        return 0;
    } else {
        return 1;
    }
}


function datumIdo(string $resz): string {
    date_default_timezone_set('Europe/Budapest');
    switch (strtolower($resz)) {
        case "év":
            return date("Y");
        case "hónap":
            return date("m");
        case "nap":
            return date("d");
        case "óra":
            return date("H");
        case "perc":
            return date("i");
        case "másodperc":
            return date("s");
        default:
            return "Érvénytelen idő rész.";
    }
}




function utolso(array $tomb) {
    if (empty($tomb)) {
        return null;
    }
    return end($tomb);
}


function osszeg(array $tomb): int {
    return array_sum($tomb);
}


function szorzat(array $tomb): int {
    $eredmeny = 1;
    foreach ($tomb as $szam) {
        $eredmeny *= $szam;
    }
    return $eredmeny;
}


function parosDb(array $tomb): int {
    $count = 0;
    foreach ($tomb as $szam) {
        if (parosE($szam)) {
            $count++;
        }
    }
    return $count;
}


function parosOsszeg(array $tomb): int {
    $sum = 0;
    foreach ($tomb as $szam) {
        if (parosE($szam)) {
            $sum += $szam;
        }
    }
    return $sum;
}


function elsoNOsszeg(array $tomb, int $n): int {
    $sum = 0;
    for ($i = 0; $i <= $n && $i < count($tomb); $i++) {
        $sum += $tomb[$i];
    }
    return $sum;
}

?>