# meg kell adnunk tippeket [5db, különböző, 1-90]
#



tippek:list[str] = []
ssz:int = 1
while len(tippek) < 5:
    tipp:int = int(input(f'kérem a(z) {assz}. tippet: '))
    if tipp not in tippek and 1 <= tipp <= 90:
        ssz += 1
        tippek.append(tipp)
    else:
        print('nem megfelelő tipp, probéld újra')
        
nyeroszamok:list[int] = []
while len(nyeroszamok) < 5:
    tipp:int = randint(1, 90)
    if nyszam not in nyeroszamok:
        nyeroszamok.append(nyszam)
        
talalatok:int = 0
for tipp in tippek:
    if tipp in nyeroszamok:
        talalatok += 1

tippek.sort()
nyeroszamok.sort()
print(f'tippek: {tippek}')
print(f'nyeroszamok: {nyeroszamok}')