<?php
require_once __DIR__ . '/vendor/autoload.php';

if (class_exists('Whoops\Run')) {
    $whoops = new \Whoops\Run;
    $whoops->pushHandler(new \Whoops\Handler\PrettyPageHandler);
    $whoops->register();
}

require_once 'data.php';

$layout = 'home';
if (!empty($_GET['layout'])) {
    $layout = $_GET['layout'];
    if (!in_array($layout, ['table', 'grid'])) {
        $layout = '404';
        http_response_code(404);
    }
} else {
    $layout = 'home';
}

// Sorting
if ($layout === 'table') {
    usort($horses, fn($a, $b) => $b->year <=> $a->year);
} elseif ($layout === 'grid') {
    usort($horses, fn($a, $b) => $a->breed <=> $b->breed);
}

// Menu Items
$menuItems = [
    ['text' => 'Főoldal', 'url' => 'index.php', 'active' => $layout === 'home'],
    ['text' => 'Táblázat', 'url' => 'index.php?layout=table', 'active' => $layout === 'table'],
    ['text' => 'Rács', 'url' => 'index.php?layout=grid', 'active' => $layout === 'grid'],
];

// Title
$titles = [
    'home' => 'felsorolás',
    'table' => 'táblázat',
    'grid' => 'rács',
    '404' => '404'
];
$pageName = $titles[$layout];
$title = "Az USA államainak nemzeti lovai ($pageName)";

// Determine page file
$pageFile = $layout === 'home' ? 'list.php' : "$layout.php";

?>
<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo $title; ?></title>
    <script src="https://cdn.jsdelivr.net/npm/@tailwindcss/browser@4"></script>
</head>
<body class="min-h-screen flex flex-col">

    <?php include 'components/menu.php'; ?>

    <main class="mx-auto max-w-7xl px-4 flex-grow">
        <h1 class="my-4 text-center text-4xl font-bold text-blue-700">
            <?php echo $title; ?>
        </h1>

        <?php include "pages/$pageFile"; ?>
    </main>

    <?php include 'components/footer.php'; ?>

</body>
</html>
