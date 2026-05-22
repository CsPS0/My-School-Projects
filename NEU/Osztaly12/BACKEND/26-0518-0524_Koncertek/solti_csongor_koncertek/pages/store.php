<?php

use Event\Party\Concert;

$old = $_POST;
$errors = [];

$name = trim($_POST['name'] ?? '');
$location = trim($_POST['location'] ?? '');
$type = $_POST['type'] ?? '';
$date = $_POST['date'] ?? '';
$price = $_POST['price'] ?? '';

if (empty($name)) {
    $errors['name'] = 'A név kitöltése kötelező!';
} elseif (strlen($name) > 255) {
    $errors['name'] = 'A név nem lehet hosszabb 255 karakternél!';
}

if (empty($location)) {
    $errors['location'] = 'A helyszín kitöltése kötelező!';
} elseif (strlen($location) > 255) {
    $errors['location'] = 'A helyszín nem lehet hosszabb 255 karakternél!';
}

if (empty($type)) {
    $errors['type'] = 'A típus kitöltése kötelező!';
} elseif (!array_key_exists($type, Concert::getTypes())) {
    $errors['type'] = 'Érvénytelen típus!';
}

if (empty($date)) {
    $errors['date'] = 'A dátum kitöltése kötelező!';
} else {
    $inputDate = new DateTime($date);
    $now = new DateTime();
    if ($inputDate > $now) {
        $errors['date'] = 'A dátum nem lehet későbbi a mainál!';
    }
}

if ($price !== '' && (!is_numeric($price) || intval($price) < 0)) {
    $errors['price'] = 'Az ár csak pozitív egész szám lehet!';
}

if (empty($errors)) {
    $maxId = 0;
    foreach ($concerts as $concert) {
        if ($concert->id > $maxId) {
            $maxId = $concert->id;
        }
    }
    $newId = $maxId + 1;
    $priceValue = ($price === '') ? 0 : intval($price);

    $csvContent = file_get_contents(__DIR__ . '/../concerts.csv');
    $newLine = "$newId;$name;$location;$type;$date 00:00:00;$priceValue";
    if (substr($csvContent, -1) !== "\n") {
        $newLine = "\n" . $newLine;
    }
    file_put_contents(__DIR__ . '/../concerts.csv', $newLine . "\n", FILE_APPEND);

    header('Location: index.php?action=index&layout=table');
    exit;
} else {
    $title = 'Koncert rögzítése';
    $page = 'pages/create.php';
}
