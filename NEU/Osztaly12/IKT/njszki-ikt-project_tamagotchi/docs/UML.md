# UML Dokumentáció

## Rendszerarchitektúra (High-Level)
Az alábbi diagram szemlélteti a projekt fő komponensei közötti kapcsolatot és az adatáramlást.

```mermaid
graph TD
    User(["Felhasználó"])
    UI["Avalonia UI — Views"]
    VM["ViewModels — Business Logic"]
    Lib["tamagotchiLib — Core Logic"]
    Storage[("Helyi Tároló — JSON")]
    Assets{"Asset Loader — Képek/Ikonok"}

    User <--> UI
    UI <--> VM
    VM <--> Lib
    Lib <--> Storage
    VM <--> Assets
```

---

## Osztályhierarchia (Class Diagram)
A projekt objektumorientált felépítése, kiemelve a `Pet` öröklődést és a ViewModellek kapcsolatát.

```mermaid
classDiagram
    class Pet {
        <<abstract>>
        +String Name
        +int Hunger
        +int Happiness
        +int Health
        +int Age
        +bool IsAlive
        +bool IsSick
        +TimeSpan SurvivalTime
        +bool DiedWithPride
        +int Money
        +Feed()
        +Heal()
        +Interact() String
        +UpdateState()
        +ApplyTimeElapsed(TimeSpan elapsed)
        #Die(bool withPride)
    }

    class CsongorPet {
        +bool CanWearClothes
        +String PetType
    }

    class DavidPet {
        +bool CanWearClothes
        +String PetType
    }

    class PetSaveManager {
        <<static>>
        +SavePet(Pet pet)
        +LoadPet(String name) Pet
        +LoadShopItems() List
    }

    class GameViewModel {
        -Pet _activePet
        +FeedCommand
        +HealCommand
        +PlayCommand
        +UpdateStatus()
    }

    Pet <|-- CsongorPet
    Pet <|-- DavidPet
    GameViewModel --> Pet : Kezeli
    GameViewModel --> PetSaveManager : Használja
```

## Komponensek Leírása
- **Avalonia UI:** A felhasználói felületért felelős réteg (XAML).
- **ViewModels:** Az MVVM mintát követve összeköti a nézetet a logikával, kezeli a parancsokat (Commands).
- **tamagotchiLib:** A játék magja. Itt történik a statisztikák számítása, az idő-kompenzáció és az állapotgép kezelése.
- **PetSaveManager:** JSON szerializációért felelős segédosztály, amely a mentéseket és a bolt adatait kezeli.
- **Assets:** Dinamikus képbetöltő modul, amely az aktuális állapot (pl. ruha, betegség) alapján választja ki a megfelelő fotót.
