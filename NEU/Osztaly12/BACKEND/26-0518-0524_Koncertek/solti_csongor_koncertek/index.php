<?php
require __DIR__ . '/vendor/autoload.php';

use Event\Party\Concert;

$whoops = new \Whoops\Run;
$whoops->pushHandler(new \Whoops\Handler\PrettyPageHandler);
$whoops->register();

$concerts = [];
$file = __DIR__ . '/concerts.csv';
if (file_exists($file)) {
    $rows = array_map(fn($line) => str_getcsv($line, ';'), file($file));
    $header = array_shift($rows);
    foreach ($rows as $row) {
        if (count($row) < 6) continue;
        $concerts[] = new Concert(
            (int)$row[0],
            $row[1],
            $row[2],
            $row[3],
            $row[4],
            (int)$row[5]
        );
    }
}

$action = $_GET['action'] ?? 'index';
$layout = $_GET['layout'] ?? 'grid';
$id = $_GET['id'] ?? null;

$title = 'Koncertek';
$page = 'pages/grid.php';
$errors = [];
$old = [];

switch ($action) {
    case 'index':
        if ($layout === 'table') {
            $page = 'pages/table.php';
        } else {
            $page = 'pages/grid.php';
        }
        break;
    case 'show':
        $selectedConcert = null;
        foreach ($concerts as $concert) {
            if ($concert->id == $id) {
                $selectedConcert = $concert;
                break;
            }
        }
        if ($selectedConcert) {
            $title = $selectedConcert->name;
            $page = 'pages/show.php';
        } else {
            $page = 'pages/404.php';
        }
        break;
    case 'create':
        $title = 'Koncert rögzítése';
        $page = 'pages/create.php';
        break;
    case 'store':
        require 'pages/store.php';
        break;
    case '404':
        $page = 'pages/404.php';
        break;
    default:
        $page = 'pages/grid.php';
        break;
}

ob_start();
?>


<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?= $title ?></title>
    <link rel="shortcut icon" href="public/favicon.ico" type="image/x-icon">
    <script src="https://cdn.jsdelivr.net/npm/@tailwindcss/browser@4"></script>
</head>
<body class="bg-gray-50">
    <?php
        require 'components/header.php';
        require 'components/menu.php';
    ?>
    
    <main class="w-11/12 max-w-340 mx-auto my-4">
        <?php
            if (file_exists($page)) {
                require $page;
            } else {
                require 'pages/404.php';
            }
        ?>
    </main>

</body>
</html>
<?php
ob_end_flush();