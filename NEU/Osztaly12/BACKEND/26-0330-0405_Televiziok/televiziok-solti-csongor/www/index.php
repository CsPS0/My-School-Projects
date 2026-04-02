<?php

require 'vendor/autoload.php';

use Whoops\Run;
use Whoops\Handler\PrettyPageHandler;

$whoops = new Run();
$whoops->pushHandler(new PrettyPageHandler());
$whoops->register();

use MyShop\Multimedia\Television;

require_once __DIR__ . '/data.php';

$page = $_GET['page'] ?? 'home';

if (!in_array($page, ['home', 'seller', 'products'])) {
    $page = '404';
}

$manufacturer_id = $_GET['manufacturer_id'] ?? '';
$minimum_size = $_GET['minimum_size'] ?? '';
$maximum_size = $_GET['maximum_size'] ?? '';

if ($manufacturer_id !== '' || $minimum_size !== '' || $maximum_size !== '') {
    $televisions = array_filter($televisions, function ($tv) use ($manufacturer_id, $minimum_size, $maximum_size) {
        if ($manufacturer_id !== '' && $tv->manufacturer_id != $manufacturer_id) {
            return false;
        }
        if ($minimum_size !== '' && $tv->size < $minimum_size) {
            return false;
        }
        if ($maximum_size !== '' && $tv->size > $maximum_size) {
            return false;
        }
        return true;
    });
}

if ($page === 'seller') {
    usort($televisions, function ($a, $b) {
        return $a->price <=> $b->price;
    });
} elseif ($page === 'products') {
    usort($televisions, function ($a, $b) {
        return strcmp($b->name, $a->name);
    });
}

$title = "TV " . count($televisions) . " darab";

$menuItems = [
    [
        'text' => 'Főoldal',
        'url' => 'index.php',
        'active' => $page === 'home'
    ],
    [
        'text' => 'Eladói oldal',
        'url' => 'index.php?page=seller',
        'active' => $page === 'seller'
    ],
    [
        'text' => 'Termékek',
        'url' => 'index.php?page=products',
        'active' => $page === 'products'
    ],
];

if ($page === '404') {
    header("HTTP/1.1 404 Not Found");
}

?>
<!DOCTYPE html>
<html lang="hu">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?= $title ?></title>
    <link rel="stylesheet" href="css/tv.css">
    <script src="https://cdn.jsdelivr.net/npm/@tailwindcss/browser@4"></script>
</head>

<body class="min-h-screen flex flex-col">

    <?php include __DIR__ . '/components/menu.php'; ?>

    <div class="mx-auto max-w-7xl px-4 py-10 flex-grow">
        <?php 
        if ($page !== '404' && $page !== 'home') {
            include __DIR__ . '/components/form.php';
        }
        include __DIR__ . "/pages/$page.php"; 
        ?>
    </div>

    <?php include __DIR__ . '/components/footer.php'; ?>

</body>

</html>