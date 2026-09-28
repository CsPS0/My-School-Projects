from random import randint

szamok:list[int] = []

for _ in range(50):
    rszam:int = randint(5, 49) * 2 + 1
    szamok.append(rszam)
    
for i in range(len(szamok)):
    print(szamok[i], end='')
    if (i+1) % 10 == 0: print(end= '\n')
    
# eldöntés tétele
    
i:int = 0
while i < len(szamok) and szamok[i] != 13:
    i += 1
    
if i < len(szamok):
    print('van benne 13')
else:
    print('nincs benne 13')
    
# vagy

for szam in szamok:
    if szam == 13:
        print('van benne 13')
        break
else:
    print('nincs benne 13')
    
# ciklushoz tartozó else' CSAK akkor futhat le, ha a ciklus futása alatt SOHA NEM volt 'break'


#ez is jó (könnyű)
if 13 in szamok:
    print('van benne 13')
else:
    print('nincs benne 13')
    