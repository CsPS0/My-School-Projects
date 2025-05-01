x:int = int(input('egyik szám: '))
y:int = int(input('másik szám: '))

osszeg:int = 0
for n in range (x+1, y):
    print(n, end='')
    osszeg += n
print(f'\nszámok összege: {osszeg}')