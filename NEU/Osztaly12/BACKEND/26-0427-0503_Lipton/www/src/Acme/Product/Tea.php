<?php

declare(strict_types=1);

namespace Acme\Product;

class Tea {
    private int $id;
    private string $brand;
    private string $name;
    private string $range;
    private string $format;
    private int $qty;
    private string $unit;
    private int $price;
    private static array $ranges = ['green', 'black', 'fruit'];

    public function __construct(int $id, string $brand, string $name, string $range, string $format, int $qty, string $unit, int $price)
    {
        $this->id = $id;
        $this->brand = $brand;
        $this->name = $name;
        $this->range = $range;
        $this->format = $format;
        $this->qty = $qty;
        $this->unit = $unit;
        $this->price = $price;
    }

    public static function ranges(): array
    {
        return self::$ranges;
    }

    public function __get(string $name)
    {
        if ($name === 'image_path') {
            return "img/teas/{$this->id}.webp";
        }
        if (property_exists($this, $name)) {
            return $this->$name;
        }
        return null;
    }

    public function __set(string $name, $value): void
    {
        if (property_exists($this, $name)) {
            $this->$name = $value;
        }
    }

    public function getFormattedPrice(string $money): string
    {
        $formatted = number_format($this->price, 0, '.', ' ');
        if ($money === 'HUF') {
            $formatted .= ' Ft';
        }
        return $formatted;
    }
}
