# Kondibelépők – Docker MySQL / MariaDB futtatási útmutató

Ez az útmutató bemutatja, hogyan kell futtatni a **Kondibelépők** adatbázis feladatot iskolai Docker környezetben (Linux / terminál), illetve Windows alatt Dockerrel vagy helyi MySQL/MariaDB szerverrel.

---

## 1. Iskolai Docker futtatás (Linux / WSL / Git Bash / PowerShell)

### 1.1. Lépj be a feladat mappájába
Nyiss terminált, és navigálj a feladat gyökérmappájába:
```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-0928-1004_Kondibelepok"
```

### 1.2. Docker konténer indítása kötetcsatolással (Volume Mount)

A mappát `/kondibelepok` néven csatoljuk be a konténerbe:

**Linux / macOS / Git Bash alatt:**
```bash
docker run --name kondi-db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/kondibelepok" -d mysql:latest
```
*(Ha az iskolában MariaDB-t használtok: `docker run --name kondi-db -e MARIADB_ROOT_PASSWORD=jelszo -v "$(pwd):/kondibelepok" -d mariadb:latest`)*

**Windows PowerShell alatt (Docker Desktop esetén):**
```powershell
docker run --name kondi-db -e MYSQL_ROOT_PASSWORD=jelszo -v "${PWD}:/kondibelepok" -d mysql:latest
```

---

## 2. Belépés a MySQL CLI-be és feladatok futtatása

### 2.1. Közvetlen belépés a MySQL parancssorba:
```bash
docker exec -it kondi-db mysql -u root -p
```
*Kérni fogja a jelszót:* írd be: `jelszo` *(nem látszanak a karakterek, üss Entert)*.

*(MariaDB esetén a parancs: `docker exec -it kondi-db mariadb -u root -p`)*

### 2.2. A teljes megoldás futtatása egyben (`solti-csongor-kondibelepok.sql`):
A MySQL parancssorban (`mysql>` promptnál):
```sql
SOURCE /kondibelepok/solti-csongor-kondibelepok.sql;
```
Ez automatikusan:
1. Létrehozza a `kondibelepok` adatbázist UTF-8 kódolással és magyar rendezéssel.
2. Kiválasztja (`USE kondibelepok;`).
3. Kikapcsolja a Foreign Key ellenőrzést, betölti a táblákat és az adatokat (`/kondibelepok/kondi-tablak.sql`, `/kondibelepok/kondi-adatok.sql`).
4. Lefuttatja az összes feladatot (4–29. feladat).

---

## 3. Feladatonkénti futtatás a `tasks` mappából

Ha a tanár a feladatok egyenkénti bemutatását kéri:

A `mysql>` promptban:
```sql
-- 2. Adatbázis létrehozása:
SOURCE /kondibelepok/tasks/2.sql;

-- 3. Adatbázis kiválasztása, táblák és adatok betöltése:
SOURCE /kondibelepok/tasks/3.sql;

-- Tetszőleges feladat futtatása (pl. 4. feladat - bérlettípusok száma):
SOURCE /kondibelepok/tasks/4.sql;

-- 5. feladat (női létszám):
SOURCE /kondibelepok/tasks/5.sql;

-- 10. feladat (Vas megye TRX):
SOURCE /kondibelepok/tasks/10.sql;

-- 29. feladat:
SOURCE /kondibelepok/tasks/29.sql;
```

---

## 4. Kilépés és a konténer kezelése

### Kilépés a MySQL konzolból:
```sql
exit;
```

### Konténer leállítása és újraindítása:
```bash
# Leállítás:
docker stop kondi-db

# Újraindítás később:
docker start kondi-db

# Törlés (ha már nincs rá szükség):
docker rm -f kondi-db
```

---

## 5. Futtatás Windows alatt WSL nélkül (Alternatívák)

Ha otthon vagy és nincs WSL / Docker telepítve:
1. **XAMPP / WampServer / Standalone MariaDB:**
   - Indítsd el a MySQL / MariaDB szolgáltatást a XAMPP Control Panelben.
   - Nyiss PowerShellt és lépj be: `mysql -u root -p` (ha nincs jelszó, csak Enter).
   - Futtasd a helyi Windows elérési úttal (előre dőlő perjellel `/`):
     ```sql
     SOURCE F:/REPOS/SCHOOL/My-School-Projects/NEU/Osztaly13/AKK/DATABASE/26-0928-1004_Kondibelepok/solti-csongor-kondibelepok.sql;
     ```
2. **HeidiSQL / DBeaver / phpMyAdmin:**
   - Csatlakozz a helyi adatbázishoz (pl. localhost:3306).
   - Nyisd meg a `solti-csongor-kondibelepok.sql` fájlt és futtasd le teljes egészében.
