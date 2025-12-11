<?php

require_once 'vendor/autoload.php';

$faker = Faker\Factory::create('hu_HU');

$isExpired = false;
if (isset($argv[1]) && $argv[1] === 'lejart') {
    $isExpired = true;
}

$cardType = $faker->creditCardType;

$rawCardNumber = $faker->creditCardNumber($cardType);
$digitsOnly = preg_replace('/\D/', '', $rawCardNumber);
$formattedCardNumber = implode('-', str_split($digitsOnly, 4));


if ($isExpired) {
    $date = $faker->dateTimeBetween('-5 years', 'last month');
    $expirationDate = $date->format('m/y');
} else {
    $expirationDate = $faker->creditCardExpirationDateString;
}

$ccv = $faker->randomNumber(3, true);

$name = $faker->lastName . ' ' . $faker->firstName;

echo "Kártya típusa: " . $cardType . PHP_EOL;
echo "Kártyaszám: " . $formattedCardNumber . PHP_EOL;
echo "Kártya lejárati ideje (hó/év): " . $expirationDate . PHP_EOL;
echo "CCV: " . $ccv . PHP_EOL;
echo "Név: " . $name . PHP_EOL;