# Iskolai Docker MySQL / MariaDB Útmutató (Adatbázis feladatok)

Ez az összefoglaló leírja a Docker használatát az iskolai adatbázis feladatokhoz, a belépést, a feladatok futtatását és a Windowsos alternatívákat (ha nincs WSL vagy Docker telepítve).

---

## 1. Gyors összefoglaló táblázat

| Feladat | Helyi mappa | Konténer mount | Teljes megoldás fájl | Tasks mappa |
| :--- | :--- | :--- | :--- | :--- |
| **Űrhajózás** | `.../26-0921-0927_Urhajozas` | `/urhajozas` | `solti-csongor-urhajozas.sql` | `/urhajozas/tasks/*.sql` |
| **Kondibelépők** | `.../26-0928-1004_Kondibelepok` | `/kondibelepok` | `solti-csongor-kondibelepok.sql` | `/kondibelepok/tasks/*.sql` |
| **HR-View** | `.../26-0914-0920_HR-View` | `/hr` | `solti-csongor-hr-view.sql` | `/hr/tasks/*.sql` |

---

## 2. A Docker munkafolyamat lépései (Iskolában / Linux / WSL / Git Bash)

### 1. lépés: Lépj be a projekt mappájába
```bash
# Például az űrhajózáshoz:
cd "NEU/Osztaly13/AKK/DATABASE/26-0921-0927_Urhajozas"

# Vagy a kondibelépőkhöz:
cd "NEU/Osztaly13/AKK/DATABASE/26-0928-1004_Kondibelepok"
```

### 2. lépés: Indítsd el a MySQL konténert felcsatolt kötettel
A `-v "$(pwd):/<mount_nev>"` parancs bemásolás nélkül közvetlenül a géped mappáját teszi elérhetővé a konténeren belül.

**Űrhajózás:**
```bash
docker run --name db-urhajozas -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/urhajozas" -d mysql:latest
```

**Kondibelépők:**
```bash
docker run --name db-kondi -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/kondibelepok" -d mysql:latest
```

*(Ha MariaDB-t kér a tanár: cseréld a `mysql:latest`-et `mariadb:latest`-re, és `MYSQL_ROOT_PASSWORD` helyett `MARIADB_ROOT_PASSWORD` használható).*

### 3. lépés: Belépés a MySQL CLI-be
```bash
docker exec -it db-urhajozas mysql -u root -p
# vagy Kondibelépőknél:
docker exec -it db-kondi mysql -u root -p
```
A jelszó bekérésekor írd be: `jelszo` és nyomj Entert.

### 4. lépés: SQL szkript futtatása a `SOURCE` paranccsal

A `mysql>` promptban:

#### A) Teljes fájl futtatása egyben:
```sql
-- Űrhajózás:
SOURCE /urhajozas/solti-csongor-urhajozas.sql;

-- Kondibelépők:
SOURCE /kondibelepok/solti-csongor-kondibelepok.sql;
```

#### B) Egyedi feladatok futtatása a `tasks` mappából:
```sql
-- Űrhajózás:
SOURCE /urhajozas/tasks/2.sql;
SOURCE /urhajozas/tasks/3.sql;
SOURCE /urhajozas/tasks/4.sql;
SOURCE /urhajozas/tasks/5.sql;
-- ...
SOURCE /urhajozas/tasks/21.sql;

-- Kondibelépők:
SOURCE /kondibelepok/tasks/2.sql;
SOURCE /kondibelepok/tasks/3.sql;
SOURCE /kondibelepok/tasks/4.sql;
-- ...
SOURCE /kondibelepok/tasks/29.sql;
```

### 5. lépés: Kilépés
```sql
exit;
```

### 6. lépés: Konténer leállítása vagy takarítása
```bash
# Leállítás:
docker stop db-urhajozas

# Törlés (felszabadítja a nevet és erőforrást):
docker rm -f db-urhajozas
```

---

## 3. Windows PowerShell szintaxis (Docker Desktop használatakor)

PowerShellben a `$(pwd)` helyett `${PWD}` vagy a pontos abszolút útvonal használandó:
```powershell
docker run --name db-kondi -e MYSQL_ROOT_PASSWORD=jelszo -v "${PWD}:/kondibelepok" -d mysql:latest
docker exec -it db-kondi mysql -u root -p
```

---

## 4. Mi a teendő Windows alatt, ha NINCS WSL és NINCS Docker?

Ha otthon gyakorolsz és a gépeden nincs WSL / Docker:

1. **XAMPP / WampServer használata:**
   - Indítsd el a MySQL modult a XAMPP Control Panelben.
   - Nyiss PowerShellt vagy parancssort:
     ```cmd
     mysql -u root -p
     ```
     *(XAMPP esetén alapértelmezetten nincs jelszó, csak üss Entert)*.
   - Futtasd a fájlt közvetlen Windows elérési úttal (használj sima `/` perjeleket):
     ```sql
     SOURCE F:/REPOS/SCHOOL/My-School-Projects/NEU/Osztaly13/AKK/DATABASE/26-0928-1004_Kondibelepok/solti-csongor-kondibelepok.sql;
     ```

2. **Grafikus kliens használata (HeidiSQL, DBeaver, phpMyAdmin):**
   - Csatlakozz a helyi szerverhez (`localhost:3306`, user: `root`).
   - Nyisd meg a `.sql` fájlt az editorban.
   - Nyomj F9-et (vagy Execute All) a futtatáshoz.
