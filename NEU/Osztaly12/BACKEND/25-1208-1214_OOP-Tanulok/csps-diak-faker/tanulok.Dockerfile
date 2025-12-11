FROM php:8.2-cli

RUN apt-get update && apt-get install -y \
    git \
    unzip

COPY --from=composer:latest /usr/bin/composer /usr/bin/composer

WORKDIR /app

COPY composer.json composer.lock ./

RUN composer install --no-dev --optimize-autoloader

COPY . .

RUN mkdir -p out

ENTRYPOINT ["php", "tanulok.php"]

