# Tancverseny

DATABASE feladat, 2026. 11.02. – 11.08.

Feladatleírás és források: `feladat.txt`

## Indítás

```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-1102-1108_Tancverseny"
docker run --name db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/feladat" -d mysql:latest
docker exec -it db mysql -u root -p
```

A `mysql>` promptban (jelszó: `jelszo`):

```sql
SOURCE /feladat/solticsongor-tancverseny.sql;
```

Takarítás: `docker rm -f db`
