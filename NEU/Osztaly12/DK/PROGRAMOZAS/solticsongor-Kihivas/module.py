import sys

sys.stdout.reconfigure(encoding='utf-8')
sys.stdin.reconfigure(encoding='utf-8')

def kihivas() -> None:
    print("1. feladat")
    aktivitas = input("Adja meg az aktivitását: ")

    print("2. feladat")
    u_count = aktivitas.count('U')
    g_count = aktivitas.count('G')
    f_count = aktivitas.count('F')
    k_count = aktivitas.count('K')
    
    tavolsag = (u_count * 1) + (g_count * 1) + (f_count * 2) + (k_count * 10)
    print(f"Az elért távolság: {tavolsag} km.")

    print("3. feladat")
    jutalom = 0
    if u_count > 0 and g_count > 0 and f_count > 0 and k_count > 0:
        jutalom = 10
        print("Bravo! Jutalma még 10 km.")
    else:
        print("Nem jár jutalom.")

    print("4. feladat")
    osszesen = tavolsag + jutalom
    siker = "Gratulálok, kihívás teljesítve!" if osszesen >= 40 else "Legközelebb sikerül!"
    print(f"Eredménye: {osszesen} km. {siker}")