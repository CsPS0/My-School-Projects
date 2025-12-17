# Jegyek PHP OOP Project

This project implements two PHP classes, `Acme\Iskola\Jegy` (School Grade) and `Acme\Mozi\Jegy` (Movie Ticket), following OOP principles. It includes a command-line script `jegyek.php` to generate instances of these classes using the Faker library, and output them to the console or to CSV/JSON files. The entire application is containerized using Docker.

## Prerequisites

To run this project, you only need [Docker](https://www.docker.com/get-started/) installed on your machine. You do **not** need PHP or Composer installed locally, as they are managed within the Docker container.

## Project Structure

- `src/Acme/Iskola/Jegy.php`: Defines the `Jegy` class for school grades.
- `src/Acme/Mozi/Jegy.php`: Defines the `Jegy` class for movie tickets.
- `jegyek.php`: The main command-line script to generate data.
- `composer.json`: Composer configuration for dependencies (Faker) and PSR-4 autoloading.
- `jegyek.Dockerfile`: Dockerfile to build the application image.
- `.dockerignore`: Specifies files and directories to ignore when building the Docker image.
- `out/`: Directory for generated output files (CSV, JSON).

## Setup and Running with Docker

This project is designed to run within a Docker container. All PHP dependencies (including the `vendor` directory) are installed automatically during the Docker image build process.

### 1. Build the Docker Image

Navigate to the `Jegyek` directory in your terminal and build the Docker image:

```bash
docker build -f jegyek.Dockerfile -t monogram/jegyek .
```

This command performs the following key steps:
*   Sets up a PHP environment based on `php:8.2-cli`.
*   Installs Composer.
*   Copies `composer.json` and runs `composer install` to download and set up the `vendor` directory and dependencies *inside the Docker image*.
*   Copies the rest of your application code.

### 2. Run the Application

You can run the `jegyek.php` script inside the Docker container to generate data.

#### a) Output to Console (Screen)

To generate 4 movie tickets and display them directly in your terminal:

```bash
docker run --rm monogram/jegyek mozi 4
```

#### b) Generate CSV File

To generate 3 school grades and save them to a CSV file in your local `out/` directory:

```bash
# Ensure the 'out' directory exists in your project root
mkdir -p out

# Run the command, mounting your local 'out' directory to the container's '/app/out'
docker run --rm -v "$(pwd)/out:/app/out" monogram/jegyek osztalyzat 3 csv
```
A file named `osztalyzat.csv` will be created in your local `Jegyek/out/` directory.

#### c) Generate JSON File

To generate movie tickets and save them to a JSON file in your local `out/` directory:

```bash
# Ensure the 'out' directory exists in your project root
mkdir -p out

# Run the command, mounting your local 'out' directory to the container's '/app/out'
docker run --rm -v "$(pwd)/out:/app/out" monogram/jegyek mozi 2 json
```
A file named `mozi.json` will be created in your local `Jegyek/out/` directory.

## Command-line Arguments

The `jegyek.php` script expects the following arguments:

1.  **Type (Required)**: `mozi` or `osztalyzat`.
    *   Example: `mozi`
2.  **Count (Required)**: An integer greater than 0, specifying the number of items to generate.
    *   Example: `4`
3.  **Output Format (Optional)**: `csv` or `json`. If omitted, output is printed to the console.
    *   Example: `csv`

### Examples:

- `docker run --rm monogram/jegyek osztalyzat 5` (Generates 5 school grades to console)
- `docker run --rm -v "$(pwd)/out:/app/out" monogram/jegyek mozi 1 json` (Generates 1 movie ticket to `mozi.json`)
