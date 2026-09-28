from module import *


#1st work
movies:list[str] = get_movies_from_txt()

f = 0
for year in movies:
    if "1995" in  year:
        f += 1
print(f"1995-ös filmek száma: {f}db")

searched_movie:str = input("ird be helyesen a filmet: ")

index:int = 0
while index < len(movies) and movies[index] != searched_movie:
    index += 1

if index < len(movies):
    print('a keresett érték megtalálható a listában')
    print(f'{searched_movie} indexe: {index} ez a {index+1}. elem')
else:
    print(f'az általad beírt  {searched_movie} nem található(nem szerepel a listában)!')

#2nd work
jahre:list[int] = get_birth_years()

amount = 0
for numero in jahre:
    amount += numero
average = amount /len(jahre)  #átlag egyenlő a mennyiséggel aminek a hossza évek
age = 2024 - average          #

print(age)

most_younger = min(jahre)
print("íjjj a legfiatalabb ember a listában:", most_younger)