# B Feladat: Relációs séma (relációkká alakított terv)

## 1. Formális relációs séma

- **csoportok** (**id** [PK], nev, nemzetiseg, atlageletkor, pontszam)
- **tancok** (**id** [PK], nev, mufaj)
- **zenek** (**cim** [PK], eloado, mufaj)
- **eloadasok** (**id** [PK], *csoport_id* [FK], *tanc_id* [FK], *zene_cim* [FK])

---

## 2. Kulcsok és kapcsolatok

### Elsődleges kulcsok (Primary Key - PK)
- `csoportok.id`: Egyedi csoportazonosító.
- `tancok.id`: Egyedi táncazonosító.
- `zenek.cim`: A feladat 4. feltétele alapján (*"a zenét egyértelműen azonosítja a címe, azaz nincs két azonos című szám"*), a cím természetes elsődleges kulcs.
- `eloadasok.id`: A kapcsolótábla egyedi azonosítója.

### Idegen kulcsok (Foreign Key - FK)
- `eloadasok.csoport_id` -> `csoportok.id`
- `eloadasok.tanc_id` -> `tancok.id`
- `eloadasok.zene_cim` -> `zenek.cim`

---

## 3. Megkötések érvényesülése

1. **Egyféle táncot több csoport is táncolhat, és egy csoport több táncot is előadhat:**
   - A `csoportok` és `tancok` közötti N:M kapcsolatot az `eloadasok` kapcsolótábla oldja fel.
2. **A zenét a címe azonosítja:**
   - A `zenek` tábla elsődleges kulcsa a `cim`.
3. **Nemzetiség és tánc műfaj korlátozott értékei:**
   - Adatbázis szinten `CHECK` megszorításokkal korlátozva az SQL scriptben.
