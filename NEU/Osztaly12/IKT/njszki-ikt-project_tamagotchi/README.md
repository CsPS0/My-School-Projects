# Tamagotchi NonchalanTeam 🐾

## Projekt Áttekintés
Az **IKT projektmunka II** keretében fejlesztett virtuális kisállat szimuláció. A cél egy interaktív, objektumorientált alkalmazás létrehozása, ahol a felhasználó felelőssége a kisállat fejlődése, jólléte és túlélése.

---

## Dokumentáció
- **[Fejlesztői Dokumentáció](docs/DEV.md)** – Technikai specifikáció és architektúra.
- **[Felhasználói Útmutató](docs/USER.md)** – Játékmenet és kezelési útmutató.
- **[UML Diagram](docs/UML.md)** – Osztályhierarchia és kapcsolatok.
- **[Feladatkezelés](docs/WorkManagement.md)** – Aktuális teendők és roadmap.

---

## Fő Funkciók
- **Karakterválasztás:** Két előredefiniált karakter (Csongor és Dávid).
- **Állapotkezelés:** Éhség, boldogság és egészség valós idejű követése.
- **Dinamikus Arckifejezések:** A karakterek képe változik az állapotuk (éhség, betegség, boldogság, halál) szerint.
- **Öltöztetés (Shop):** Csongor karakterhez egyedi ruhák (kalapok, pólók, nadrágok) vásárolhatók és cserélhetők.
- **Mini-játékok:** Pénzszerzési lehetőség (Snake és Amőba/TicTacToe).
- **Dicsőséglista (Scoreboard):** A legjobban teljesítő kisállatok rangsorolása.
- **Túlélési Kihívás:** 5 perces túlélési limit "Died with Pride" minősítéssel.
- **Interakció:** Etetés, játék (Play) és gyógyítás (Heal) lehetősége.
- **Betegség Mechanika:** Véletlenszerűen előforduló betegség, amely gyorsítja az állapotromlást.
- **Beállítások:** Testreszabható ablakmódok (Ablakos, Keret nélküli, Teljes képernyő) és felbontás skálázás (1280x720 - 2560x1440).
- **Időkezelés:** A játék figyelembe veszi a mentés óta eltelt időt a statisztikák kiszámításakor.

---

## Technológiai Stack
| Technológia | Leírás |
| :--- | :--- |
| **Framework** | .NET 10.0 + AvaloniaUI (Cross-platform) |
| **Library** | CommunityToolkit.Mvvm |
| **Scaling** | Viewbox-alapú reszponzív UI |
| **Logic Layer** | C# Class Library (tamagotchiLib) |
| **Design** | Figma tervezett UI |

---

## Arculat
A projekt az alábbi színpalettát használja:
- `#8ecae6` (Háttér)
- `#219ebc` (Elsődleges gombok)
- `#023047` (Szöveg és panelek)
- `#ffb703` (Highlight / Boldogság)
- `#fb8500` (Action / Éhség)

---

## NonchalanTeam
- **Polyák Dávid Attila**
- **Solti Csongor Péter**
