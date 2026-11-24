# Urhajozas

DATABASE feladat, 2026. 09.21. – 09.27.

Feladatleírás és források: `urhajozas.pdf`

## Indítás

```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-0921-0927_Urhajozas"
docker run --name db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/urhajozas" -d mysql:latest
docker exec -it db mysql -u root -p
```

A `mysql>` promptban (jelszó: `jelszo`):

```sql
SOURCE /urhajozas/solti-csongor-urhajozas.sql;
```

A megoldás fájl a `/urhajozas` mountról tölti be a táblákat és az adatokat. Egyesével: `SOURCE /urhajozas/tasks/<szam>.sql;`

Takarítás: `docker rm -f db`
