# HR-View – Docker MySQL / MariaDB futtatási útmutató

Ez az útmutató bemutatja, hogyan kell futtatni a **HR-View** adatbázis feladatot iskolai Docker környezetben (Linux / terminál), illetve Windows alatt Dockerrel vagy helyi MySQL/MariaDB szerverrel.

---

## 1. Iskolai Docker futtatás (Linux / WSL / Git Bash / PowerShell)

### 1.1. Lépj be a feladat mappájába
Nyiss terminált, és navigálj a feladat gyökérmappájába:
```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-0914-0920_HR-View"
```

### 1.2. Docker konténer indítása kötetcsatolással (Volume Mount)

A feladat gyökerét `/hr` vagy a tasks mappát csatoljuk be a konténerbe:

**Linux / macOS / Git Bash alatt:**
```bash
docker run --name hr-db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/hr" -d mysql:latest
```

**Windows PowerShell alatt (Docker Desktop esetén):**
```powershell
docker run --name hr-db -e MYSQL_ROOT_PASSWORD=jelszo -v "${PWD}:/hr" -d mysql:latest
```

---

## 2. Belépés a MySQL CLI-be és feladatok futtatása

### 2.1. Közvetlen belépés a MySQL parancssorba:
```bash
docker exec -it hr-db mysql -u root -p
```
*Jelszó:* `jelszo` *(üss Entert)*.

### 2.2. A feladat futtatása:
A MySQL konzolban (`mysql>` prompt):
```sql
-- HR adatbázis és táblák betöltése:
SOURCE /hr/tasks/hr.sql;

-- Teljes megoldás futtatása:
SOURCE /hr/solti-csongor-hr-view.sql;
```

---

## 3. Feladatonkénti futtatás a `tasks` mappából

```sql
-- 1. Alapadatok betöltése:
SOURCE /hr/tasks/hr.sql;

-- 3. Programozók nézet létrehozása:
SOURCE /hr/tasks/3.sql;

-- 4. Programozók listázása:
SOURCE /hr/tasks/4.sql;

-- 5. Munkakör létszám nézet:
SOURCE /hr/tasks/5.sql;

-- 6. Nagy létszámú munkakörök:
SOURCE /hr/tasks/6.sql;
```

---

## 4. Kilépés és a konténer leállítása
```sql
exit;
```
```bash
docker stop hr-db
docker rm -f hr-db
```
