# ASDA QueryBuilder

FULLSTACK feladat, 2026. 11.16. – 11.22.

Feladatleírás és források: `asda-backend-feladat.pdf`, `openapi.yaml`

## Indítás

Megoldás: `solti-csongor-asda-backend/` (az iskolai `fullstack2627` alapra épül)

A VM-ben (Docker kell hozzá):

```bash
cd solti-csongor-asda-backend
just init --seed
```

- Frontend: http://frontend.vm1.test
- Backend: http://backend.vm1.test/api/...
- Swagger: http://swagger.vm1.test
- phpMyAdmin: http://pma.vm1.test

Leállítás: `just down`
