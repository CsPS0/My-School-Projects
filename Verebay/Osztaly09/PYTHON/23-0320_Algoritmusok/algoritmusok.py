from random import randint

randomszamok:list[int] = []
for x in range(10):
    randomszamok.append(randint(10, 99))
print(randomszamok)


s:int = 0
for szam in randomszamok:
    s += szam
print(f'számok összege: {s}')

maxi:int = 0
for i in range(1, len(randomszamok)):
    if randomszamok[i] > randomszamok[maxi]:
        maxi = i
print(f'legnagyobb elem indexe: {maxi}')