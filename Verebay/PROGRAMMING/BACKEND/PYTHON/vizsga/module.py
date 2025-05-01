from random import choice
import math

#-------------------------------------------------------------------------
                                                                        #|
def feldadat01() -> None:                                               #|
    nev:str = input('Írj be egy nevet: ')                               #|
    kor:str = input('Írd be mikor születtél: ')                         #|
    jelek:list[str] = ['#', '&', '@', '%', '!',]                        #|
                                                                        #|
    p1:str = nev[:4].capitalize()                                       #|
    p2:str = kor[2:]                                                    #|
    p3:str = f'{choice(jelek)}{choice(jelek)}'                          #|
                                                                        #|
    print(f'A jelszavad: {p1}{p2}{p3}')                                 #|
#-------------------------------------------------------------------------

def feladat02() -> None:
    meret:int = 0
    while meret < 5 or meret > 15:
        meret:int = int(input('Írj egy szám 5 és 15 között: '))

    hely:int = int(input(f'Írjon be egy [0; {meret}] közti számot: '))

    if hely < 0 or hely >= meret:
        print('hibás input')
    else:
        abc:str = 'abcdefghijklmnopqrstuvwxyz'
        for index in range(meret):
            if index == hely: print('#', end=' ')
            print(abc[index], end=' ')
        tipp:str = input('Mi lehet a # helyén?: ')
        if tipp == abc[hely]:
            print('helyes!')
        else:
            print(f'nem, a helyes válasz a {abc[hely]}')

#-------------------------------------------------------------------------
class Dijazott:
        def __init__(self, sor:str) -> None:
            v:list[str] = sor.strip().split(';')
            self.ev:int = int(v[0])
            self.nev:str = v[1]
            self.orszag = v[2]

def feladat03() -> None:
    