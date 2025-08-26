import random

def fej_vagy_iras():
    valasz = random.randint(0, 1)
    if valasz == 0:
        print("Fej")
    else:
        print("Írás")