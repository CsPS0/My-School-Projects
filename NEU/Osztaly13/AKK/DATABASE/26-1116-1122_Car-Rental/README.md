# Car Rental

DATABASE feladat, 2026. 11.16. – 11.22.

Feladatleírás és források: `car-rental.pdf`

## Indítás

```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-1116-1122_Car-Rental"
docker run --name db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd)/solti-csongor-autokolcsonzo/sql:/sql" -d mysql:latest
docker exec -it db mysql -u root -p
```

A `mysql>` promptban (jelszó: `jelszo`):

```sql
CREATE DATABASE autokolcsonzo;
USE autokolcsonzo;
SOURCE /sql/autok-table.sql;
SOURCE /sql/kolcsonzesek-table.sql;
SOURCE /sql/kolcsonzesek_nezet.sql;
SOURCE /sql/felhasznalok.sql;
```

A 15–17. feladat lépései (más felhasználóként belépve is) a `DOCKER_UTMUTATO.md` Car-Rental részében vannak.

Takarítás: `docker rm -f db`
