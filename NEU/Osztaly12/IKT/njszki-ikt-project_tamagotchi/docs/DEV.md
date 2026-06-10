# Fejlesztői Dokumentáció

## Rendszerkövetelmények
- **SDK:** .NET 10.0
- **IDE:** Visual Studio 2022 vagy JetBrains Rider
- **UI:** Avalonia 11.3.11

## Projekt Struktúra és Könyvtárak

### 1. Tamagotchi-NonchalanTeam (Fő Alkalmazás)
Az AvaloniaUI alapú asztali alkalmazás, amely az MVVM mintát követi.
- **`ViewModels/`**: A projekt agya. Itt található a nézetekhez tartozó logika, a parancsok (RelayCommands) és az adatkötéshez (Data Binding) szükséges tulajdonságok.
- **`Views/`**: Az XAML fájlok helye, amelyek a felhasználói felület kinézetét és elrendezését definálják.
- **`Converters/`**: Speciális osztályok, amelyek a ViewModel adatait alakítják át a View számára érthető formátumba:
    - `ImagePathToDownscaledBitmapConverter`: A memóriahatékonyság érdekében a nagy felbontású fotókat csak a szükséges méretben tölti be.
    - `FoodStockToOpacityConverter`: Vizuális visszajelzést ad (átlátszóság), ha elfogyott az élelem.
    - `XoColorConverter`: Az Amőba játékban a bábuk színét kezeli.
    - `PointToPositionConverter`: A Kígyó játékban a koordinátákat alakítja át képernyőpozícióvá.
- **`Data/`**: Statikus adatfájlok helye (pl. `shop_items.json`), amelyek a bolt kínálatát határozzák meg.
- **`Images/`**: A karakterek és ruhák fotóit tartalmazó könyvtár, karakter- és állapot-specifikusan lebontva.
- **`Assets/`**: Beágyazott erőforrások (pl. alkalmazás ikon).

### 2. tamagotchiLib (Mag Logika)
Egy tiszta C# osztálykönyvtár, amely független a felhasználói felülettől.
- **`Pet.cs`**: Az absztrakt alaposztály, amely tartalmazza az életerő, éhség és boldogság számításának matematikai logikáját.
- **`PetSaveManager.cs`**: A perzisztenciáért felelős. Kezeli a JSON szerializációt és a fájlrendszerbe történő mentést/betöltést.
- **`PetSaveData.cs`**: Egy egyszerű adatátviteli objektum (DTO), amely csak a mentéshez szükséges mezőket tartalmazza.

### 3. tamagotchiLib.Tests (Egységtesztek)
MSTest alapú tesztprojekt, amely a játékszabályok helyességét ellenőrzi (pl. éhen hal-e a pet, megfelelően gyógyul-e a betegség).

## Implementált Megoldások
- **MVVM:** Source Generators használata a `CommunityToolkit.Mvvm` csomaggal (ObservableProperty, RelayCommand).
- **Shop System:** Dinamikus ruházat kezelés Csongor karakterhez. JSON alapú tárgykatalógus és érmekezelés.
- **Mini-Games:** Snake (Kígyó) és TicTacToe (Amőba) modulok a pénzszerzéshez.
- **Scoreboard:** Lokális dicsőséglista a túlélési idő és gyűjtött érmék alapján.
- **Tray System:** Az alkalmazás bezárás helyett tálcára minimalizálódik (Cross-platform támogatással).
- **Safe Asset Loading:** Dinamikus képbetöltés fallback logikával (PNG/JPG támogatás).
- **Responsive Scaling:** `Viewbox` és `ResolutionScaling` a konzisztens megjelenítéshez.
- **Sickness Mechanic:** Véletlenszerű állapotromlás és vizuális jelzés.
- **Save/Load System:** JSON-alapú Pet mentés és betöltés, amely figyelembe veszi a kikapcsolt állapotban eltelt időt is.

## Következő Lépések
- [x] Unit tesztek implementálása a `tamagotchiLib`-hez.
- [x] Mentés/Betöltés rendszer (JSON alapú).
- [x] Véletlenszerű események (betegség).
- [x] Mini-games implementálása a pénzszerzéshez.
- [x] Karakter-specifikus interfészek és öröklődés finomítása.
- [ ] Felhő-alapú mentés (opcionális).
