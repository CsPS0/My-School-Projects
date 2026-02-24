<?php

declare(strict_types=1);

require_once 'vendor/autoload.php';

use Acme\Airliner;
use Acme\Freighter;
use Faker\Factory;

// Argument validation
if ($argc < 3) {
    echo "Az első két paraméter megadása kötelező!" . PHP_EOL;
    exit(3);
}

$type = $argv[1];
if ($type !== 'teher' && $type !== 'utas') {
    echo "Hibás paraméter!" . PHP_EOL;
    echo "Az első paraméter csak 'teher' vagy 'utas' lehet!" . PHP_EOL;
    exit(7);
}

$count = $argv[2];
if (!is_numeric($count) || (int)$count < 1) {
    echo "Hibás paraméter!" . PHP_EOL;
    echo "A második paraméternek 0-nál nagyobb számnak kell lennie!" . PHP_EOL;
    exit(2);
}
$count = (int)$count;

$outputFormat = null;
if (isset($argv[3])) {
    if ($argv[3] !== 'csv') {
        echo "Hibás" . PHP_EOL;
        echo "A harmadik paraméter csak 'csv' lehet, amennyiben meg lett adva!" . PHP_EOL;
        exit(9);
    }
    $outputFormat = 'csv';
}

$faker = Factory::create('hu_HU');
$planes = [];

for ($i = 0; $i < $count; $i++) {
    if ($type === 'utas') {
        $manufacturers = Airliner::allManufacturers();
        $models = Airliner::allModels();
        
        $manufacturer = $faker->randomElement($manufacturers);
        $model = $faker->randomElement($models);
        $flightNumber = $faker->regexify('[A-Z]{3}[0-9]{4}');
        $passengers = $faker->numberBetween(1, 500);
        $range = $faker->randomFloat(2, 2000, 10000);

        $planes[] = new Airliner($manufacturer, $model, $flightNumber, $passengers, $range);
    } else {
        $manufacturers = Freighter::allManufacturers();
        $models = Freighter::allModels();

        $manufacturer = $faker->randomElement($manufacturers);
        $model = $faker->randomElement($models);
        // regexify pattern for Freighter: AAA1111F -> 3 letters, 4 numbers, F
        $flightNumber = $faker->regexify('[A-Z]{3}[0-9]{4}F'); 
        $maxTonnage = $faker->randomFloat(2, 1, 500);
        $distanceLimit = $faker->randomFloat(2, 2000, 15000);

        $planes[] = new Freighter($manufacturer, $model, $flightNumber, $maxTonnage, $distanceLimit);
    }
}

if ($outputFormat === null) {
    foreach ($planes as $plane) {
        echo $plane . PHP_EOL;
    }
} else {
    // CSV Output
    $filename = "out/{$type}.csv";
    $file = fopen($filename, 'w');
    
    if ($file === false) {
        echo "Hiba a fájl írásakor: $filename" . PHP_EOL;
        exit(1);
    }

    foreach ($planes as $plane) {
        // Using reflection or getters to extract data for CSV would be clean, 
        // but simple property access via getters/magic getters is enough.
        // The instruction says "az adott repülők és adataik".
        // I will adhere to the properties defined in the class.
        
        $data = [];
        // Common properties
        $data[] = $plane->manufacturer;
        $data[] = $plane->model;
        $data[] = $plane->flightNumber;

        if ($type === 'utas') {
            $data[] = $plane->passengers;
            $data[] = $plane->range;
            // Maybe derived data too? Instructions are vague on CSV columns.
            // Usually raw data is best.
        } else {
            $data[] = $plane->maxTonnage;
            $data[] = $plane->distanceLimit;
        }
        
        fputcsv($file, $data);
    }
    
    fclose($file);
    echo "Fájl létrehozva: $filename" . PHP_EOL;
}
