# Book Store API (Laravel)

Megoldás a `26-0920-0926_Konyvesbolt` feladatra.

## Útvonalak (API Endpoints)

- `GET /api/books` - Könyvek listája növekvő ár szerint rendezve (`pages_min` és `pages_max` opcionális szűrőkkel)
- `GET /api/books/most-expensive` - Legdrágább könyv adatai
- `GET /api/books/random` - Véletlenszerű könyv adatai
- `GET /api/books/popular` - Népszerű könyvek (értékelés >= 4.5)
- `GET /api/books/genre/{genre}` - Megadott műfajú könyvek (`order=asc|desc` rendezéssel, alapértelmezett: `desc`)
- `GET /api/books/{id}` - Könyv lekérése azonosító alapján (nem létező azonosító esetén `404 Not Found`)
