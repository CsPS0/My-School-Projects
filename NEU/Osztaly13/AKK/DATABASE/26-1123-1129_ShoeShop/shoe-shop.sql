SET FOREIGN_KEY_CHECKS = 0;

DROP DATABASE IF EXISTS `shoe_shop`;

CREATE DATABASE `shoe_shop`
DEFAULT CHARACTER SET utf8mb4
COLLATE utf8mb4_hungarian_ci;

USE `shoe_shop`;

DROP TABLE IF EXISTS `sales`;
DROP TABLE IF EXISTS `clients`;
DROP TABLE IF EXISTS `shoes`;
DROP TABLE IF EXISTS `brands`;

CREATE TABLE `brands` (
    `id`          INT           NOT NULL AUTO_INCREMENT,
    `brand_name`  VARCHAR(100)  NOT NULL,
    `country`     VARCHAR(100),
    `website`     VARCHAR(255),
    `created_at`  TIMESTAMP     DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    UNIQUE (`brand_name`)
);

CREATE TABLE `shoes` (
    `id`          INT           NOT NULL AUTO_INCREMENT,
    `brand_id`    INT           NOT NULL,
    `model`       VARCHAR(120)  NOT NULL,
    `color`       VARCHAR(50)   NOT NULL,
    `size`        DECIMAL(4,1)  NOT NULL,
    `style`       VARCHAR(100)  NOT NULL,
    `price`       DECIMAL(10,2) NOT NULL,
    `stock_qty`   INT           NOT NULL DEFAULT 0,
    PRIMARY KEY (`id`),
    CONSTRAINT `fk_shoe_brand`
        FOREIGN KEY (`brand_id`) REFERENCES `brands`(`id`)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CHECK (`stock_qty` >= 0),
    CHECK (`price`     >= 0),
    CHECK (`size`      >  0)
);

CREATE INDEX `idx_shoe_brand` ON `shoes` (`brand_id`);
CREATE INDEX `idx_shoe_model` ON `shoes` (`model`);

CREATE TABLE `clients` (
    `id`          INT           NOT NULL AUTO_INCREMENT,
    `first_name`  VARCHAR(100)  NOT NULL,
    `last_name`   VARCHAR(100)  NOT NULL,
    `email`       VARCHAR(255)  NOT NULL,
    `phone`       VARCHAR(30),
    `address`     VARCHAR(255),
    `created_at`  TIMESTAMP     DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    UNIQUE (`email`)
);

CREATE TABLE `sales` (
    `id`              INT           NOT NULL AUTO_INCREMENT,
    `client_id`       INT           NOT NULL,
    `shoe_id`         INT           NOT NULL,
    `quantity`        INT           NOT NULL DEFAULT 1,
    `unit_price`      DECIMAL(10,2) NOT NULL,
    `total_price`     DECIMAL(10,2) NOT NULL,
    `payment_method`  ENUM('card','cash','gift_card') NOT NULL,
    `sale_date`       TIMESTAMP     DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    CONSTRAINT `fk_sale_client`
        FOREIGN KEY (`client_id`) REFERENCES `clients`(`id`)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT `fk_sale_shoe`
        FOREIGN KEY (`shoe_id`)   REFERENCES `shoes`(`id`)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CHECK (`quantity`    >= 1),
    CHECK (`total_price` >  0)
);

CREATE INDEX `idx_sale_client` ON `sales` (`client_id`);
CREATE INDEX `idx_sale_shoe`   ON `sales` (`shoe_id`);
CREATE INDEX `idx_sale_date`   ON `sales` (`sale_date`);

INSERT INTO `brands` (`brand_name`, `country`, `website`) VALUES
('Nike',        'USA',     'https://www.nike.com'),
('Adidas',      'Germany', 'https://www.adidas.com'),
('New Balance', 'USA',     'https://www.newbalance.com'),
('Dr. Martens', 'UK',      'https://www.drmartens.com'),
('Puma',        'Germany', 'https://www.puma.com'),
('Converse',    'USA',     'https://www.converse.com');

