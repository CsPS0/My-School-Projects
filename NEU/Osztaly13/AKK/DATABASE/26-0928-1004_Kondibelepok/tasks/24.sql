-- 24. feladat
ALTER TABLE tagok
ADD CONSTRAINT FK_tagok_kedvezmeny_id
FOREIGN KEY (kedvezmeny_id)
REFERENCES kedvezmenyek(id)
ON DELETE RESTRICT
ON UPDATE CASCADE;
