szó = input('Írj be egy szót: ')
print(f'kurva menő szó a(z) hogy {szó}!')

szam = int(input('Hányszor ismételjem azt a szót? '))

for x in range(szam):
    print(f'{szó}', end='\n')