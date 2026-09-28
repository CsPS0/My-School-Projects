szamlalo:int = 1
while szamlalo <= 10:
    allat:str = input(f'Írd be a(z) {szamlalo}. allatot: ')
    szamlalo += 1
    if allat == 'fotel':break
else: print('ügyes vagy, egyik állat sem fotel!')