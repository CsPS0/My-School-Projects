# Kondibelepok

DATABASE feladat, 2026. 09.28. – 10.04.

Feladatleírás és források: `kondibelepok.pdf`

## Indítás

```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-0928-1004_Kondibelepok"
docker run --name db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/kondibelepok" -d mysql:latest
docker exec -it db mysql -u root -p
```

A `mysql>` promptban (jelszó: `jelszo`):

```sql
SOURCE /kondibelepok/solti-csongor-kondibelepok.sql;
```

A megoldás fájl a `/kondibelepok` mountról tölti be a táblákat és az adatokat. Egyesével: `SOURCE /kondibelepok/tasks/<szam>.sql;`

Takarítás: `docker rm -f db`
