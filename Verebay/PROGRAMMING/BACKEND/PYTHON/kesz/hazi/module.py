import random

def lotto_sorsolas(lottotipus, tipp_lista):
    if lottotipus not in ['5', '6', '7']:
        print("Mondtam, hogy 5, 6 vagy 7-es lottó van, akkor azt mi a fenéért nem lehet megjegyezni?")
        return
    if not all(1 <= tipp <= (90 if lottotipus=='5' else 45 if lottotipus=='6' else 35) for tipp in tipp_lista):
        print(f"Hiba: A tippeknek 1 és {90 if lottotipus=='5' else 45 if lottotipus=='6' else 35} között kell lennie.")
        return
    if lottotipus == '5':
        nyeroszamok = random.sample(range(1, 91), 5)
    elif lottotipus == '6':
        nyeroszamok = random.sample(range(1, 46), 6)
    else:
        nyeroszamok = random.sample(range(1, 36), 7)

#ugye 0 = 1; 1 = 2; 2 = 3; 3 = 4

    nyeroszamok.sort()
    print("A nyerőszámok:", nyeroszamok)

    talalatok = set(tipp_lista) & set(nyeroszamok)
    talalatok_szama = len(talalatok)

    print(f"Hát gec néked {talalatok_szama} találatod van.")
