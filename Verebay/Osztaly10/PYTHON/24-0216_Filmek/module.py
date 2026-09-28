from random import randint, seed

def get_movies_from_txt() -> list[str]:
    movies:list[str] = []
    for r in open('filmek.txt', 'r', encoding='utf-8'):
        movies.append(r.strip())
    return movies

#nekem nem ment az utf-8 a VSC-ben, de nem nyúltam hozzá

def get_birth_years() -> list[int]:
    seed(1337)
    years:list[int] = []
    for _ in range(13):
        years.append(randint(2001, 2012))
    return years