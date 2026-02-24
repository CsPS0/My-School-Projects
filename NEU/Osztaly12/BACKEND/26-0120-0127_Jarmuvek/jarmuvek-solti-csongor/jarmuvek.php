<?php
declare(strict_types=1);
require_once __DIR__ . "/vendor/autoload.php";
use Acme\Kozlekedes\Busz;
use Acme\Kozlekedes\Auto;
use Acme\Kozlekedes\Roller;

if($argc > 3){
    echo "Tól sok paraméter!!!\n";
    exit(1);
}
$tipusok = ["busz","roller","auto"];
$tipus = $argv[1] ?? null;
if($tipus != null && !in_array(mb_strtolower($tipus),$tipusok)) {
    echo "Nem megfelelő típus!!!\n";
    exit(1);
}

$fajl = $argv[2] ?? null;
if($fajl != null && !str_ends_with($fajl,".csv") )
{
    echo "Nem megfelelő fájl kimenet!!!\n";
    exit(1);
}
$tomb = [];
$tomb[] = new Busz("Ikarus","280","kék","diesel",36);
$tomb[] = new Auto("Tesla","Model S","fehér","elektromos",5);
$tomb[] = new Roller("Blackwheels","Blink gyerek roller","színes",3);

if($argc === 1){
    foreach($tomb as $jarmu){
        echo $jarmu->getGyarto(). " által gyártott ". $jarmu->getTipus() . " típusú " . $jarmu->getSzin() . " színű jármű.\n";
    }
}
else if ($argc === 2){
    foreach($tomb as $jarmu)
        {
            switch($tipus){
                case 'auto':
                    if ($jarmu instanceof Auto) echo $jarmu . "\n";
                    break;
                case 'busz':
                    if ($jarmu instanceof Busz) echo $jarmu . "\n";
                    break;
                default:
                    if ($jarmu instanceof Roller) echo $jarmu . "\n";
                    break;
            }
        }
}
else{
    $outdir = __DIR__ . "/out";
    $file = $outdir ."/". $fajl;
    $fp = fopen($file,"w");
    foreach($tomb as $jarmu)
        {
            switch($tipus){
                case 'auto':
                    if ($jarmu instanceof Auto) fputcsv($fp,$jarmu->toArray());
                    break;
                case 'busz':
                    if ($jarmu instanceof Busz) fputcsv($fp,$jarmu->toArray());
                    break;
                default:
                    if ($jarmu instanceof Roller) fputcsv($fp,$jarmu->toArray());
                    break;
            }
        }
        
        fclose($fp);
}

?>