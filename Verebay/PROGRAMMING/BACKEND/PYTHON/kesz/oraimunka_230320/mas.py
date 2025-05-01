nevek:list[str] = ['Éva', 'Blanka', 'Renáta', 'Zsuzsa', 'Emese', 'Furujás', 'Sárküzi', 'Csingor', 'PewDiePie', 'Mr. Rodrigo', 'Yasujó']
ures:list = []
szamok:list[int] = [36, 11, 42, 26, 121]

# list elemeinek leírása:
print(nevek)

# lista elemeinek bejárása:
for nev in nevek:
    print(f'a(z) {nev} egy fíú vagy egy női név')
    
print(nevek[5])

nevek[3] = 'Bernadett'

print(nevek)
print(nevek [99])


lista_hossza:int = len(nevek)
print(f'lista hossza: {lista_hossza}')

print(nevek[len(nevek) - 1])
uj_nev:str = 'Furujás'
nevek.append(uj_nev)
nevek.append('Emese')
print(nevek)
for i in range(len(nevek)):
    print(f'a lista {i + 1}.eleme: {nevek[i]}')
        
nevek.sort()
print(nevek)

szamok.sort()
print(szamok)

nevek.remove('Zsuzsa')
print(nevek)

nevek.insert(0, 'Furujás')
print(nevek)