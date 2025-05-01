import math
import random

#1. feladat
def veletlen_lista(meret):
  lista = []
  for _ in range(meret):
    lista.append(random.randint(10, 99))
  return lista

#2. feladat
def kiir_lista(lista):
  for i in range(len(lista)):
    if i % 5 == 0:
      print()
    print(lista[i], end=" ")

#3. feladt
def szamol(lista, szam):
  db = 0
  for elem in lista:
    if elem % szam == 0:
      db += 1
  return db

#4. feladat
def osztalyzat_szoveg(osztalyzat):
  if osztalyzat == 1:
    return "elégtelen"
  elif osztalyzat == 2:
    return "elégséges"
  elif osztalyzat == 3:
    return "közepes"
  elif osztalyzat == 4:
    return "jó"
  elif osztalyzat == 5:
    return "jeles"
  else:
    return "Nem jó bazzz"

#5. feladat
def atfogo_szamolo(a, b):
  return math.sqrt(a**2 + b**2)

#6. feladat
def kigyolese(lista):
  uj_lista = []
  for elem in lista:
    if elem % 5 == 0:
      uj_lista.append(elem)
  uj_lista.sort()
  return uj_lista