INSERT INTO `shoes` (`brand_id`, `model`, `color`, `size`, `style`, `price`, `stock_qty`) VALUES
(1,'Air Max 90','White',  8.0,  'Sneaker', 120.00, 20),
(1,'Air Max 90','White',  9.0,  'Sneaker', 120.00, 18),
(1,'Air Max 90','White', 10.5,  'Sneaker', 120.00, 12),
(1,'Air Max 90','Black',  8.5,  'Sneaker', 125.00, 15),
(1,'Air Max 90','Black',  9.5,  'Sneaker', 125.00, 14),
(1,'Air Max 90','Black', 11.0,  'Sneaker', 125.00,  9),
(1,'Air Max 90','Navy',   9.0,  'Sneaker', 125.00, 10),
(1,'Air Max 90','Navy',  10.0,  'Sneaker', 125.00,  8),
(1,'Air Max 90','Navy',  11.0,  'Sneaker', 125.00,  5),

(1,'Air Force 1','White',  8.5,  'Sneaker', 110.00, 22),
(1,'Air Force 1','White',  9.5,  'Sneaker', 110.00, 20),
(1,'Air Force 1','White', 10.0,  'Sneaker', 110.00, 16),
(1,'Air Force 1','Black',  8.0,  'Sneaker', 115.00, 18),
(1,'Air Force 1','Black',  9.0,  'Sneaker', 115.00, 17),
(1,'Air Force 1','Black', 10.5,  'Sneaker', 115.00, 13),

(2,'Ultraboost 22','Grey',  9.0,  'Runner', 160.00, 14),
(2,'Ultraboost 22','Grey', 10.0,  'Runner', 160.00, 12),
(2,'Ultraboost 22','Black', 8.5,  'Runner', 165.00, 16),
(2,'Ultraboost 22','Black', 9.5,  'Runner', 165.00, 15),
(2,'Ultraboost 22','Blue',  9.0,  'Runner', 170.00, 10),
(2,'Ultraboost 22','Blue', 10.5,  'Runner', 170.00,  7),

(2,'Stan Smith','White',  8.0,  'Sneaker', 100.00, 25),
(2,'Stan Smith','White',  9.0,  'Sneaker', 100.00, 22),
(2,'Stan Smith','White', 10.0,  'Sneaker', 100.00, 20),
(2,'Stan Smith','Green',  8.5,  'Sneaker', 105.00, 18),
(2,'Stan Smith','Green',  9.5,  'Sneaker', 105.00, 16),
(2,'Stan Smith','Green', 10.5,  'Sneaker', 105.00, 14),

(3,'550','White/Teal',  9.0,  'Sneaker', 130.00, 11),
(3,'550','White/Teal', 10.0,  'Sneaker', 130.00,  9),
(3,'550','White/Teal', 11.0,  'Sneaker', 130.00,  6),
(3,'550','Grey/Red',    8.5,  'Sneaker', 130.00, 13),
(3,'550','Grey/Red',    9.5,  'Sneaker', 130.00, 12),
(3,'550','Grey/Red',   10.5,  'Sneaker', 130.00,  8),

(4,'1460 Boot','Black',  8.0,  'Boot',  210.00, 10),
(4,'1460 Boot','Black',  9.0,  'Boot',  210.00,  9),
(4,'1460 Boot','Brown',  8.5,  'Boot',  220.00,  7),
(4,'1460 Boot','Brown', 10.0,  'Boot',  220.00,  6),
(4,'1460 Boot','Olive',  9.0,  'Boot',  230.00,  5),
(4,'1460 Boot','Olive', 10.5,  'Boot',  230.00,  4),

(5,'Suede Classic','White/Red',  8.0,  'Sneaker',  80.00, 24),
(5,'Suede Classic','White/Red',  9.0,  'Sneaker',  80.00, 22),
(5,'Suede Classic','White/Red', 10.0,  'Sneaker',  80.00, 19),
(5,'Suede Classic','Black',      8.5,  'Sneaker',  75.00, 20),
(5,'Suede Classic','Black',      9.5,  'Sneaker',  75.00, 18),
(5,'Suede Classic','Black',     11.0,  'Sneaker',  75.00, 15),

(6,'Chuck 70','Black',  8.5,  'Sneaker',  95.00, 26),
(6,'Chuck 70','Black',  9.5,  'Sneaker',  95.00, 24),
(6,'Chuck 70','Black', 10.5,  'Sneaker',  95.00, 21),
(6,'Chuck 70','White',  8.0,  'Sneaker',  95.00, 23),
(6,'Chuck 70','White',  9.0,  'Sneaker',  95.00, 20),
(6,'Chuck 70','White', 10.0,  'Sneaker',  95.00, 18);

