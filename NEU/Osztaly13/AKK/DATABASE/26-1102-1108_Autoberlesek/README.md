# Autoberlesek

DATABASE feladat, 2026. 11.02. – 11.08.

Feladatleírás és források: `autoberlesek.ab2.pdf`

## Indítás

```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-1102-1108_Autoberlesek"
docker run --name db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/feladat" -d mysql:latest
docker exec -it db mysql -u root -p
```

A `mysql>` promptban (jelszó: `jelszo`):

```sql
SOURCE /feladat/solti-csongor-autoberlesek/solti-csongor-autoberlesek.sql;
```

Takarítás: `docker rm -f db`
