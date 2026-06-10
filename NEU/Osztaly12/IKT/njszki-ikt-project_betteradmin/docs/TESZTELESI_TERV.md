# Tesztelési Terv - Rusztikus Étterem (BetterAdmin)

## 1. Tesztelési Területek

### 1.1. Hitelesítés és Felhasználókezelés
- [ ] Regisztráció érvényes és érvénytelen adatokkal.
- [ ] Bejelentkezés helyes és hibás jelszóval.
- [ ] Admin felhasználó törlésének megakadályozása (Web & App).
- [ ] Felhasználó törlése és a változás azonnali megjelenése a listában.

### 1.2. Foglalási Rendszer
- [ ] Foglalás leadása bejelentkezett felhasználóként.
- [ ] Foglalás elutasítása nem bejelentkezett felhasználó esetén.
- [ ] Foglalás törlése admin felületről.

### 1.3. Hálózati Kapcsolat (Prezentációs Teszt)
- [ ] Szerver indítása hotspoton.
- [ ] Weboldal elérése okostelefonról a hálózati IP címen.
- [ ] Asztali app **"Kapcsolati beállítások"** felületén az IP cím megadása és a **Teszt** gomb megnyomása.
- [ ] Sikeres teszt utáni automatikus konfiguráció mentés ellenőrzése.

### 1.4. CRUD Műveletek (Admin)
- [ ] Új étel hozzáadása a menühöz -> Megjelenés a weboldalon.
- [ ] Asztal státuszának módosítása -> Statisztika frissülése.
- [ ] Étlap elem törlése.

---

## 2. Tesztesetek (Példák)

| ID | Megnevezés | Lépések | Elvárt Eredmény |
|----|------------|---------|-----------------|
| T1 | Admin védelem | Próbáld törölni az 'admin' nevű felhasználót az asztali appban. | A törlés sikertelen, figyelmeztető üzenet jelenik meg. |
| T2 | IP Dinamizmus | Indítsd el a szervert és ellenőrizd a konzol kimenetet. | Megjelenik a gép aktuális hálózati IP címe. |
| T3 | Web Auth | Próbálj foglalni asztalt bejelentkezés nélkül. | A rendszer a regisztrációra/bejelentkezésre irányít. |

---

## 3. Elfogadási Kritériumok
- Minden API végpont helyesen válaszol.
- Az adminisztrátor nem tudja törölni önmagát.
- A rendszer stabilan működik Wi-Fi hotspoton keresztül is.