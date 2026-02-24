<?php

require_once 'vendor/autoload.php';

$whoops = new \Whoops\Run;
$whoops->pushHandler(new \Whoops\Handler\PrettyPageHandler);
$whoops->register();

$page = 'home';
if (!empty($_GET['page'])) {
    $page = $_GET['page'];
    if ($page !== 'table' && $page !== 'grid') {
        $page = '404';
    }
}

$menuItems = [
    [
        'text' => 'Főoldal',
        'url' => 'index.php',
        'active' => $page === 'home'
    ],
    [
        'text' => 'Táblázat',
        'url' => 'index.php?page=table',
        'active' => $page === 'table'
    ],
    [
        'text' => 'Rács',
        'url' => 'index.php?page=grid',
        'active' => $page === 'grid'
    ]
];

require_once 'data.php';

if ($page === 'table') {
    usort($tablets, fn($a, $b) => $a->price <=> $b->price);
} elseif ($page === 'grid') {
    usort($tablets, fn($a, $b) => $b->price <=> $a->price);
}

$title = "Tabletek " . count($tablets) . " darab";

if ($page === '404') {
    http_response_code(404);
}

?>
<!DOCTYPE html>
<html lang="hu">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?= $title ?></title>
    <script src="https://cdn.jsdelivr.net/npm/@tailwindcss/browser@4"></script>
</head>

<body class="min-h-screen flex flex-col">

    <?php include 'components/menu.php'; ?>

    <main class="flex-grow">
        <?php include "pages/{$page}.php"; ?>
    </main>

    <?php include 'components/footer.php'; ?>

</body>

</html>
