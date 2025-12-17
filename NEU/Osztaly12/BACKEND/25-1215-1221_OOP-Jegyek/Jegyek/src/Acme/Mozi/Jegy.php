<?php

namespace Acme\Mozi;

use DateTime;
use Stringable;

class Jegy implements Stringable
{
    private static array $termek = ["Spielberg", "Bujtor", "Coppola", "Hitchcock"];

    private string $cim;
    private int $ar;
    private string $terem;
    private string $sor;
    private int $ules;
    private DateTime $kezdes;
    private bool $felnott;

    public function __construct(string $cim, int $ar, string $terem, string $sor, int $ules, DateTime $kezdes, bool $felnott)
    {
        $this->cim = $cim;
        $this->ar = $ar;
        $this->terem = $terem;
        $this->sor = $sor;
        $this->ules = $ules;
        $this->kezdes = $kezdes;
        $this->felnott = $felnott;
    }

    public static function termekNevei(): array
    {
        return self::$termek;
    }

    public function __get(string $name): mixed
    {
        if (property_exists($this, $name)) {
            return $this->$name;
        }
        return null;
    }

    public function __set(string $name, mixed $value): void
    {
        if (property_exists($this, $name)) {
            $this->$name = $value;
        }
    }

    public function __toString(): string
    {
        // LOGAN 1 990 Ft [Spielberg terem F sor 9. ülés] 2023-12-12 18:00 (18+)
        $formattedPrice = number_format($this->ar, 0, '.', ' ');
        $adultStr = $this->felnott ? ' (18+)' : '';
        
        return sprintf(
            "%s %s Ft [%s terem %s sor %d. ülés] %s%s",
            strtoupper($this->cim),
            $formattedPrice,
            $this->terem,
            $this->sor,
            $this->ules,
            $this->kezdes->format('Y-m-d H:i'),
            $adultStr
        );
    }

    public function toArray(bool $asszociativ = false): array
    {
        $formattedPrice = number_format($this->ar, 0, '.', ' ') . ' Ft';
        $felnottStr = $this->felnott ? 'igen' : 'nem';

        if ($asszociativ) {
            return [
                'cim' => $this->cim,
                'ar' => $formattedPrice,
                'terem' => $this->terem,
                'sor' => $this->sor,
                'ules' => $this->ules,
                'kezdes' => $this->kezdes->format('Y-m-d H:i:s'),
                'felnott' => $felnottStr,
            ];
        }

        return [
            $this->cim,
            $formattedPrice,
            $this->terem,
            $this->sor,
            $this->ules,
            $this->kezdes->format('Y-m-d H:i:s'),
            $felnottStr,
        ];
    }
}
