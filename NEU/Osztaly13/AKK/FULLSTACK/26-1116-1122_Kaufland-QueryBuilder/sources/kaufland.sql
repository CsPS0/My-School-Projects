-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Host: db:3306
-- Generation Time: Oct 15, 2026 at 06:47 PM
-- Server version: 9.7.1
-- PHP Version: 8.3.33

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `laravel`
--

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
CREATE TABLE `products` (
  `id` bigint UNSIGNED NOT NULL,
  `name` varchar(200) COLLATE utf8mb4_unicode_ci NOT NULL,
  `category` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `price` double NOT NULL,
  `discount` double DEFAULT NULL,
  `free_shipping` tinyint(1) NOT NULL DEFAULT '0',
  `image` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`id`, `name`, `category`, `price`, `discount`, `free_shipping`, `image`) VALUES
(1, 'Vanilkový cukor 20 kg Intenzívna vanilková aróma Aróma na pečenie Cukrovinky Šľahačka GiDel', 'non-perishables', 79.99, NULL, 1, '1.webp'),
(2, 'Med MGO™ 550+ - Manuka Health - 500g', 'non-perishables', 150.99, NULL, 0, '2.webp'),
(3, 'LAO GAN MA Chrumkavé chilli v oleji 210g', 'non-perishables', 4.29, 0.16, 0, '3.webp'),
(4, 'Royal Tiger ryža jazmínová dlhozrnná 18kg', 'non-perishables', 92.38, NULL, 0, '4.webp'),
(5, 'Dr. Oetker Vitalis KnusperPlus Double Choc crunchy müsli 420g', 'non-perishables', 8.9, NULL, 0, '5.webp'),
(6, 'Mutti Polpa Fine - dužina, jemne nasekaná 400g', 'non-perishables', 3.96, NULL, 0, '6.webp'),
(7, 'Finn Crisp Original Tenký celozrnný ražný chlieb s kváskom 200g', 'non-perishables', 2.08, NULL, 0, '7.webp'),
(8, 'Cestoviny (proso) BEZLEPKOVÉRAMEN 280g - DIÉTNE POTRAVINY', 'non-perishables', 8.31, NULL, 1, '8.webp'),
(9, 'Zimmermann Regensburger párky 4 kusy ATG 250g plechovka 550g', 'non-perishables', 7.72, NULL, 0, '9.webp'),
(10, 'SodaStream Isotonic Syrup 440ml, 6 kusov', 'soda syrup', 26.99, 0.04, 0, '10.webp'),
(11, '48 Bolero Sticks 3g instantný nápoj v prášku bez cukru nízkokalorická zmes Mix Testovacie balenie ', 'soda syrup', 12.79, NULL, 0, '11.webp'),
(12, 'Pepsi sirup bez cukru s príchuťou čerešní, 440 ml', 'soda syrup', 10.63, NULL, 1, '12.webp'),
(13, 'HORKÁ ČOKOLÁDA 75%80 g - VIVANI', 'chocolate', 6.51, 0.36, 0, '13.webp'),
(14, 'Milky Way Crispy Rolls: Kreskavé vaflové roláky s nadýchanou mliečnou náplňou a polevou z mliečnej čokolády 112,5 g', 'chocolate', 6.58, NULL, 0, '14.webp'),
(15, 'Milka Luflee alpské mlieko s množstvom malých vzduchových bubliniek 90g', 'chocolate', 2.13, NULL, 0, '15.webp'),
(16, 'Grand Chocolat - Mandľový pomaranč 100g', 'chocolate', 2.97, NULL, 0, '16.webp'),
(17, 'Milka Vianočné tyčinky 15 samostatne balených mini tyčiniek 150g', 'chocolate', 12.28, NULL, 0, '17.webp'),
(18, 'Casali banánové marshmallow kúsky s čokoládovou polevou 300g', 'chocolate', 4.14, NULL, 0, '18.webp'),
(19, 'Milka Vanilkový croissant 50 g', 'chocolate', 2.38, 0.32, 0, '19.webp'),
(20, 'Cathedral City Extra Mature Cheddar - Aromatyczny ser cheddar, dojrzewający 200g', 'cheese', 9.37, 0.3, 0, '20.webp'),
(21, 'Ser wysokobiałkowy Piórko Ceko niskotłuszczowy 225 g', 'cheese', 6.45, 0.31, 0, '21.webp'),
(22, 'Galbani Mascarpone 250 g', 'cheese', 3.2, NULL, 0, '22.webp'),
(23, 'Pečený syr Camembert 2 x 75 g', 'cheese', 6.93, NULL, 0, '23.webp'),
(24, 'Syr Papillon Roquefort z ovčieho mlieka, 100 g', 'cheese', 6.29, 0.31, 0, '24.webp'),
(25, 'Syr Mlekovita Cheddar, plátky, 1 kg', 'cheese', 9.55, NULL, 0, '25.webp'),
(26, 'BiFi The Original Turkey Poultry Mini Salami s morčacím mäsom 20g', 'sausage', 0.78, NULL, 0, '26.webp'),
(27, 'Kabanosy Exclusive z serem Tarczyński 105 g', 'sausage', 3.91, 0.21, 0, '27.webp'),
(28, 'Viedenská klobása 180g', 'sausage', 8.73, NULL, 0, '28.webp'),
(29, 'Wiener - Minis bez dusitanovej soli 180g', 'sausage', 6.28, NULL, 0, '29.webp'),
(30, 'Cantaluppi Tavernerio - Mortadella Classica - Vákuovo balené - 2,4/3,1 kg', 'sausage', 59.9, NULL, 1, '30.webp'),
(31, 'Kiełbaski Bekonowo Serowe wędzone JBB 190 g', 'sausage', 4.06, 0.24, 0, '31.webp'),
(32, 'Krakowska Sucha Tarczyński 330 g', 'sausage', 8.09, 0.04, 0, '32.webp'),
(33, 'Kapsuly Tchibo Cafissimo, espresso, silná káva, 96 kusov', 'coffee', 47.3, NULL, 1, '33.webp'),
(34, 'Borbone Magica Palermo, hliníkové kapsuly pre Nespresso - 30 kapsúl', 'coffee', 10.45, NULL, 0, '34.webp'),
(35, 'Julius Meinl Lungo Classico, kávové kapsuly pre zariadenia Nespresso®, 30 kusov.', 'coffee', 13.2, NULL, 0, '35.webp'),
(36, 'Káva Starbucks NESS Creamy Vanilla', 'coffee', 12.32, NULL, 0, '36.webp');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
