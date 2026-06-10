# Adatkezelés és Szinkronizáció (REST API)

A projekt egy **Kliens-Szerver** architektúrát használ, ahol minden adatot egy központi Node.js szerver kezel. Ez biztosítja, hogy a weboldal és az asztali alkalmazás mindig ugyanazt az állapotot lássa.

## 1. Az Architektúra
A rendszer a fájlrendszert használja adatbázisként (JSON fájlok), de a hozzáférés csak a szerveren keresztül történik.

1. **Szerver (Node.js):** Az egyetlen komponens, amely közvetlenül írja/olvassa a `src/server/data/*.json` fájlokat.
2. **Weboldal:** A `Fetch API` segítségével kommunikál a szerverrel.
3. **Asztali App:** A `HttpClient` segítségével éri el a szerver végpontjait.

---

## 2. Adatfájlok (JSON)
A szerver a következő fájlokban tárolja az adatokat a `src/server/data/` mappában:
- `users.json`: Regisztrált felhasználók és adminok.
- `bookings.json`: Asztalfoglalások.
- `menu.json`: Az étterem aktuális kínálata.
- `tables.json`: Elérhető asztalok és kapacitásuk.
- `settings.json`: Globális éttermi beállítások (név, nyitvatartás).

---

## 3. API Végpontok (Példák)

### Weboldal / Asztali App -> Szerver
- `GET /menu`: Étlap lekérése (nyilvános).
- `POST /login`: Hitelesítés.
- `GET /users`: Felhasználók listázása (admin szükséges).
- `DELETE /users/:id`: Felhasználó törlése (admin szükséges).
- `POST /bookings`: Új foglalás rögzítése.

---

## 4. Hálózati Szinkronizáció (Hotspot/LAN)
Ahhoz, hogy több eszköz is elérje az adatokat (pl. prezentáció közben):
1. A szervernek a `0.0.0.0` címen kell indulnia (már beépítve).
2. A klienseknek (Web/App) ismerniük kell a szerver gép IP címét.
3. A weboldal automatikusan detektálja ezt a `window.location.origin` segítségével.
4. Az asztali app a `server_config.json` fájlból tölti be az IP címet.

## Előnyök
- **Konzisztencia:** Nincs adatütközés, mert a szerver sorban kezeli a kéréseket.
- **Biztonság:** A fájlokhoz való közvetlen hozzáférés korlátozott; a szerver validálja a kéréseket.
- **Rugalmasság:** Könnyen váltható valódi adatbázisra (pl. MongoDB/SQL) a kliensek módosítása nélkül.