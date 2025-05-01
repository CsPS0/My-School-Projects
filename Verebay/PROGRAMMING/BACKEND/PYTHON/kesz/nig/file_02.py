# elágazás (szelekció)
nev:str = input('what is your name?:')
valasz:str = input('do you like programming?:')

if nev == 'Zoli' and valasz == 'igen':
    print('Well my frien you are the GodCaesar!')
else: print('kinemszarjale')

#többágú szelekció
hm:int = int(input)('hány fok van odakint?:')

if hm < 15:
    print('vegyél sapkát, mert anyukád fázik!')
elif hm > 27:
    print('Ez meleg')
else: print('oksa')

# ciklus (literáció)