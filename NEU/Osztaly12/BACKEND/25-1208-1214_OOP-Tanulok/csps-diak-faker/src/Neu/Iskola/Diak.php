<?php

namespace Neu\Iskola;

use DateTime;

class Diak
{
    private string $vnev;
    private string $knev;
    private string $email;
    private DateTime $szuletett;
    private static int $szamlalo = 0;
    private int $sorszam;

    public function __construct(string $vnev, string $knev, string $email, DateTime $szuletett)
    {
        $this->vnev = $vnev;
        $this->knev = $knev;
        $this->email = $email;
        $this->szuletett = $szuletett;
        self::$szamlalo++;
        $this->sorszam = self::$szamlalo;
    }

    public function __get(string $nev)
    {
        if ($nev === 'teljes_nev') {
            return $this->vnev . ' ' . $this->knev;
        }

        if ($nev === 'szuletett_iso') {
            return $this->szuletett->format('Y-m-d');
        }

        if (property_exists($this, $nev)) {
            return $this->$nev;
        }

        return null;
    }

    public function __set($nev, $ertek): void
    {
        if (property_exists($this, $nev)) {
            $this->$nev = $ertek;
        }
    }
}