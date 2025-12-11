<?php

require 'vendor/autoload.php';

use Neu\Iskola\Diak;
use Faker\Factory;


if (!isset($argv[1])) {
    echo "A szkriptnek nem lett megadva a kimeneti fájl neve!" . PHP_EOL;
    exit(1);
}

$filename = $argv[1];
$extension = pathinfo($filename, PATHINFO_EXTENSION);


if (!in_array($extension, ['txt', 'csv'])) {
    echo "A kimeneti fájl kiterjesztése csak `txt` vagy `csv` lehet!" . PHP_EOL;
    exit(2);
}


$count = 1;
if (isset($argv[2])) {
    if ((int)$argv[2] <= 0) {
        echo "Amennyiben a második paraméter kitöltésre kerül, úgy az minimum 1 legyen" . PHP_EOL;
        exit(3);
    }
    $count = (int)$argv[2];
}


$faker = Factory::create('hu_HU');


$diakok = [];
for ($i = 0; $i < $count; $i++) {
    $vnev = $faker->lastName();
    $knev = $faker->firstName();
    $email = $faker->email();
    $szuletett = $faker->dateTimeBetween('-30 years', '-10 years');

    $diakok[] = new Diak($vnev, $knev, $email, $szuletett);
}


$outputPath = __DIR__ . '/out/' . $filename;

if ($extension === 'txt') {
    
    $content = '';
    foreach ($diakok as $diak) {
        $content .= $diak->sorszam . PHP_EOL;
        $content .= $diak->teljes_nev . PHP_EOL;
        $content .= $diak->email . PHP_EOL;
        $content .= $diak->szuletett_iso . PHP_EOL;
    }

    file_put_contents($outputPath, $content);


    $file = fopen($outputPath, 'w');
    if ($file) {
        foreach ($diakok as $diak) {

            fputcsv($file, [
                $diak->sorszam,
                $diak->teljes_nev,
                $diak->email,
                $diak->szuletett_iso
            ], ';');
        }
        fclose($file);
    }
}