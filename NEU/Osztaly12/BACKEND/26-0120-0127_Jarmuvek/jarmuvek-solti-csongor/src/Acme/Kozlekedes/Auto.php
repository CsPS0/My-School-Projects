<?php
namespace Acme\Kozlekedes;
use Stringable;
class Auto extends Gepjarmu implements Stringable
{

    protected int $ajtok;

    public function __construct(string $gyarto, string $tipus, string $szin, string $motor, int $ajtok){
        parent::__construct($gyarto, $tipus, $szin, $motor);
        $this->ajtok = $ajtok;
    }

    public function getAjto(){
        return $this->ajtok;
    }

    public function __tostring(){
        return $this->ajtok . " ajtós ". $this->gyarto ." által gyártott " 
        . $this->motor . "-al ellátott " . $this->szin . " " . $this->tipus. " autó.";
    }
    public function toArray(): array {
        return [
            'gyarto' => $this->getGyarto(),
            'tipus'  => $this->getTipus(),
            'szin'   => $this->getSzin(),
            'motor'  => $this->getMotor(),
            'ajtok'  => $this->getAjtok()
        ];
    }
}

?>