# Rotary

DESKTOP feladat, 2026. 11.23. – 11.29.

Feladatleírás és források: `Eredmenyek.csv`, `Rotary feladat.pdf`

## Indítás

Megoldás: `solticsongor-Rotary/solticsongor-Rotary.slnx`

| Projekt | Típus |
|---|---|
| `solticsongor-Rotary` | REST API, http://localhost:5250 |
| `Rotary_Minimal` | REST API, http://localhost:5251 (ugyanaz Minimal API-val) |
| `RotaryConsole` | konzol |
| `RotaryGUI` | WPF (grafikus) |
| `RotaryLib` | osztálykönyvtár |

```powershell
dotnet run --project "solticsongor-Rotary/solticsongor-Rotary/solticsongor-Rotary.csproj"
dotnet run --project "solticsongor-Rotary/RotaryConsole/RotaryConsole.csproj"
dotnet run --project "solticsongor-Rotary/RotaryGUI/RotaryGUI.csproj"
```

Az API-t indítsd el először (külön terminálban), a konzol és a grafikus alkalmazás azt hívja.
