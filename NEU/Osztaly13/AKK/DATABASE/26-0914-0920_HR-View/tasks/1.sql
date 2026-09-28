-- 1. feladat
-- opcionális: docker pull mysql:latest
-- mysql indítása dockerrel (a tasks mappából): docker run --name mymysql -e MYSQL_ROOT_PASSWORD=jelszo -v "$(pwd):/solticsongor-hr/tasks" -d mysql:9.7.1
-- csatlakozás: docker exec -it mymysql mysql -p
SOURCE /solticsongor-hr/tasks/hr.sql;
