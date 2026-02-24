<?php

namespace Neu\Eszkozok;

class Nyomtato
{
    protected string $gyarto;
    protected string $tipus;
    protected bool $szines;
    protected int $ar;

    private static array $gyartok = ['HP', 'Canon', 'Xerox', 'Epson'];

    public function __construct(string $gyarto, string $tipus, bool $szines, int $ar)
    {
        $this->gyarto = $gyarto;
        $this->tipus = $tipus;
        $this->szines = $szines;
        $this->ar = $ar;
    }

    public static function getGyartok(): array
    {
        return self::$gyartok;
    }

    public function __get($tulajdonsag)
    {
        if (property_exists($this, $tulajdonsag)) {
            return $this->$tulajdonsag;
        }
        return null;
    }
}