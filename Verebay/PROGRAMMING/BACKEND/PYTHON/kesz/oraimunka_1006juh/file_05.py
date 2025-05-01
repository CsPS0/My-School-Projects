osszeg:int = 0
szam:int = 3456
while szam % 10 != 0:
    szam = int(input(f'Írj be egy számot:'))
    osszeg += szam
print(f'számok összege: {osszeg}')

while True: 
    szam:int = int(input('Írj be egy számot: '))