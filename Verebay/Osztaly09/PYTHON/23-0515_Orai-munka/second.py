nevek:list[str] = []

for _ in range(10):
    nev:str = input('irj be egy nevet: ')
    if nev != '': nevek.append(nev)
    else: break
    
nevek.sort()

print('rendezve: ')
for nev in nevek:
    print(nev)