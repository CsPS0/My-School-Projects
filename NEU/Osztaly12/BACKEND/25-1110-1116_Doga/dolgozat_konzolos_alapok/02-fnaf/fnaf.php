<?php

require_once 'data.php';

function average(array $nights): float {
    $totalFear = 0;
    $eventCount = 0;
    foreach ($nights as $events) {
        foreach ($events as $event) {
            $totalFear += $event['fear'];
            $eventCount++;
        }
    }
    return $eventCount > 0 ? $totalFear / $eventCount : 0;
}

function animatronic(int $id, array $nights): ?string {
    foreach ($nights as $events) {
        foreach ($events as $event) {
            if ($event['id'] === $id) {
                return $event['animatronic'];
            }
        }
    }
    return null;
}

function scariest(array $nights): ?array {
    $scariestEvent = null;
    $maxFear = -1;
    foreach ($nights as $events) {
        foreach ($events as $event) {
            if ($event['fear'] > $maxFear) {
                $maxFear = $event['fear'];
                $scariestEvent = $event;
            }
        }
    }
    return $scariestEvent;
}

if ($argc > 2) {
    echo "Túl sok paraméter!" . "\n";
    exit(3);
}

$param = $argv[1] ?? null;

if ($param === null) {
    foreach ($nights as $nightName => $events) {
        foreach ($events as $event) {
            echo "({$event['id']}) {$event['animatronic']} - {$event['room']} - ({$event['fear']}) - {$event['time']} [{$nightName}]" . "\n";
        }
    }
    exit(0);
}

if ($param === 'atlag' || $param === 'átlag') {
    echo average($nights) . "\n";
} elseif (is_numeric($param) && $param >= 1 && $param <= 20) {
    $name = animatronic((int)$param, $nights);
    if ($name !== null) {
        echo $name . "\n";
    } else {
        echo "Nincs ilyen azonosítójú feljegyzés!" . "\n";
    }
} elseif ($param === 'scary') {
    $event = scariest($nights);
    if ($event !== null) {
        echo "Animatronik: {$event['animatronic']}" . "\n";
        echo "Szoba: {$event['room']}" . "\n";
        echo "Félelmetesség: {$event['fear']}" . "\n";
        echo "Időpont: {$event['time']}" . "\n";
    }
} else {
    echo "Ismeretlen paraméter!" . "\n";
    exit(1);
}

exit(0);