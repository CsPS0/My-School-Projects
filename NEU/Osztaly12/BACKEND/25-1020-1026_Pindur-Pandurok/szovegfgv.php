<?php


$pp = [
    "sziporka" => "Blossom",
    "puszedli" => "Bubbles",
    "csuporka" => "Buttercup",
    "pukkancs" => "Bliss",
    "nyuszi" => "Bunny"
];


echo "Pindúr Pandúrok (The Powerpuff Girls)\n";



if (isset($argv[1])) {
    $input_names_string = $argv[1];

    
    $hungarian_names = explode(';', $input_names_string);

    $english_names = [];

    foreach ($hungarian_names as $name) {

        $lowercase_name = strtolower($name);


        if (array_key_exists($lowercase_name, $pp)) {

            $english_names[] = $pp[$lowercase_name];
        }
    }



    echo implode("\n", $english_names) . "\n";

} else {
    echo "Hiba a futtatás során!\n";
}

?>