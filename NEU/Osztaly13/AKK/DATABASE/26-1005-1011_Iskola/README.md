# Iskola

DATABASE feladat, 2026. 10.05. – 10.11.

Feladatleírás és források: `iskola.pdf`

## Indítás

```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-1005-1011_Iskola"
docker run --name db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/feladat" -d mysql:latest
docker exec -it db mysql -u root -p
```

A `mysql>` promptban (jelszó: `jelszo`):

```sql
CREATE DATABASE iskola;
USE iskola;
SOURCE /feladat/sql/tantargyak-table.sql;
SOURCE /feladat/sql/jegyek-table.sql;
SOURCE /feladat/sql/jegyeim.sql;
SOURCE /feladat/sql/felhasznalok.sql;
SOURCE /feladat/sql/show.sql;
```

A `sql/feladatok` mappa fájljait a megfelelő felhasználóként belépve futtasd (pl. `docker exec -it db mysql -u Ilona -p iskola`).

Takarítás: `docker rm -f db`
