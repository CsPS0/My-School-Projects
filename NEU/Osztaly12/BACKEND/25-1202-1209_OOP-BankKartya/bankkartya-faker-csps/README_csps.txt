Futtatási útmutató (csps)
==========================

A projekt futtatásához szükséges, hogy a VM-en (vmbox) telepítve legyen a PHP és a Composer.

Lépések:

1. Nyiss egy terminált vagy parancssort.

2. Navigálj a projekt könyvtárába:
   cd bankkartya-faker-csps

3. Telepítsd a szükséges csomagokat (Faker) a composer segítségével:
   composer install
   
   Ez létrehozza a 'vendor' mappát és az 'autoload.php' fájlt.

4. A szkript futtatása (normál mód):
   php bankkartya.php
   
   Kimenet példa:
   Kártya típusa: Visa
   Kártyaszám: 4532-1234-5678-9123
   Kártya lejárati ideje (hó/év): 08/26
   CCV: 123
   Név: Kovács János

5. A szkript futtatása lejárt kártya generálásához (8. feladat):
   php bankkartya.php lejart
   
   Ebben az esetben a lejárati idő egy múltbéli dátum lesz.
