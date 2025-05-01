from random import randint

szamok:list[int] = []
while len(szamok) < 50:
    rszam:int = randint(10, 99)
    if rszam not in szamok: szamok.append(rszam)
    
szamok.sort()
for i in range(len(szamok)):
    print(szamok[i], end=' ')
    if (i+1) % 10 == 0: print(end= '\n')