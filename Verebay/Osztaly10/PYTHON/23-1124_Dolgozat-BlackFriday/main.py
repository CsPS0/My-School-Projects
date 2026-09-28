import module
import math

# 1. feladat
lista = module.veletlen_lista(10)
print(lista)

# 2. feladat
module.kiir_lista(lista)

# 3. feladat
szam = 7
db = module.szamol(lista, szam)
print(f"A listában {db} db {szam}-el osztható szám található.")

# 4. feladat
osztalyzat = int(input("Adj meg egy osztályzatot!"))
osztalyzat_szoveg = module.osztalyzat_szoveg(osztalyzat)
print(f"Az osztályzatod: {osztalyzat_szoveg}")

# 5. feladat
a = int(input("Add meg az első befogót:"))
b = int(input("Add meg a második befogót:"))
atfogo = module.atfogo_szamolo(a, b)
print(f"Az átfogó hossza: {atfogo}")

# 6. feladat
lista = [15, 13, 50, 72, 15, 4, 15, 5, 3, 70, 85]
uj_lista = module.kigyolese(lista)
print(uj_lista)