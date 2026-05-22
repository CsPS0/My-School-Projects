# Feladatok Megoldása

## 1. feladat: Miért rossz gyakorlat a manuális hívás?
- **Kódismétlés (DRY elv megsértése):** Minden tesztben ugyanazt a kódot kellene leírni, ami nehezíti a karbantartást.
- **Hibaforrás:** Ha egy teszt elbukik az Assert-nél, a `CloseConnection()` hívása elmaradhat (ha manuálisan a végére tesszük), így a fájlzár nyitva maradhat, ami befolyásolja a többi tesztet és instabil tesztkörnyezetet eredményez.

## 3. feladat: Származtatott tesztek - Miért előnyös itt a származtatás?
1. **Kód újrafelhasználás:** Nem kell újraírni a `[SetUp]` és `[TearDown]` metódusokat, mivel az ősosztályból öröklődnek.
2. **Egységesség:** Garantálja, hogy a speciális (pl. read-only) tesztek is pontosan ugyanolyan tiszta környezetben fussanak, mint az alap tesztek.
3. **Karbantarthatóság:** Ha változik az adatbázis elérési útja vagy a csatlakozás módja, csak az ősosztályban kell módosítani a logikát.
