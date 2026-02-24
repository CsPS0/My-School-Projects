<?php

namespace Acme;

use Stringable;

class Airliner implements Stringable
{
    private string $manufacturer;
    private string $model;
    private string $flightNumber;
    private int $passengers;
    private float $range;

    private static array $manufacturers = ["Boeing", "Airbus"];
    private static array $models = ["A320", "A350", "757", "777"];

    public function __construct(string $manufacturer, string $model, string $flightNumber, int $passengers, float $range)
    {
        $this->manufacturer = $manufacturer;
        $this->model = $model;
        $this->flightNumber = $flightNumber;
        $this->passengers = $passengers;
        $this->range = $range;
    }

    public static function allModels(): array
    {
        return self::$models;
    }

    public static function allManufacturers(): array
    {
        return self::$manufacturers;
    }

    public function whichAisle(): string
    {
        return $this->passengers > 250 ? "SZÉLESTÖRZS" : "KESKENYTÖRZS";
    }

    public function __get(string $name): mixed
    {
        if ($name === 'aisles') {
            return $this->whichAisle();
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
        // Format: [Airbus A350] 325 SZÉLESTÖRZS 9372.43km
        // Using strtoupper for aisle just in case, though logic returns upper.
        return sprintf(
            "[%s %s] %d %s %.2fkm",
            $this->manufacturer,
            $this->model,
            $this->passengers,
            strtoupper($this->whichAisle()),
            $this->range
        );
    }
}
