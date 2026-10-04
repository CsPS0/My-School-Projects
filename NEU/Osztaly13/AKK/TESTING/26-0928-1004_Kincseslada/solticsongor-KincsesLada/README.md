# Refaktorálási javaslatok

A tesztek futtatásakor 2 teszteset jelzi a forráskód hibáit a specifikációhoz képest. Cuczor Gábor tanár úrral azt tanultuk, hogy ha ilyen probléma merül fel a tesztelés során, akkor mindig kell, hogy legyen refaktorálási javaslat a kódhoz, ezért a `Chest.cs` osztályban az alábbi javítások szükségesek:

---

### 1. Negatív térfogat kezelése (`Volume`)
A leírás szerint a láda térfogata nem lehet negatív (0 megengedett). Jelenleg negatív értékkel is létrejön a láda.

**Javítás:**
```csharp
public int Volume
{
    get => _volume;
    init
    {
        if (value < 0)
            throw new ArgumentException("A láda térfogata nem lehet negatív.");
        _volume = value;
    }
}
```

---

### 2. Első egyező kincs kivétele (`TakeOut`)
A specifikáció szerint az azonos nevű kincsek közül csak a legelsőt szabad kivenni. A jelenlegi ciklus az összes azonos nevű elemet törli a ládából.

**Javítás:**
```csharp
public Treasure? TakeOut(string name)
{
    if (_contents.Count == 0 || !IsOpen) return null;

    for (int i = 0; i < _contents.Count; i++)
    {
        if (_contents[i].Name == name)
        {
            Treasure treasure = _contents[i];
            _contents.RemoveAt(i);
            return treasure;
        }
    }
    return null;
}
```