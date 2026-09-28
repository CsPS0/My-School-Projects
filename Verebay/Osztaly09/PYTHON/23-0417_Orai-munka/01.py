#a feladat

nevek = ["Tivadar", "Töhötöm", "Csontos", "Etele", "Huba"]
print(nevek)

#b feladat

magassagok = []
for i in range(5):
    magassag = int(input(f"Kérem adja meg a(z) {i+1}. ember magasságát (cm-ben): "))
    magassagok.append(magassag)
print(magassagok)

#c feladat

atlag_magassag = sum(magassagok) / len(magassagok)
print("Az 5 ember átlagmagassága:", atlag_magassag, "cm")

#d feladat

nevek.sort()
print(nevek)

#e feladat

max_magasssag = max(magassagok)
print("A legmagasabb ember", max_magasssag, "cm magas.")

magassagok.sort()
print("Az emberek magasság szerint növekvő sorrendben:", nevek)