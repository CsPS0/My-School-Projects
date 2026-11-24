CREATE TABLE IF NOT EXISTS `berlesek` (
  `auto_id` INT NOT NULL,
  `ugyfel_id` INT NOT NULL,
  `elvitte` DATETIME NOT NULL,
  `visszahozta` DATETIME DEFAULT NULL,
  PRIMARY KEY (`auto_id`, `ugyfel_id`),
  CONSTRAINT `fk_berlesek_ugyfel` FOREIGN KEY (`ugyfel_id`) REFERENCES `ugyfelek` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;
