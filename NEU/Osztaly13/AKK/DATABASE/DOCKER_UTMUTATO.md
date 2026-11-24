# Docker MySQL – gyors útmutató (dolgozatra)

## 1. Indítás (3 parancs)

```bash
cd "NEU/Osztaly13/AKK/DATABASE/<feladat-mappa>"
docker run --name db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/feladat" -d mysql:latest
docker exec -it db mysql -u root -p
```

- Jelszó: `jelszo`.
- Ha a `docker exec` hibát ad, várj 10–20 másodpercet, a MySQL még indul.
- PowerShellben `$(pwd)` helyett `${PWD}` kell.
- Ha a feladat egy `sql` mappát kér `/sql` néven: `-v "$(pwd)/sql:/sql"`.
- MariaDB-hez: `mariadb:latest` és `MARIADB_ROOT_PASSWORD`.

## 2. Parancsok a `mysql>` promptban

| Mit | Parancs |
| :--- | :--- |
| Adatbázisok listája | `SHOW DATABASES;` |
| Új adatbázis | `CREATE DATABASE autokolcsonzo;` |
| Magyar ékezetekkel | `CREATE DATABASE biztositasok DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;` |
| Alapértelmezett adatbázis | `USE autokolcsonzo;` |
| Táblák listája (nézettel együtt) | `SHOW TABLES;` |
| Tábla szerkezete | `DESCRIBE autok;` |
| SQL fájl futtatása | `SOURCE /sql/autok-table.sql;` |
| Nézet létrehozása | `CREATE VIEW nezet AS SELECT ...;` |
| Felhasználók listája | `SELECT user, host FROM mysql.user;` |
| Jogok megnézése | `SHOW GRANTS FOR 'anna'@'%';` |
| Ki vagyok? | `SELECT CURRENT_USER();` |
| Törlés | `DROP TABLE x;` / `DROP DATABASE x;` |
| Kilépés | `exit` |

**Felhasználók és jogok** (a jelszó = a név):

```sql
CREATE USER 'anna'@'%' IDENTIFIED BY 'anna';
GRANT SELECT, INSERT ON autokolcsonzo.kolcsonzesek TO 'anna'@'%';
GRANT SELECT, INSERT, UPDATE ON autokolcsonzo.* TO 'tulajdonos'@'%';
GRANT SELECT ON autokolcsonzo.kolcsonzesek_nezet TO 'penzugyes'@'%';
GRANT ALL PRIVILEGES ON iskola.* TO 'Admin'@'%';
```

**Belépés más felhasználóként** (új terminálban, vagy `exit` után):

```bash
docker exec -it db mysql -u anna -p autokolcsonzo
```

**Aktuális időpont:** `NOW()`, például `VALUES (11, 'Kiss Peter', '2024-12-11 12:35:00', NOW());`.

## 3. A három legutóbbi feladat

### Car-Rental (autókölcsönző)

```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-1116-1122_Car-Rental/solti-csongor-autokolcsonzo"
docker run --name autokolcsonzo -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd)/sql:/sql" -d mysql:latest
docker exec -it autokolcsonzo mysql -u root -p
```

```sql
CREATE DATABASE autokolcsonzo;  SHOW DATABASES;  USE autokolcsonzo;
SOURCE /sql/autok-table.sql;  SOURCE /sql/kolcsonzesek-table.sql;  SOURCE /sql/kolcsonzesek_nezet.sql;
SHOW TABLES;
SOURCE /sql/felhasznalok.sql;  SELECT user, host FROM mysql.user;
INSERT INTO autok (rendszam, marka, modell, ar_naponta) VALUES ('AA-BB-111','Toyota','Corolla',9500), ('GLX-132','Ford','Focus',4500);
SELECT * FROM autok;
```

- **16. feladat (anna):** belépés `docker exec -it autokolcsonzo mysql -u anna -p autokolcsonzo`, majd `INSERT INTO kolcsonzesek (auto_id, ugyfel, kolcsonzes_kezdete, kolcsonzes_vege) VALUES (11, 'Kiss Peter', '2024-12-11 12:35:00', NOW());`. Anna nem látja az `autok` táblát, ezért az id-t (11) a root `SELECT`-jéből írd be.
- **17. feladat (root):** `UPDATE kolcsonzesek SET kolcsonzes_kezdete = '2024-12-31 16:55:00' WHERE ugyfel = 'Kiss Peter';`, majd `SELECT * FROM kolcsonzesek;`.

### Telekommunikáció és Biztosítások

Mindkettőnél az egész mappát csatold fel `/feladat` néven, és futtasd a fájlokat:

```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-1109-1115_Telekommunikacio"
docker run --name db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/feladat" -d mysql:latest
```

```sql
SOURCE /feladat/solticsongor-Telekommunikacio.sql;
SHOW TABLES;  DESCRIBE Mobiltelefon;
SOURCE /feladat/solticsongor-TelekommunikacioOneTable.sql;
```

- **Biztosítások:** mappa `26-1109-1115_Biztositasok`, fájlok `solticsongor-Biztositasok.sql` és `solticsongor-BiztositasokOneTable.sql`.
- Mindkét feladatban az első fájl a külön táblás megoldás, a `...OneTable.sql` az egytáblás.
- Minden fájl `DROP DATABASE IF EXISTS`-szel kezd, ezért bármikor újrafuttatható.

## 4. Gyakori hibák

| Hiba | Megoldás |
| :--- | :--- |
| `Conflict. The container name "/db" is already in use` | `docker rm -f db`, majd indítsd újra |
| `ERROR 2002 ... Can't connect` közvetlenül indítás után | Várj pár másodpercet, a MySQL még indul |
| `ERROR 1045 Access denied` | Rossz jelszó, vagy a felhasználó még nincs létrehozva |
| `ERROR 1142 ... command denied` | A felhasználónak nincs joga ehhez a táblához. Ez a jogok tesztelésénél jó jel. |
| `ERROR 1046 No database selected` | `USE adatbazis;` |
| `SOURCE` nem fut, `ERROR 1064 near 'SOURCE'` | Csak interaktív módban (`docker exec -it`) működik. Csővel (`\|`) futtatva add hozzá: `--commands=ON`. |
| `Failed to open file` a `SOURCE`-nál | Rossz útvonal: a konténeren belüli mount nevet használd (`/sql/...`, `/feladat/...`) |
| Eltűnnek vagy elcsúsznak az ékezetek beírásnál | Írj ékezet nélkül, vagy lépj be így: `docker exec -it -e LANG=C.UTF-8 db mysql -u root -p` |

## 5. Takarítás

```bash
docker rm -f db
```