INSERT INTO `clients` (`first_name`, `last_name`, `email`, `phone`, `address`) VALUES
('Alice','Johnson',  'alice.j@email.com',  '555-0101','12 Oak St, Springfield'),
('Bob',  'Smith',    'bob.s@email.com',    '555-0102','45 Elm Ave, Shelbyville'),
('Carol','Williams', 'carol.w@email.com',  '555-0103','7 Birch Rd, Riverton'),
('Dan',  'Brown',    'dan.b@email.com',    '555-0104','88 Pine Ln, Lakeview'),
('Eve',  'Smith',    'eve.d@email.com',    '555-0105','3 Maple Ct, Fairview'),
('Frank','Miller',   'frank.m@email.com',  '555-0106','56 Cedar Blvd, Hilltop'),
('Grace','Wilson',   'grace.w@email.com',  '555-0107','9 Poplar Way, Brookfield'),
('Henry','Moore',    'henry.m@email.com',  '555-0108','71 Spruce Dr, Greenfield'),
('Iris', 'Taylor',   'iris.t@email.com',   '555-0109','22 Willow St, Meadow'),
('Jack', 'Offenbach', 'jack.a@email.com',   '555-0110','63 Ash Rd, Crestwood');

INSERT INTO `sales`
  (`client_id`, `shoe_id`, `quantity`, `unit_price`, `total_price`,
   `payment_method`, `sale_date`)
VALUES
( 1,   1, 1, 120.00, 120.00, 'card',       '2025-01-03 09:15:00'),
( 2,   4, 2, 125.00, 250.00, 'cash',       '2025-01-03 11:42:00'),
( 3,  10, 1, 110.00, 110.00, 'gift_card',  '2025-01-04 14:00:00'),
( 4,  18, 1, 165.00, 165.00, 'card',       '2025-01-05 10:30:00'),
( 1,  22, 3, 100.00, 300.00, 'cash',       '2025-01-06 16:10:00'),
( 5,  28, 1, 130.00, 130.00, 'gift_card',  '2025-01-07 13:25:00'),
( 6,  34, 1, 210.00, 210.00, 'card',       '2025-01-08 09:50:00'),
( 7,  40, 2,  80.00, 160.00, 'cash',       '2025-01-09 15:30:00'),
( 8,  46, 1,  95.00,  95.00, 'card',       '2025-01-10 11:05:00'),
( 2,   2, 1, 120.00, 120.00, 'cash',       '2025-01-10 12:00:00'),
( 3,   9, 1, 125.00, 125.00, 'gift_card',  '2025-01-11 10:20:00'),
( 9,  13, 2, 115.00, 230.00, 'card',       '2025-01-12 14:45:00'),
(10,  19, 1, 165.00, 165.00, 'cash',       '2025-01-13 09:30:00'),
( 1,  35, 1, 210.00, 210.00, 'gift_card',  '2025-01-14 16:55:00'),
( 4,  42, 3,  80.00, 240.00, 'card',       '2025-01-15 10:10:00'),
( 5,  47, 1,  95.00,  95.00, 'cash',       '2025-01-16 13:00:00'),
( 6,   5, 1, 125.00, 125.00, 'card',       '2025-01-17 11:35:00'),
( 7,  11, 2, 110.00, 220.00, 'gift_card',  '2025-01-18 15:20:00'),
( 8,  27, 1, 105.00, 105.00, 'cash',       '2025-01-19 09:40:00'),
( 2,  14, 1, 115.00, 115.00, 'card',       '2025-01-20 14:15:00'),
( 9,  30, 1, 130.00, 130.00, 'cash',       '2025-01-21 10:55:00'),
(10,   3, 1, 120.00, 120.00, 'gift_card',  '2025-01-22 16:00:00'),
( 1,  17, 2, 160.00, 320.00, 'card',       '2025-01-23 12:30:00'),
( 3,  49, 1,  95.00,  95.00, 'cash',       '2025-01-24 09:05:00'),
( 5,  21, 1, 170.00, 170.00, 'card',       '2025-01-25 14:50:00'),
( 6,   7, 1, 125.00, 125.00, 'gift_card',  '2025-01-26 11:10:00'),

