# Felhasználói Dokumentáció - Rusztikus Étterem (BetterAdmin)

## 🌐 Ügyfeleknek (Weboldal)

### 1. Regisztráció és Bejelentkezés
Az asztalfoglaláshoz saját fiókra van szükség:
- Kattintson a jobb felső sarokban található **Regisztráció** gombra.
- A sikeres regisztráció után jelentkezzen be.

### 2. Étlap böngészése
- Az **Összes**, **Levesek**, **Főételek** és **Desszertek** gombokkal szűrheti a kínálatot.
- Az árak és az allergének minden ételnél fel vannak tüntetve.

### 3. Asztalfoglalás
- Bejelentkezés után a **Foglalás** szekcióban adja meg a telefonszámát, a kívánt dátumot és időpontot.
- Adja meg a vendégek számát.
- Kattintson a **Foglalás küldése** gombra.

---

## 🖥️ Adminisztrátoroknak (Asztali Alkalmazás)

### 1. Kapcsolódás a Szerverhez
Ha az alkalmazás nem ugyanazon a gépen fut, mint a szerver:
1. Kattintson a bejelentkező ablakban a jobb felső sarokban található **⚙️ Kapcsolati beállítások** feliratra.
2. Írja be a szerver IP címét (pl. `http://192.168.1.15:3000`).
3. Kattintson a **Teszt** gombra a kapcsolat ellenőrzéséhez.
4. Ha megjelenik a "✅ Kapcsolat OK!" üzenet, a beállítás elmentődött, és bejelentkezhet.

### 2. Felhasználók Kezelése
- A **Felhasználók** menüpontban láthatja az összes regisztrált tagot.
- A **Törlés** gombbal eltávolíthat felhasználókat.
- *Megjegyzés: A központi admin felhasználó védett, nem törölhető.*

### 3. Étlap és Asztalok
- Új ételeket adhat hozzá az étlaphoz, amelyek azonnal megjelennek a weboldalon.
- Kezelheti az asztalok elérhetőségét és elhelyezkedését.

### 4. Statisztikák
- A **Statisztikák** oldalon nyomon követheti a mai napra várható vendégek számát és a szabad asztalok arányát.

---

## 🛠️ Hibaelhárítás

- **"Nem sikerült kapcsolódni a szerverhez":** Ellenőrizze, hogy a Node.js szerver fut-e, és az IP cím helyesen van-e beállítva a `server_config.json` fájlban.
- **"Admin törlése nem lehetséges":** Ez egy biztonsági funkció, a rendszergazdai fiók nem távolítható el.