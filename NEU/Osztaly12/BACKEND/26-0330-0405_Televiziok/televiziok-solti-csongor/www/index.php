<?php
require 'vendor/autoload.php';

$whoops = new \Whoops\Run;
$whoops->pushHandler(new \Whoops\Handler\PrettyPageHandler);
$whoops->register();

use Budapest\Transport\TramLine;

require 'data.php';

$terminusFilter = $_GET['terminus'] ?? '';
$interconnectedFilter = $_GET['interconnected'] ?? '';
$sinceFilter = $_GET['since'] ?? '';

$filteredLines = $lines;

if (!empty($terminusFilter)) {
    $filteredLines = array_filter($filteredLines, fn($line) => str_contains(strtolower($line->route), strtolower($terminusFilter)));
}

if (!empty($interconnectedFilter)) {
    $filteredLines = array_filter($filteredLines, fn($line) => $line->interconnected === $interconnectedFilter);
}

if (!empty($sinceFilter)) {
    $filteredLines = array_filter($filteredLines, fn($line) => $line->since <= (int)$sinceFilter);
    usort($filteredLines, fn($a, $b) => $a->since <=> $b->since);
}

$allowedPages = ["grid", "table"];
$page = $_GET['page'] ?? 'grid';
if (!in_array($page, $allowedPages)) {
    $page = 'grid';
}

$menuItems = [
    "grid" => "Rács",
    "table" => "Táblázat"
];

?>
<!DOCTYPE html>
<html lang="hu">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Viszonylatok</title>
    <script src="https://cdn.tailwindcss.com/3.4.3"></script>
    <link rel="stylesheet" href="style.css">
</head>

<body class="bg-gray-100 p-4">
    
    <header class="bg-white shadow rounded-lg mb-4">
        <div class="w-11/12 mx-auto max-w-7xl p-4 sm:p-6 lg:p-8 flex justify-between items-center">
            <h1 class="text-3xl font-bold tracking-tight text-gray-900">Viszonylatok</h1>
            <?php include('components/menu.php'); ?>
        </div>
    </header>

    <main class="w-11/12 mx-auto max-w-7xl p-4 sm:p-6 lg:p-8 bg-white shadow rounded-lg">
        <?php include('components/form.php'); ?>
        <div class="mt-4 border-t pt-4">
            <?php include("pages/{$page}.php"); ?>
        </div>
    </main>

</body>
</html>
