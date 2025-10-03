<?php
if ($argc < 3) {
    echo "Usage: php f04_ciklus4.php <A> <B>
";
    exit(1);
}

$A = (int)$argv[1];
$B = (int)$argv[2];

if ($B > $A) {
    for ($i = $A; $i <= $B; $i++) {
        if ($i % 2 != 0) {
            echo $i . "
";
        }
    }
} else {
    for ($i = $A; $i >= $B; $i--) {
        if ($i % 2 != 0) {
            echo $i . "
";
        }
    }
}
?>