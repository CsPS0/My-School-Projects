# Autoverseny Szimuláció

Ez a projekt egy autóverseny szimulációt valósít meg C# nyelven.

## Funkciók

- Versenyzők különböző vezetési stílusokkal (Agresszív, Lendületes, Veszélyes, Óvatos).
- Előzések, tankolások és balesetek szimulációja.
- Baleseti valószínűségek kezelése (sima kiesés, páros baleset, tömegkarambol).
- Körönkénti állás és kiesők listázása.
- Végeredmény megjelenítése.

## Futtatás

A projekt futtatásához .NET 10.0 SDK szükséges.

1. Navigálj a projekt mappájába:
   ```bash
   cd solticsongor-Autoverseny/solticsongor-Autoverseny
   ```

2. Futtasd a programot a teszt bemenettel:
   ```bash
   echo test.txt | dotnet run
   ```
   Vagy interaktív módban csak `dotnet run`, majd add meg a fájl nevét (pl. `test.txt`).

## Bemeneti Fájl Formátuma

A bemeneti fájl (pl. `test.txt`) első sora a körök számát és a versenyzők számát tartalmazza.
A következő sorok a versenyzők nevét és kategóriáját (1-4) tartalmazzák.

Kategóriák:
1. Agresszív
2. Lendületes
3. Veszélyes
4. Óvatos
