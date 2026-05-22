<?php

declare(strict_types=1);

require_once __DIR__ . '/vendor/autoload.php';

$whoops = new \Whoops\Run;
$whoops->pushHandler(new \Whoops\Handler\PlainTextHandler);
$whoops->register();

require_once __DIR__ . '/data.php';

$page = $_GET['page'] ?? 'home';
$id = $_GET['id'] ?? null;

$filteredTeas = $teas;

$nameFilter = $_GET['name'] ?? null;
if ($nameFilter) {
    $filteredTeas = array_filter($filteredTeas, function($tea) use ($nameFilter) {
        return mb_stripos($tea->name, $nameFilter) !== false;
    });
}

$rangeFilter = $_GET['range'] ?? null;
if ($rangeFilter) {
    $filteredTeas = array_filter($filteredTeas, function($tea) use ($rangeFilter) {
        return $tea->range === $rangeFilter;
    });
}

$qtyMin = isset($_GET['qty_min']) ? (int)$_GET['qty_min'] : null;
if ($qtyMin !== null) {
    $filteredTeas = array_filter($filteredTeas, function($tea) use ($qtyMin) {
        return $tea->qty >= $qtyMin;
    });
}

$qtyMax = isset($_GET['qty_max']) ? (int)$_GET['qty_max'] : 1000;
if (isset($_GET['qty_max'])) {
    $qtyMax = (int)$_GET['qty_max'];
    $filteredTeas = array_filter($filteredTeas, function($tea) use ($qtyMax) {
        return $tea->qty <= $qtyMax;
    });
}

if ($page === 'table') {
    usort($filteredTeas, function($a, $b) {
        return strcasecmp($a->name, $b->name);
    });
} elseif ($page === 'cards') {
    usort($filteredTeas, function($a, $b) {
        return $b->price <=> $a->price;
    });
}

$bgColor = '#fef3c7';
if ($rangeFilter === 'green') {
    $bgColor = '#afd7a4';
} elseif ($rangeFilter === 'black') {
    $bgColor = '#e3e1e1';
} elseif ($rangeFilter === 'fruit') {
    $bgColor = '#f7d4d4';
}

$pageFile = __DIR__ . "/pages/{$page}.php";
if ($page === 'tea') {
    if (!$id || !isset($teas[$id])) {
        $page = '404';
        $pageFile = __DIR__ . '/pages/404.php';
        http_response_code(404);
    }
} elseif (!file_exists($pageFile)) {
    $page = '404';
    $pageFile = __DIR__ . '/pages/404.php';
    http_response_code(404);
}

$title = "Teák";
if ($page === 'table' || $page === 'cards') {
    $count = count($filteredTeas);
    $title .= " ({$count} találat)";
}

?>
<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo $title; ?></title>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="css/teas.css">
    <style>
        body { background-color: <?php echo $bgColor; ?>; }
    </style>
</head>
<body class="min-h-screen flex flex-col font-sans">

    <?php include __DIR__ . '/components/menu.php'; ?>

    <main class="container mx-auto p-4 flex-grow">
        <h1 class="text-4xl text-center my-8 text-gray-800"><?php echo $title; ?></h1>
        <?php include $pageFile; ?>
    </main>

</body>
</html>
