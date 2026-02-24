<?php

namespace Acme\Kozlekedes;

use Stringable;
class Busz extends Gepjarmu implements Stringable
{

    protected int $ulesek;

    public function __construct(string $gyarto, string $tipus, string $szin, string $motor, int $ulesek){
        parent::__construct($gyarto, $tipus, $szin, $motor);
        $this->ulesek = $ulesek;
    }

    public function getUlesek(){
        return $this->ulesek;
    }

    public function __tostring(){
        return $this->ulesek . " üléssel ellátott ". $this->gyarto ." által gyártott " 
        . $this->motor . " " . $this->szin . " " . $this->tipus. " busz.";
    }

    public function toArray(): array {
        return [
            'gyarto' => $this->getGyarto(),
            'tipus'  => $this->getTipus(),
            'szin'   => $this->getSzin(),
            'motor'  => $this->getMotor(),
            'ulesek'  => $this->getUlesek()
        ];
    }
}

?>