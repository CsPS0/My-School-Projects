# Telekommunikacio

DATABASE feladat, 2026. 11.09. – 11.15.

Feladatleírás és források: `telekommunikacio-feladat.pdf`

## Indítás

```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-1109-1115_Telekommunikacio"
docker run --name db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/feladat" -d mysql:latest
docker exec -it db mysql -u root -p
```

A `mysql>` promptban (jelszó: `jelszo`):

```sql
SOURCE /feladat/solticsongor-Telekommunikacio.sql;
SOURCE /feladat/solticsongor-TelekommunikacioOneTable.sql;
```

Takarítás: `docker rm -f db`
