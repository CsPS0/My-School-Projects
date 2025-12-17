<?php

namespace Acme\Iskola;

use DateTime;
use Stringable;

class Jegy implements Stringable
{
    private static array $tipusok = ["Témazáró", "Órai munka", "Teszt"];

    public static function lehetsegesTipusok(): array
    {
        return self::$tipusok;
    }

    private static array $osztalyzatok = ["elégtelen", "elégséges", "közepes", "jó", "jeles"];

    public static function lehetsegesOsztalyzatok(): array
    {
        return self::$osztalyzatok;
    }

    private static array $tantargyak = ["Matematika", "Angol", "Programozás", "Nyelvtan", "Történelem"];

    public static function lehetsegesTantargyak(): array
    {
        return self::$tantargyak;
    }

    private string $tipus;
    private int $jegy;
    private string $tantargy;
    private string $tanar;
    private DateTime $beirva;

    public function __get(string $name): mixed
    {
        if ($name === 'osztalyzat') {
            return self::$osztalyzatok[$this->jegy - 1] ?? null;
        }

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
        return sprintf(
            "%d - %s (%s) %s %s",
            $this->jegy,
            strtoupper($this->osztalyzat),
            $this->tantargy,
            $this->tanar,
            $this->beirva->format('Y.m.d H:i')
        );
    }

    public function toArray(bool $asszociativ = false): array
    {
        if ($asszociativ) {
            return [
                'tipus' => $this->tipus,
                'jegy' => $this->jegy,
                'osztalyzat' => $this->osztalyzat,
                'tantargy' => $this->tantargy,
                'tanar' => $this->tanar,
                'beirva' => $this->beirva->format('Y-m-d H:i:s'),
            ];
        }

        return [
            $this->tipus,
            $this->jegy,
            $this->osztalyzat,
            $this->tantargy,
            $this->tanar,
            $this->beirva->format('Y-m-d H:i:s'),
        ];
    }
}