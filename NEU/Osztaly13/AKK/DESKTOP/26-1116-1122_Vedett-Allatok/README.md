# Vedett Allatok

DESKTOP feladat, 2026. 11.16. – 11.22.

Feladatleírás és források: `Védett állatok feladat.pdf`, `vedett.csv`

## Indítás

Megoldás: `solticsongor-VedettAllatok/solticsongor-VedettAllatok.slnx`

| Projekt | Típus |
|---|---|
| `solticsongor-VedettAllatok` | REST API, http://localhost:5243 |
| `VedettAllatok_Minimal` | REST API, http://localhost:5244 (ugyanaz Minimal API-val) |
| `VedettConsole` | konzol |
| `VedettGUI` | WPF (grafikus) |
| `VedettLib` | osztálykönyvtár |

```powershell
dotnet run --project "solticsongor-VedettAllatok/solticsongor-VedettAllatok/solticsongor-VedettAllatok.csproj"
dotnet run --project "solticsongor-VedettAllatok/VedettConsole/VedettConsole.csproj"
dotnet run --project "solticsongor-VedettAllatok/VedettGUI/VedettGUI.csproj"
```

Az API-t indítsd el először (külön terminálban), a konzol és a grafikus alkalmazás azt hívja.
