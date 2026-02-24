<?php

namespace Foo\Bar;

class Tablet
{
    private static $manufacturers = [
        1 => "Apple",
        2 => "Samsung",
        3 => "Lenovo"
    ];

    public function __construct(
        private int $id,
        private int $manufacturer_id,
        private string $fullname,
        private float $screen,
        private int $storage,
        private string $os,
        private int $price
    ) {}

    public function __get(string $name): mixed
    {
        if ($name === 'manufacturer_name') {
            return self::$manufacturers[$this->manufacturer_id] ?? 'Ismeretlen';
        }
        return $this->$name;
    }

    public static function getManufacturers(): array
    {
        return self::$manufacturers;
    }

    public function getImagePath(): string
    {
        return "img/{$this->id}.webp";
    }
}
