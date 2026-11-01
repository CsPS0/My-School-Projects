SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

CREATE DATABASE IF NOT EXISTS `pelda`
CHARACTER SET utf8
COLLATE utf8_hungarian_ci;

CREATE TABLE `polobolt` (
  `id` int NOT NULL,
  `tipus` varchar(10) NOT NULL,
  `szin` varchar(10) NOT NULL,
  `meret` varchar(5) NOT NULL
);


INSERT INTO `polobolt` (`id`, `tipus`, `szin`, `meret`) VALUES
(1, 'trikó', 'zöld', 'XXL'),
(2, 'póló', 'kék', 'XXL'),
(3, 'póló', 'piros', 'S'),
(4, 'trikó', 'kék', 'S'),
(5, 'trikó', 'kék', 'M'),
(6, 'póló', 'zöld', 'XL'),
(7, 'póló', 'kék', 'S'),
(8, 'trikó', 'piros', 'M'),
(9, 'póló', 'piros', 'XL');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
