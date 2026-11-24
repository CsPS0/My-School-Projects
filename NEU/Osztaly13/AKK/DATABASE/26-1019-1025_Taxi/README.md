# Taxi

DATABASE feladat, 2026. 10.19. – 10.25.

Feladatleírás és források: `taxi-ek-diak.pdf`

## Indítás

```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-1019-1025_Taxi"
docker run --name db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/feladat" -d mysql:latest
docker exec -it db mysql -u root -p
```

A `mysql>` promptban (jelszó: `jelszo`):

```sql
SOURCE /feladat/solticsongor-taxi.sql;
```

Takarítás: `docker rm -f db`
