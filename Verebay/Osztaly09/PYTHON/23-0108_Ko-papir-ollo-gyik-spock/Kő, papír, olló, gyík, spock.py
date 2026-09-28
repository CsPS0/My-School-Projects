#Ugye nincs párom ezért netről néztem a segédletet és egy bot lesz az ellenség. :D

import random

játékos_választása = input("Válasz egyet az ötből(kő, papír, olló, spock, gyík): ")
választható_lehetőségek = ["kő", "papír", "olló", "spock", "gyík"]
számítógép_választása = random.choice(választható_lehetőségek)

print(f"\nTe választásod {játékos_választása}, számítógép választása {számítógép_választása}.\n")

if játékos_választása == számítógép_választása:
    print(f"Mindkét fél ezt választotta: {játékos_választása}. Ez egy döntetlen :I")
elif játékos_választása == "kő":
    if számítógép_választása == "olló":
        print("A kő szétkapja az ollót, ezért te nyertél! :D ")
    else:
        print("A papír bekebelezi a követ, szóval vesztettél! :'( ")
elif játékos_választása == "papír":
    if számítógép_választása == "kő":
        print("A papír bekebelezi a követ, ezért te nyertél! :D ")
    else:
        print("Az olló elvágja a papírt, szóval vesztettél! :'( ")
elif játékos_választása == "olló":
    if számítógép_választása == "papír":
        print("Az olló elvágja a papírt, ezért te nyertél! :D ")
    else:
        print("A kő szétkapja az ollót, szóval vesztettél! :'( ")
elif játékos_választása == "spock":
    if számítógép_választása == "olló":
        print("Spock széttöri az ollót, ezért te nyertél! :D ")
    else:
        print("A gyík megmérgezi Spockot, szóval vesztettél! :'( ")
elif játékos_választása == "gyík":
    if számítógép_választása == "paper":
        print("A gyík megeszi a papírt, ezért te nyertél! :D ")
    else:
        print("A kő összezuzza a gyíkot, szóval vesztettél! :'( ")
        
        #hát nem jó köll egy másik.