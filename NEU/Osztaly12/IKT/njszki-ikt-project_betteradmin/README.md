# BetterAdmin - Rusztikus Étterem Kezelő

## Projekt Áttekintés
A BetterAdmin egy modern éttermi foglaláskezelő rendszer, amely egy asztali adminisztrációs alkalmazásból, egy kliensoldali weboldalból és egy központi Node.js szerverből áll.

## Fő Komponensek
*   **Szerver (Backend):** Node.js & Express alapú API, amely JSON fájlokban tárolja az adatokat (foglalások, menü, asztalok, felhasználók).
*   **Webes Felület (Frontend):** HTML5, CSS3 és Vanilla JS alapú reszponzív weboldal a vendégek számára (asztalfoglalás, étlap megtekintése).
*   **Asztali Alkalmazás (Admin):** C# & Avalonia (cross-platform .NET) alapú szoftver az étterem személyzete számára.

---

## Indítási Útmutató

### 1. Szerver indítása
A szervernek futnia kell ahhoz, hogy a weboldal és az asztali alkalmazás működjön.
```bash
cd src/server
npm install
node index.js
```
*A szerver alapértelmezetten a **3000-es porton** indul.*

### 2. Weboldal elérése
A szerver indítása után a weboldal automatikusan elérhető a böngészőben:
*   Helyben: `http://localhost:3000`
*   Hálózaton keresztül: `http://[SZERVER-IP]:3000`

### 3. Asztali Alkalmazás indítása
Szükséges hozzá a .NET 9 SDK.
```bash
cd src/app/RusztikusAdmin
dotnet run
```

---

## Bemutató Iskolai Hálózaton / Hotspoton
A projektet úgy készítettük el, hogy prezentáció közben több eszközről (telefonról, másik laptopról) is elérhető legyen.

### Csatlakozás menete:
1.  **Hotspot:** Indíts el egy mobilhotspotot a telefonodon, és csatlakoztasd rá a laptopot (szerver) és a többi eszközt.
2.  **IP cím:** A szerver indításakor a terminál kiírja a hálózati IP címet (pl. `http://192.168.1.15:3000`).
3.  **Weboldal:** Bármelyik telefonról a hotspoton beírhatod ezt a címet a böngészőbe.
4.  **Asztali App beállítása:** 
    *   Indítsd el az alkalmazást.
    *   A bejelentkező ablakban kattints a **⚙️ Kapcsolati beállítások** gombra.
    *   Írd be a szerver IP címét (pl. `http://192.168.1.15:3000`) és kattints a **Teszt** gombra.
    *   Sikeres teszt után a beállítás mentésre kerül a `server_config.json` fájlba.
5.  **Tűzfal:** Ha nem sikerül a csatlakozás, ellenőrizd, hogy a Windows tűzfal engedélyezi-e a forgalmat a 3000-es porton.

---

## Funkciók

### Felhasználókezelés
Az adminisztrátorok mind az asztali appban, mind a webes admin felületen kezelhetik a felhasználókat:
*   Regisztrált felhasználók listázása.
*   Felhasználók törlése.
*   **Védelem:** Az `admin` felhasználó rendszerszinten védett, nem törölhető.

### Foglalás és Menü
*   Valós idejű asztalfoglalás a weboldalon.
*   Interaktív menükezelés az admin felületen.
*   Statisztikák a napi vendégforgalomról.

## Csapat
*   Polyák Dávid Attila
*   Solti Csongor Péter

## Dokumentáció
Részletesebb információkért, technológiai áttekintésért és fejlesztői útmutatóért tekintsd meg a [teljes dokumentációt](/DOCUMENTAION.md).