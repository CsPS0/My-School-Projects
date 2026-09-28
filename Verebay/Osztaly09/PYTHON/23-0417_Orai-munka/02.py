import random

szamok = [random.randint(50, 150) for _ in range(20)]

szamok.sort()

print("A tömb elemei:", szamok)

osszeg = sum(szamok)
atlag = osszeg / len(szamok)
nullasok = len([x for x in szamok if x % 10 == 0])

print("Az összeg:", osszeg)
print("Az átlag:", atlag)
print("A 0-ra végződő számok száma", nullasok)