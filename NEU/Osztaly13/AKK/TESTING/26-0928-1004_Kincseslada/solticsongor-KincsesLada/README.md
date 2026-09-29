# Refaktorálás szükséges

A 4 sikertelen teszt a kód hibáit jelzi. A `Chest.cs` és a `Treasure.cs` refaktorálása a kód készítőjének feladata:

- `Treasure.cs`: a `Volume` init dobjon `ArgumentException`-t, ha az érték 0 vagy kisebb.
- `Chest.cs`: a `Volume` init dobjon `ArgumentException`-t, ha az érték negatív.
- `Chest.cs`: a `TakeOut` csak az első azonos nevű kincset vegye ki, és utána azonnal térjen vissza.