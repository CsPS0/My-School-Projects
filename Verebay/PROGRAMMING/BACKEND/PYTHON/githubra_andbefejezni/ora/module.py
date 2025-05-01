import random


def feladat01() -> None:
    vnev:str = input('Írd be a keresztneved: ')
    knev:str = input('Írd be a vezetéknevedet is: ')
    szamok:list[int] = int['1', '2', '3', '4', '5', '6', '7', '8', '9',]
    password:int = int(vnev + knev + szamok(random.choice))

    print(f'A jelszavad nem más mint: {password}-let')

def feladat02() -> None:
    szam:int = int(input('Adj meg egy számot, és ismételd addig, amíg nem jó a feladat végkimenetele')))