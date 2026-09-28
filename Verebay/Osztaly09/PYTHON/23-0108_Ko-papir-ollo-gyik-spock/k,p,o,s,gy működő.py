import random

játékos_választása = input("Válasz egyet az ötből(kő, papír, olló, spock, gyík): ")
választható_lehetőségek = ["kő", "papír", "olló", "spock", "gyík"]
számítógép_választása = random.choice(választható_lehetőségek)

print(f"\nTe választásod {játékos_választása}, számítógép választása {számítógép_választása}.\n")

#Akkor most elöször amikor csak nyerhetsz:

if játékos_választása == számítógép_választása:
    print(f"Mindkét fél ezt választotta: {játékos_választása}. Ez egy döntetlen :I")
elif játékos_választása == "kő":
    if számítógép_választása == "olló":
        print("A kő szétkapja az ollót, ezért te nyertél! :D ")
    if számítógép_választása == "gyík":
        print("A kő összezuzza a gyíkot, ezért te nyertél! :D ")
        
    if számítógép_választása == "papír":
        print("A papír bekebelezi a követ, szóval vesztettél! :'( ")
    if számítógép_választása == "spock":
        print("Spock elpárologtatja a követ, szóval vesztettél! :'( ")
        
        
elif játékos_választása == "papír":
    if számítógép_választása == "kő":
        print("A papír bekebelezi a követ, ezért te nyertél! :D ")
    if számítógép_választása == "spock":
        print("A papír megcáfollja Spockot, ezért te nyertél! :D ")
        
    if számítógép_választása == "olló":
        print("Az olló elvágja a papírt, szóval vesztettél! :'( ")
    if számítógép_választása == "gyík":
        print("A gyík megeszi a papírt, szóval vesztettél! :'( ")
        
        
elif játékos_választása == "olló":
    if számítógép_választása == "papír":
        print("Az olló kettévágja a papírt, ezért te nyertél! :D ")
    if számítógép_választása == "gyík":
        print("Az olló kettévágja a gyíkot, ezért te nyertél! :D ")
        
    if számítógép_választása == "kő":
        print("A kő szétkapja az ollót, szóval vesztettél! :'( ")
    if számítógép_választása == "spock":
        print("Spock széttöri az ollót, szóval vesztettél! :'( ")
        
        
elif játékos_választása == "spock":
    if számítógép_választása == "olló":
        print("Spock szétkapja az ollót, ezért te nyertél! :D ")
    if számítógép_választása == "kő":
        print("Spock elpárologtatja a követ, ezért te nyertél! :D ")
        
    if számítógép_választása == "papír":
        print("A papír megcáfollja Spockot, szóval vesztettél! :'( ")
    if számítógép_választása == "gyík":
        print("A gyík megmérgezi Spockot, szóval vesztettél! :'( ")
        
        
elif játékos_választása == "gyík":
    if számítógép_választása == "spock":
        print("A gyík megmérgezi Spockot, ezért te nyertél! :D ")
    if számítógép_választása == "papír":
        print("A gyík megesszi a papírt, ezért te nyertél! :D ")
        
    if számítógép_választása == "kő":
        print("A kő összezuzza a gyíkot, szóval vesztettél! :'( ")
    if számítógép_választása == "olló":
        print("Az olló kettévágja a gyíkot, szóval vesztettél! :'( ")