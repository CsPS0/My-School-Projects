from module import *


print('Első feladat:')
feldadat01()

print('Második feladat:')
feladat02()

print('Harmadik feladat:')
feladat03()

Dijazottak:list[Dijazott] = []

file = open('dijazottak.txt', 'r', encoding='utf-8')
for sor in file:
    Dijazottak.append