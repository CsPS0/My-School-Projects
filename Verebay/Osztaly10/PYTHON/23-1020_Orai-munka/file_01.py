x:int = int(input('Adj meg egy számot: '))
y:int = int(input('Adj meg még egy számot: '))
nork:str = str(input('Ha kivonást akkor (k), ha viszont összeadást akkor (ö) betűt írj: '))

if nork == 'k':
    print(x-y)
elif nork == 'ö':
    print(x+y)