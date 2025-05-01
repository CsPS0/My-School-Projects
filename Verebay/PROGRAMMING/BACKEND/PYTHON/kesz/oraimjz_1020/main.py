from module import keretez
valami:str = input('Írj be valamit: ')
keretes = keretez(valami)
print(keretes)

szavak:list[str] = ['cica', 'kutya', 'kacsa']
for szo in szavak:
    print(keretez(szo)+ '\n')