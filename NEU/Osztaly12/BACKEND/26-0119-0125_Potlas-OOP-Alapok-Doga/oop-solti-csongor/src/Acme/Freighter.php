<?php

namespace Acme;

use Stringable;

class Freighter implements Stringable
{
    private string $manufacturer;
    private string $model;
    private string $flightNumber;
    private float $maxTonnage;
    private float $distanceLimit;

    private static array $manufacturers = ["Boeing", "Airbus"];
    private static array $models = ["A330-200F", "A350F", "747-8F", "777F"];

    public function __construct(string $manufacturer, string $model, string $flightNumber, float $maxTonnage, float $distanceLimit)
    {
        $this->manufacturer = $manufacturer;
        $this->model = $model;
        $this->flightNumber = $flightNumber;
        $this->maxTonnage = $maxTonnage;
        $this->distanceLimit = $distanceLimit;
    }

    public static function allModels(): array
    {
        return self::$models;
    }

    public static function allManufacturers(): array
    {
        return self::$manufacturers;
    }

    public function calcEfficiency(): float
    {
        if ($this->distanceLimit == 0) return 0.0;
        return round(($this->maxTonnage * 1000) / $this->distanceLimit, 2);
    }

    public function __get(string $name): mixed
    {
        if ($name === 'eff') {
            return $this->calcEfficiency();
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
        // Format: [Airbus A350F] (12.76) 111.34t 8700.76km
        return sprintf(
            "[%s %s] (%.2f) %.2ft %.2fkm",
            $this->manufacturer,
            $this->model,
            $this->calcEfficiency(),
            $this->maxTonnage,
            $this->distanceLimit
        );
    }
}
