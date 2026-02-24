<?php

namespace Neu\Eszkozok;

class LezerNyomtato extends Nyomtato
{
    protected int $tonerekSzama;

    public function __construct(string $gyarto, string $tipus, bool $szines, int $ar, int $tonerekSzama)
    {
        parent::__construct($gyarto, $tipus, $szines, $ar);
        $this->tonerekSzama = $tonerekSzama;
    }

    public function __toString(): string
    {
        $szinStr = $this->szines ? 'szines' : 'fekete-feher';
        $arStr = number_format($this->ar, 0, '', ' ');
        return "{$this->gyarto} {$this->tipus} {$szinStr} lezernyomtato ({$this->tonerekSzama} toner) {$arStr} Ft";
    }

    public function toArray(): array
    {
        $szinStr = $this->szines ? 'szines' : 'fekete-feher';
        return [
            $this->gyarto,
            $this->tipus,
            $szinStr,
            $this->tonerekSzama,
            $this->ar
        ];
    }
}