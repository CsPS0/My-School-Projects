<?php
namespace Acme\Kozlekedes;

abstract class Jarmu{
    protected string $gyarto;
    protected string $tipus;
    protected string $szin;

    protected function __construct(string $gyarto, string $tipus, string $szin)
    {
        $this->gyarto = $gyarto;
        $this->tipus = $tipus;
        $this->szin = $szin;
    }

    public function getGyarto(): string
    {
        return $this->gyarto;
    }

    public function getTipus(): string
    {
        return $this->tipus;
    }

    public function getSzin(): string
    {
        return $this->szin;
    }
}
?>