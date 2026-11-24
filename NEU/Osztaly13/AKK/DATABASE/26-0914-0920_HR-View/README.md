# HR View

DATABASE feladat, 2026. 09.14. – 09.20.

Feladatleírás és források: `hr.view.pdf`

## Indítás

```bash
cd "NEU/Osztaly13/AKK/DATABASE/26-0914-0920_HR-View"
docker run --name db -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/feladat" -v "$(pwd)/tasks:/solticsongor-hr/tasks" -d mysql:latest
docker exec -it db mysql -u root -p
```

A `mysql>` promptban (jelszó: `jelszo`):

```sql
SOURCE /feladat/solti-csongor-hr-view.sql;
```

A megoldás fájl maga tölti be a `tasks/hr.sql`-t, ezért kell a második mount is. A feladatok külön-külön is futtathatók: `SOURCE /solticsongor-hr/tasks/<szam>.sql;`

Takarítás: `docker rm -f db`
