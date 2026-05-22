# NJSZKI - WebSzerverCombined

* Ez egy egyesített repó a **Vite (Frontend)** és a **JSON-Server (Backend)** projektekből. A célja, hogy megkönnyítse az iskolai feladatok megoldását: így nem kell két külön projektet klónozni és kezelni.
* Az alap kódok **@ignazcdominik**-tól származnak. Ez a projekt az ő munkájára épül, a feladatok egyszerűbb megoldása érdekében készült.
* (Kizárólag könnyebb vagy nem dolgozati szintű feladatokhoz ajánlott.)

## Főbb Jellemzők
- **Egyesített Indítás:** A `pnpm dev` parancs egyszerre indítja el a frontendet és a backendet.
- **Beépített Proxy:** A frontend kérések (`/api/...`) automatikusan a backendre (`localhost:3000`) irányítódnak, így nem kell CORS hibákkal küzdeni.
- **Modularizált Backend:** A szerver logikája (`relationships.js`) le van választva, így a kód átláthatóbb.
- **Többféle Futási Mód:** Támogatja a Docker, Node.js és PHP alapú környezeteket is.

## Telepítés
Először telepítsd a függőségeket:
```sh
pnpm install
# Vagy ha npm-et használsz:
npm install
```

## Indítási Módok

### 1. Fejlesztés (Ajánlott)
Ez a mód elindítja a Vite fejlesztői szervert (Hot Module Replacement-tel) és a JSON-Servert is egy terminálablakban.
```sh
pnpm dev
```
- Frontend: `http://localhost:8080`
- Backend: `http://localhost:3000` (Proxy-n keresztül: `http://localhost:8080/api`)

### 2. Docker (Teljes környezet)
Ha konténerizálva szeretnéd futtatni az egészet:
```sh
docker-compose up --build
```
A projekt a `http://localhost:3000` címen lesz elérhető (a frontend buildelődik és a node szerver szolgálja ki).

### 3. PHP Mód (Statikus kiszolgálás)
Ha a feladat PHP-s kiszolgálást kér a frontendhez (pl. `php -S`):
```sh
pnpm php
```
Ez a parancs:
1. Lefordítja a frontendet (`dist` mappába).
2. Elindítja a PHP szervert a `dist` mappán (`localhost:8000`).
3. A háttérben elindítja a Node.js backendet is (`localhost:3000`), hogy az API hívások működjenek.

### 4. Hagyományos Node.js Start
Ha csak a kész, lefordított verziót szeretnéd futtatni Node-dal:
```sh
pnpm start
```

## Projekt Struktúra
- **`src/`**: A Vue/React/JS frontend forráskódja.
- **`data/`**: A backend adatbázisa (`db.json`) és konfigurációi.
- **`server.js`**: A backend belépési pontja.
- **`relationships.js`**: A backend adatkapcsolati logikája.
