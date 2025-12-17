<?php

declare(strict_types=1);

require_once __DIR__ . '/vendor/autoload.php';

date_default_timezone_set('Europe/Budapest');

use Acme\Iskola\Jegy as IskolaJegy;
use Acme\Mozi\Jegy as MoziJegy;
use Faker\Factory;

if ($argc < 3) {
    echo "Az első két paraméter megadása kötelező!" . PHP_EOL;
    exit(3);
}

$type = $argv[1];
$count = (int)$argv[2];
$format = $argv[3] ?? null;

if (!in_array($type, ['mozi', 'osztalyzat'])) {
    echo "Nem megfelelő paraméter!" . PHP_EOL;
    echo "Az első paraméter csak 'busz', 'mozi', 'osztalyzat' vagy 'repulo' lehet!" . PHP_EOL; // Keeping strict to the requested error message even if 'busz'/'repulo' not implemented
    exit(7);
}

if (!is_numeric($argv[2]) || $count < 1) {
    echo "Nem megfelelő paraméter!" . PHP_EOL;
    echo "A második paraméternek 0-nál nagyobb számnak kell lennie!" . PHP_EOL;
    exit(2);
}

if ($format !== null && !in_array($format, ['csv', 'json'])) {
    echo "Nem megfelelő paraméter!" . PHP_EOL;
    echo "A harmadik paraméter csak 'csv' vagy 'json' lehet amennyiben meg lett adva!" . PHP_EOL;
    exit(9);
}

$faker = Factory::create('hu_HU');
$jegyek = [];

if ($type === 'osztalyzat') {
    $possibleTipusok = IskolaJegy::lehetsegesTipusok();
    $possibleTantargyak = IskolaJegy::lehetsegesTantargyak();

    for ($i = 0; $i < $count; $i++) {
        $tipus = $faker->randomElement($possibleTipusok);
        $jegyVal = $faker->numberBetween(1, 5);
        $tantargy = $faker->randomElement($possibleTantargyak);
        $tanar = $faker->name();
        $beirva = $faker->dateTimeBetween('-2 weeks', 'now');

        $jegyek[] = new IskolaJegy($tipus, $jegyVal, $tantargy, $tanar, $beirva);
    }
} elseif ($type === 'mozi') {
    $possibleTermek = MoziJegy::termekNevei();

    for ($i = 0; $i < $count; $i++) {
        $cim = $faker->text(40);
        
        $k = $faker->numberBetween(0, 19);
        $ar = 990 + ($k * 1000);
        
        $terem = $faker->randomElement($possibleTermek);
        
        $sor = strtoupper($faker->randomLetter());
        
        $ules = $faker->numberBetween(1, 60);
        
        $kezdes = $faker->dateTimeBetween('tomorrow', '+30 days');
        
        $felnott = $faker->boolean();

        $jegyek[] = new MoziJegy($cim, $ar, $terem, $sor, $ules, $kezdes, $felnott);
    }
}

if ($format === null) {
    foreach ($jegyek as $jegy) {
        echo $jegy . PHP_EOL;
    }
} else {
    $filename = __DIR__ . '/out/' . $type . '.' . $format;
    
    if ($format === 'csv') {
        $fp = fopen($filename, 'w');
        foreach ($jegyek as $jegy) {
            fputcsv($fp, $jegy->toArray(false), ';');
        }
        fclose($fp);
    } elseif ($format === 'json') {
        $data = [];
        foreach ($jegyek as $jegy) {
            $data[] = $jegy->toArray(true);
        }
        file_put_contents($filename, json_encode($data, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE));
    }
}
