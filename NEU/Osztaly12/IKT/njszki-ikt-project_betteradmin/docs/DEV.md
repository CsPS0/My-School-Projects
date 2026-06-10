# Fejlesztői Dokumentáció - Rusztikus Étterem Adminisztrációs Rendszer

## Projekt áttekintés
Ez a projekt egy teljes körű éttermi menedzsment rendszer, amely egy Node.js backendből, egy Vanilla JS weboldalból és egy Avalonia (C#) asztali alkalmazásból áll.

## Technológiai stack
- **Backend:** Node.js, Express.js (JSON adattárolás)
- **Frontend (Web):** HTML5, CSS3, Vanilla JavaScript (Fetch API)
- **Admin App:** C#, .NET 9, Avalonia UI, ReactiveUI
- **Hálózat:** Cross-device elérés hotspoton/LAN-on keresztül

## Projekt struktúra
```
/
├── src/
│   ├── server/          # Express.js backend & JSON adatok
│   ├── website/         # Kliensoldali weboldal (statikus fájlok a szerver kiszolgálásában)
│   └── app/             # Avalonia C# asztali alkalmazás
├── docs/                # Részletes dokumentációk
└── README.md            # Gyorsindítási útmutató
```

## Adatkezelés (REST API)

### Felhasználók (users.json)
```json
{
  "id": 1,
  "username": "admin",
  "password": "...",
  "email": "admin@etterem.hu",
  "fullName": "Admin",
  "role": "admin"
}
```

### Foglalások (bookings.json)
```json
{
  "id": "1705574400000",
  "name": "Teszt Elek",
  "email": "teszt@elek.hu",
  "phone": "+36301234567",
  "date": "2026-01-20",
  "time": "18:30",
  "guests": 4,
  "table": "2"
}
```

## Hálózati Konfiguráció és Prezentáció
A rendszer támogatja a többeneszközös használatot (pl. prezentáció mobil hotspoton):

1. **Szerver IP detektálás:** Az `index.js` automatikusan detektálja a gép hálózati IP címét.
2. **Weboldal:** A `js/data-service.js` a `window.location.origin` használatával automatikusan a megfelelő szerverhez kapcsolódik.
3. **Asztali App:** A bejelentkező ablakban módosítható a szerver IP címe, amely a `server_config.json` fájlba mentődik. A szolgáltatások a `ReloadConfig()` metóduson keresztül frissítik a bázis URL-t a fájl módosítása után.

## Fejlesztői környezet
1. **Szerver:** `npm install` -> `node index.js`
2. **Asztali App:** .NET 9 SDK szükséges.
3. **Debug:** A szerver logolja a beérkező kéréseket a terminálban.

## Biztonsági szabályok
- Az `admin` felhasználó (`id: 1`) törlése tiltott mind a kliensoldali, mind a szerveroldali kódban.
- Minden módosító művelethez (POST, DELETE) `Authorization: Bearer [token]` fejléc szükséges.