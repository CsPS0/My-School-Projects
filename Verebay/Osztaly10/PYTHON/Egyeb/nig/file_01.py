print('hello world!')
print(42)
print(f'az elet ertlme: {25+17}')

# galaxis utikalauz stopposoknak
#int, float, bool, str

x:int = 10
y:int = 20

print(x * y)
print(y / x)
print(x + y)
print(y - x)
print(x // y)
print(y % x)
print(x ** y)


print(10 / 3)
print(f'10pi = {10 * 3.14}')

logikai:bool = True # or False

x:int = int(input('írd be az x értékét: '))

if x < 10: print('az x értéke kisebb minz 10')
else: print('az x nagyobb vagy egyenlő, mint 10')

# and
# or
# not -> egy operandusú, a kifejezés elé kell tenni
#       not True => False; not False => True

print(not True)

if x < 10 and y == 32: print('Igaz')
else: print('Hamis')

# str

karakterlanc:str = 'cica'
print(f'{karakterlanc}' * 10)

masik:str = 'alom'
print(karakterlanc + masik)