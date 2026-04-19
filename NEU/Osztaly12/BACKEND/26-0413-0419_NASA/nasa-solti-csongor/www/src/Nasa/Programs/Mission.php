<?php

namespace Nasa\Programs;

class Mission {
    private int $id;
    private string $program;
    private string $name;
    private string $launchDate;
    private int $crewSize;
    private string $status;
    private int $budget;

    private static array $programs = [
        "Apollo",
        "Gemini",
        "Artemis"
    ];

    public function __construct(int $id, string $program, string $name, string $launchDate, int $crewSize, string $status, int $budget)
    {
        $this->id = $id;
        $this->program = $program;
        $this->name = $name;
        $this->launchDate = $launchDate;
        $this->crewSize = $crewSize;
        $this->status = $status;
        $this->budget = $budget;
    }

    public function __get(string $name): mixed
    {
        return $this->$name;
    }

    public function __set(string $name, mixed $value): void
    {
        $this->$name = $value;
    }

    public static function getPrograms(): array
    {
        return self::$programs;
    }

    public function getFormattedBudget(): string
    {
        return number_format($this->budget, 0, ",", " ") . " millió USD";
    }
}
