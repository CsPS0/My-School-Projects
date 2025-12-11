## Setup

1.  **Clone the repository (or navigate to the project folder):**
    ```bash
    cd solti-aks20xi-diak-faker
    ```

2.  **Install PHP dependencies:**
    ```bash
    composer install
    ```

#### Generate 5 students to a `.txt` file:

```bash
php tanulok.php students.txt 5
```

The output file (`out/students.txt`) will have the following format for each student:

```
[Student Number]
[Full Name]
[Email Address]
[Birth Date in YYYY-MM-DD format]
```

#### Generate 3 students to a `.csv` file:

```bash
php tanulok.php students.csv 3
```

The output file (`out/students.csv`) will have the following format for each student, with fields separated by semicolons:

```csv
[Student Number];[Full Name];[Email Address];[Birth Date in YYYY-MM-DD format]
```

#### Generate a single student (default count) to a `.txt` file:

```bash
php tanulok.php single_student.txt
```

## Docker

The project can also be run using Docker, which encapsulates all dependencies.

### Build the Docker Image

Navigate to the project root and build the Docker image:

```bash
docker build -t monogram/tanulok -f tanulok.Dockerfile .
```

### Run the Docker Container

You can run the script inside a Docker container. It's recommended to mount the `out/` directory as a volume to access the generated files on your host machine.

#### Example: Generate 10 students to `docker_students.csv`

```bash
docker run --rm -v "$(pwd)/out:/app/out" monogram/tanulok docker_students.csv 10
```

This command will:
*   `--rm`: Automatically remove the container when it exits.
*   `-v "$(pwd)/out:/app/out"`: Mount your host's `out/` directory to the container's `/app/out` directory, making the generated files accessible on your host.
*   `monogram/tanulok`: The name of the Docker image to use.
*   `docker_students.csv 10`: The arguments passed to the `tanulok.php` script inside the container.

After running, you will find `docker_students.csv` in your local `out/` directory.
