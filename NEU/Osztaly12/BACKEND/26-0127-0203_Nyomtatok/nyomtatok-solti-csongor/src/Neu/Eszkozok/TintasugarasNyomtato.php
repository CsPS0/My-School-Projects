<?php

namespace Neu\Eszkozok;

class TintasugarasNyomtato extends Nyomtato
{
    protected int $patronokSzama;

    public function __construct(string $gyarto, string $tipus, bool $szines, int $ar, int $patronokSzama)
    {
        parent::__construct($gyarto, $tipus, $szines, $ar);
        $this->patronokSzama = $patronokSzama;
    }

    public function __toString(): string
    {
        $szinStr = $this->szines ? 'szines' : 'fekete-feher';
        $arStr = number_format($this->ar, 0, '', ' ');
        return "{$this->gyarto} {$this->tipus} {$szinStr} tintasugaras nyomtato ({$this->patronokSzama} patron) {$arStr} Ft";
    }

    public function toArray(): array
    {
        $szinStr = $this->szines ? 'szines' : 'fekete-feher';
        return [
            $this->gyarto,
            $this->tipus,
            $szinStr,
            $this->patronokSzama,
            $this->ar
        ];
    }
}