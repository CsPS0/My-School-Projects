# Kutya Nyilvántartás - Adatkezelés

Ez a projekt egy vanilla JavaScript frontendből és egy JSON-server alapú backendből áll.

## Manuális futtatás (Docker nélkül)

A futtatáshoz két külön terminálra lesz szükség.

### 1. Backend (JSON Server) beállítása és futtatása

1. Navigáljon a backend mappába:
   ```bash
   cd solticsongor-Adatkezeles/www/json-server
   ```
2. Telepítse a szükséges függőségeket (csak első alkalommal):
   ```bash
   npm install json-server@0.17.4 body-parser multer pluralize lodash
   ```
3. Indítsa el a szervert:
   ```bash
   node index.js
   ```
   *A szerver a **8888**-as porton fog futni.*

### 2. Frontend futtatása

1. Navigáljon a frontend mappába:
   ```bash
   cd solticsongor-Adatkezeles/www/webprog
   ```
2. Indítsa el a webkiszolgálót (például a `serve` csomaggal):
   ```bash
   npx serve -l 3000 .
   ```
   *A weboldal a **http://localhost:3000** címen lesz elérhető.*

## Docker alapú futtatás

### Build
```sh
docker build -t <monogram>/json-server:26 .
```

### Futtatás
```sh
docker run -d --rm -p 8888:3000 -v $(pwd):/app -v /app/node_modules <monogram>/json-server:26
```
