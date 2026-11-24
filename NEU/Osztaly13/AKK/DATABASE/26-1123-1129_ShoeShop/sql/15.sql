UPDATE `shoes`
SET `price` = 79.00
WHERE `model` = 'Suede Classic' AND `color` = 'Black'
  AND `brand_id` = (SELECT `id` FROM `brands` WHERE `brand_name` = 'Puma');