( 7,  12, 1, 110.00, 110.00, 'cash',       '2025-02-01 10:00:00'),
( 8,  23, 1, 100.00, 100.00, 'card',       '2025-02-02 13:45:00'),
( 2,  38, 2, 230.00, 460.00, 'gift_card',  '2025-02-03 09:20:00'),
( 9,   6, 1, 125.00, 125.00, 'cash',       '2025-02-04 15:30:00'),
(10,  20, 1, 170.00, 170.00, 'card',       '2025-02-05 11:00:00'),
( 1,  26, 1, 105.00, 105.00, 'cash',       '2025-02-06 14:15:00'),
( 4,  32, 1, 130.00, 130.00, 'gift_card',  '2025-02-07 10:40:00'),
( 3,   8, 1, 125.00, 125.00, 'card',       '2025-02-08 16:25:00'),
( 5,  36, 2, 220.00, 440.00, 'cash',       '2025-02-09 09:55:00'),
( 6,  15, 1, 115.00, 115.00, 'card',       '2025-02-10 13:00:00'),
( 8,  24, 1, 100.00, 100.00, 'gift_card',  '2025-02-11 10:10:00'),
( 2,   2, 1, 120.00, 120.00, 'cash',       '2025-02-12 15:45:00'),
(10,  50, 1,  95.00,  95.00, 'card',       '2025-02-13 11:30:00'),
( 1,  13, 1, 115.00, 115.00, 'gift_card',  '2025-02-14 14:20:00'),
( 4,  29, 1, 130.00, 130.00, 'cash',       '2025-02-15 09:35:00'),
( 7,   4, 2, 125.00, 250.00, 'card',       '2025-02-16 16:00:00'),
( 9,  20, 1, 170.00, 170.00, 'cash',       '2025-02-17 10:50:00'),
( 3,  31, 1, 130.00, 130.00, 'gift_card',  '2025-02-18 13:15:00'),
( 5,  16, 1, 160.00, 160.00, 'card',       '2025-02-19 11:40:00'),
( 6,  51, 2,  95.00, 190.00, 'cash',       '2025-02-20 15:05:00'),
( 8,   9, 1, 125.00, 125.00, 'card',       '2025-02-21 09:15:00'),
( 2,  22, 1, 100.00, 100.00, 'gift_card',  '2025-02-22 14:30:00'),
(10,  11, 1, 110.00, 110.00, 'cash',       '2025-02-23 10:00:00'),
( 1,  46, 3,  95.00, 285.00, 'card',       '2025-02-24 16:45:00'),
( 4,   6, 1, 125.00, 125.00, 'cash',       '2025-02-25 11:20:00'),
( 7,  18, 1, 165.00, 165.00, 'gift_card',  '2025-02-26 13:50:00'),
( 9,  38, 1, 230.00, 230.00, 'card',       '2025-02-27 09:30:00'),
( 3,  12, 2, 110.00, 220.00, 'cash',       '2025-02-28 15:10:00'),

( 5,   1, 1, 120.00, 120.00, 'card',       '2025-03-02 10:00:00'),
(10,  33, 1, 130.00, 130.00, 'cash',       '2025-03-03 14:20:00'),
( 6,  44, 2,  75.00, 150.00, 'gift_card',  '2025-03-05 09:45:00'),
( 1,  35, 1, 210.00, 210.00, 'card',       '2025-03-07 11:10:00'),
( 8,  48, 1,  95.00,  95.00, 'cash',       '2025-03-08 16:30:00'),
( 2,  15, 1, 115.00, 115.00, 'card',       '2025-03-09 10:05:00'),
( 4,  25, 1, 105.00, 105.00, 'gift_card',  '2025-03-10 13:55:00'),
( 7,  41, 2,  80.00, 160.00, 'cash',       '2025-03-11 09:20:00'),
( 9,  39, 1, 230.00, 230.00, 'card',       '2025-03-12 15:40:00'),
( 3,  10, 1, 110.00, 110.00, 'cash',       '2025-03-13 11:00:00');

SET FOREIGN_KEY_CHECKS = 1;
