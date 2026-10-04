# Űrhajózás – Docker MySQL / MariaDB futtatási útmutató

Ez az útmutató bemutatja, hogyan kell futtatni az **Űrhajózás** adatbázis feladatot iskolai Docker környezetben (Linux / terminál), illetve Windows alatt Dockerrel vagy helyi MySQL/MariaDB szerverrel.

---

## 1. Iskolai Docker futtatás (Linux / WSL / Git Bash / PowerShell)

### 1.1. Lépj be a feladat mappájába
Nyiss terminált, és navigálj a feladat gyökérmappájába:
```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-0921-0927_Urhajozas"
```

### 1.2. Docker konténer indítása kötetcsatolással (Volume Mount)

A mappát `/urhajozas` néven csatoljuk be a konténerbe:

**Linux / macOS / Git Bash alatt:**
```bash
docker run --name urhajozas-db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/urhajozas" -d mysql:latest
```
*(Ha az iskolában MariaDB-t használtok: `docker run --name urhajozas-db -e MARIADB_ROOT_PASSWORD=jelszo -v "$(pwd):/urhajozas" -d mariadb:latest`)*

**Windows PowerShell alatt (Docker Desktop esetén):**
```powershell
docker run --name urhajozas-db -e MYSQL_ROOT_PASSWORD=jelszo -v "${PWD}:/urhajozas" -d mysql:latest
```

---

## 2. Belépés a MySQL CLI-be és feladatok futtatása

### 2.1. Közvetlen belépés a MySQL parancssorba:
```bash
docker exec -it urhajozas-db mysql -u root -p
```
*Kérni fogja a jelszót:* írd be: `jelszo` *(nem látszanak a karakterek, üss Entert)*.

*(MariaDB esetén a parancs: `docker exec -it urhajozas-db mariadb -u root -p`)*

### 2.2. A teljes megoldás futtatása egyben (`solti-csongor-urhajozas.sql`):
A MySQL parancssorban (`mysql>` promptnál):
```sql
SOURCE /urhajozas/solti-csongor-urhajozas.sql;
```
Ez automatikusan:
1. Létrehozza az `urhajozas` adatbázist UTF-8 kódolással és magyar rendezéssel.
2. Kiválasztja (`USE urhajozas;`).
3. Kikapcsolja a Foreign Key ellenőrzést, betölti a táblákat és adatokat (`/urhajozas/urhajozas-tablak.sql`, `/urhajozas/urhajozas-adatok.sql`).
4. Lefuttatja az összes lekérdezést és módosítást (5–21. feladat).

---

## 3. Feladatonkénti futtatás a `tasks` mappából

Ha a tanár kéri a feladatok egyenkénti bemutatását vagy futtatását:

A `mysql>` promptban:
```sql
-- 2. Adatbázis létrehozása:
SOURCE /urhajozas/tasks/2.sql;

-- 3. Adatbázis kiválasztása:
SOURCE /urhajozas/tasks/3.sql;

-- 4. Táblák és adatok betöltése:
SOURCE /urhajozas/tasks/4.sql;

-- Tetszőleges feladat futtatása (pl. 5. feladat):
SOURCE /urhajozas/tasks/5.sql;

-- 13. feladat:
SOURCE /urhajozas/tasks/13.sql;

-- 21. feladat:
SOURCE /urhajozas/tasks/21.sql;
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
docker stop urhajozas-db

# Újraindítás később:
docker start urhajozas-db

# Törlés (ha már nincs rá szükség):
docker rm -f urhajozas-db
```

---

## 5. Futtatás Windows alatt WSL nélkül (Alternatívák)

Ha otthon vagy és nincs WSL / Docker telepítve:
1. **XAMPP / WampServer / Standalone MariaDB:**
   - Indítsd el a MySQL / MariaDB szervert a XAMPP vezérlőpultján.
   - Nyiss PowerShellt és lépj be: `mysql -u root -p` (ha nincs jelszó, csak Enter).
   - Futtasd a helyi Windows elérési úttal (előre dőlő perjellel `/`):
     ```sql
     SOURCE F:/REPOS/SCHOOL/My-School-Projects/NEU/Osztaly13/AKK/DATABASE/26-0921-0927_Urhajozas/solti-csongor-urhajozas.sql;
     ```
2. **HeidiSQL / DBeaver / phpMyAdmin:**
   - Csatlakozz a helyi adatbázishoz.
   - Nyisd meg a `solti-csongor-urhajozas.sql` fájlt és nyomj F9-et / Execute Scriptet.
