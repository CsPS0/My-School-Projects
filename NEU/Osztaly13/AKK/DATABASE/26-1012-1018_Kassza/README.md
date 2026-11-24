# Kassza

DATABASE feladat, 2026. 10.12. – 10.18.

Feladatleírás és források: `feladat.txt`

## Indítás

```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-1012-1018_Kassza"
docker run --name db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/feladat" -d mysql:latest
docker exec -it db mysql -u root -p
```

A `mysql>` promptban (jelszó: `jelszo`):

```sql
SOURCE /feladat/sql/kassza.sql;
SOURCE /feladat/sql/solticsongor-kassza-megoldas.sql;
```

Takarítás: `docker rm -f db`
