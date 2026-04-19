<?php

require_once __DIR__ . '/vendor/autoload.php';

$whoops = new \Whoops\Run;
$whoops->pushHandler(new \Whoops\Handler\PrettyPageHandler);
$whoops->register();

use Nasa\Programs\Mission;

$missions = require_once __DIR__ . '/data.php';

$programFilter = $_GET['program'] ?? 'Összes program';
$sortField = $_GET['sort'] ?? 'launchDate';
$sortOrder = $_GET['order'] ?? 'ASC';
$view = $_GET['view'] ?? 'grid';

if (!in_array($view, ['grid', 'table'])) {
    $view = 'grid';
}

if ($programFilter !== 'Összes program') {
    $missions = array_filter($missions, function(Mission $m) use ($programFilter) {
        return $m->program === $programFilter;
    });
}

usort($missions, function(Mission $a, Mission $b) use ($sortField, $sortOrder) {
    $valA = $a->$sortField;
    $valB = $b->$sortField;
    
    if ($sortOrder === 'ASC') {
        return $valA <=> $valB;
    } else {
        return $valB <=> $valA;
    }
});
?>
<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Nasa Űrprogramok</title>
    <script src="https://cdn.jsdelivr.net/npm/@tailwindcss/browser@4"></script>
</head>
<body class="bg-slate-900 text-slate-100 font-sans antialiased m-0 p-0">
    
    <?php include __DIR__ . '/components/menu.php'; ?>

    <main class="max-w-7xl mx-auto my-8 px-4">
        
        <?php include __DIR__ . '/components/form.php'; ?>

        <section class="content">
            <?php include __DIR__ . "/pages/$view.php"; ?>
        </section>
    </main>

</body>
</html>
