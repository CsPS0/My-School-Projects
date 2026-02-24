<?php
namespace Acme\Kozlekedes;
use Stringable;
class Roller extends Jarmu implements Stringable
{

    protected int $kerekSzam;

    public function __construct(string $gyarto, string $tipus, string $szin, int $kerekSzam){
        parent::__construct($gyarto, $tipus, $szin);
        $this->kerekSzam = $kerekSzam;
    }

    public function getKerekSzam(){
        return $this->kerekSzam;
    }

    public function __tostring(){
        return $this->kerekSzam . " kerékkel ellátott ". $this->gyarto ." által gyártott " 
        . $this->szin . " " . $this->tipus. " roller.";
    }
    public function toArray(): array {
        return [
            'gyarto' => $this->getGyarto(),
            'tipus'  => $this->getTipus(),
            'szin'   => $this->getSzin(),
            'kerekSzam'  => $this->getKerekSzam()
        ];
    }
}

?>