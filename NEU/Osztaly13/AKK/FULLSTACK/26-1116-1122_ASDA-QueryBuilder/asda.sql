-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Host: db:3306
-- Generation Time: Oct 16, 2026 at 06:00 AM
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
-- Table structure for table `items`
--
DROP TABLE IF EXISTS `items`;
CREATE TABLE `items` (
  `id` bigint UNSIGNED NOT NULL,
  `event` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `qty` int NOT NULL DEFAULT '1',
  `unit` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `price` decimal(8,2) NOT NULL,
  `discount_price` decimal(8,2) DEFAULT NULL,
  `image` varchar(200) COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`id`, `event`, `name`, `qty`, `unit`, `price`, `discount_price`, `image`, `created_at`, `updated_at`) VALUES
(1, 'Tickled Pink', 'Diet Coke Cola 30 x 330ml Cans', 330, 'ml', 14.24, 10.00, '5000112572933_T1.webp', NULL, NULL),
(2, 'Tickled Pink', 'Nescafe Nescafe Original Instant Coffee 300g', 300, 'g', 7.25, 6.00, '5011546415499_T1.webp', NULL, NULL),
(3, 'Tickled Pink', 'Taylors of Harrogate 160 Tea Bags 500g', 160, 'pk', 6.17, 4.50, '5010357112085_T1.webp', NULL, NULL),
(4, 'Tickled Pink', 'Regina Blitz Household Towel', 1, 'roll', 1.82, 2.00, '8004260452816_T1.webp', NULL, NULL),
(5, 'Tickled Pink', 'Napolina Rigatoni 500g', 500, 'g', 1.00, NULL, '5000232024725_T1.webp', NULL, NULL),
(6, 'Halloween', 'Halloween Family Pumpkin Carving Kit', 9, 'pcs', 3.00, NULL, 'T_5063717307522_T1.webp', NULL, NULL),
(7, 'Halloween', 'Halloween Blood Spray', 56, 'ml', 2.00, NULL, 'T_5063717289606_T1.webp', NULL, NULL),
(8, 'Halloween', 'Pacific Wax Co Pumpkin Picking Candle', 510, 'g', 3.50, NULL, 'T_8904338641060_T1.webp', NULL, NULL),
(9, 'Christmas', 'Exceptional by ASDA 4 Toffee Apple Spiced Crumble Tarts', 4, 'pk', 2.95, NULL, '5063089609859_T1.webp', NULL, NULL),
(10, 'Christmas', 'Exceptional by ASDA Exceptional by 6 Luxury Mince Pies', 6, 'pk', 2.84, NULL, '5063089609866_T1.webp', NULL, NULL),
(11, 'Christmas', 'Exceptional by ASDA 4 Brown Butter Flavour & Rum Mince Pies', 4, 'pk', 2.95, NULL, '5063089609828_T1.webp', NULL, NULL),
(12, 'Christmas', 'Exceptional by ASDA 9 Luxury Mini Mince Pies', 9, 'pk', 3.25, NULL, '5063717011351_T1.webp', NULL, NULL),
(13, 'Christmas', 'Celebrations Milk Chocolate Tub 500g', 500, 'g', 5.00, NULL, '5000159570701_T1.webp', NULL, NULL),
(14, 'Christmas', 'ASDA Before Nine Thins 180g', 180, 'g', 2.14, 2.00, '5057172332366_T1.webp', NULL, NULL),
(15, 'Christmas', 'Paterson\'s Classic Shortbread Fingers 300g', 300, 'g', 1.50, 1.25, '0072417196379_T1.webp', NULL, NULL),
(16, 'Halloween', 'ASDA Horror Emporium Scarily Spicy Devil Horns 150g', 150, 'g', 1.00, 0.90, '5063717271038_T1.webp', NULL, NULL),
(17, 'Halloween', 'Squishmallows Original Biscuit Kit 186g', 186, 'g', 2.50, NULL, '5060202714377_T1.webp', NULL, NULL),
(18, 'Halloween', 'Cake Décor Limited Edition Eyeball Cupcake Kit 241g', 241, 'g', 2.50, NULL, '5060234980511_T1.webp', NULL, NULL),
(19, 'Halloween', 'Haribo Starmix Minis Fruit & Cola Flavour Jelly & Foam Sweets 22 x 16g Fun Size Bags Multipack 352g', 352, 'g', 2.98, 2.00, '5012035951214_T1.webp', NULL, NULL),
(20, 'Halloween', 'Haribo Trick or Treat 40 Spooky Mi̇ni Bags', 640, 'g', 3.00, NULL, '5012035998059_T1.webp', NULL, NULL),
(21, 'Halloween', 'Haribo HARIBO Bone Shakers 160g', 160, 'g', 1.00, NULL, '5012035973124_T1.webp', NULL, NULL),
(22, 'Tickled Pink', 'Warburtons Limited Edition Mega Thick Toastie Soft & Sliced White Loaf 800g', 800, 'g', 1.40, NULL, '5010044011585_T1.webp', NULL, NULL),
(23, 'Tickled Pink', 'Belvoir Farm Sparkling Elderflower 750ml', 750, 'ml', 3.47, 2.50, '5022019010110_T1.webp', NULL, NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
