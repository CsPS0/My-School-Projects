DELETE FROM `berlesek` WHERE `ugyfel_id` IN (SELECT `id` FROM `ugyfelek` WHERE `varos` = 'Vasvár');
DELETE FROM `ugyfelek` WHERE `varos` = 'Vasvár';
