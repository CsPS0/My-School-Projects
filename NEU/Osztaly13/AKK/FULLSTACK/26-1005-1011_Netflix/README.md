# Netflix

FULLSTACK feladat, 2026. 10.05. – 10.11.

Feladatleírás és források: `netflix-toplist.pdf`, `openapi.yaml`

## Indítás

Megoldás: `netflix-solti-csongor/` (az iskolai `fullstack2627` alapra épül)

A VM-ben (Docker kell hozzá):

```bash
cd netflix-solti-csongor
just init --seed
```

- Frontend: http://frontend.vm1.test
- Backend: http://backend.vm1.test/api/...
- Swagger: http://swagger.vm1.test
- phpMyAdmin: http://pma.vm1.test

Leállítás: `just down`
