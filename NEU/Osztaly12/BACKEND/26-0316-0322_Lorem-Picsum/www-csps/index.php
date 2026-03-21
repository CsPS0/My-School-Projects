<?php
require_once __DIR__ . '/vendor/autoload.php';

$whoops = new \Whoops\Run;
$whoops->pushHandler(new \Whoops\Handler\PrettyPageHandler);
$whoops->register();

require_once __DIR__ . '/data.php';

$id = isset($_GET['id']) && is_numeric($_GET['id']) && $_GET['id'] >= 1 ? (int)$_GET['id'] : 678;
$width = isset($_GET['width']) && is_numeric($_GET['width']) && $_GET['width'] >= 100 ? (int)$_GET['width'] : 400;
$height = isset($_GET['height']) && is_numeric($_GET['height']) && $_GET['height'] >= 100 ? (int)$_GET['height'] : 320;
$blur = isset($_GET['blur']) && is_numeric($_GET['blur']) && $_GET['blur'] >= 0 && $_GET['blur'] <= 3 ? (int)$_GET['blur'] : 0;
$grayscale = isset($_GET['grayscale']);

$imageUrl = "https://picsum.photos/id/{$id}/{$width}/{$height}";
$query = [];
if ($blur > 0) $query['blur'] = $blur;
if ($grayscale) $query['grayscale'] = '';

if (!empty($query)) {
    $imageUrl .= "?" . http_build_query($query);
    $imageUrl = str_replace('grayscale=', 'grayscale', $imageUrl);
}

$themeColor = isset($colors[$id]) ? $colors[$id] : '#6b6b40';

?>
<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Lorem Picsum képválasztó</title>
    <link rel="stylesheet" href="style.css">
    <style>
        :root {
            --primary: <?php echo $themeColor; ?>;
        }
        h1 {
            color: <?php echo $themeColor; ?> !important;
        }
        .navbar {
            background-color: <?php echo $themeColor; ?> !important;
        }
    </style>
</head>
<body>
    <?php include 'components/menu.php'; ?>
    
    <main>
        <h1>Lorem Picsum képválasztó</h1>
        <div>
            <?php include 'components/form.php'; ?>
            
            <aside>
                <?php if ($imageUrl): ?>
                    <img src="<?php echo $imageUrl; ?>" alt="Lorem Picsum">
                <?php else: ?>
                    <p>Lorem Picsum</p>
                <?php endif; ?>
            </aside>
        </div>
    </main>
</body>
</html>
