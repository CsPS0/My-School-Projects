from module import lotto_sorsolas

def main():
    lottotipus = input("Válasszá lottó típust (5, 6, 7): ")
    tipp_lista = []
    
    for i in range(int(lottotipus)):
        while True:
            try:
                tipp = int(input(f"Adja meg a(z) {i + 1}. tippjét (1-{90 if lottotipus=='5' else 45 if lottotipus=='6' else 35}): "))
                tipp_lista.append(tipp)
                break
            except ValueError:
                print("Csak számokat gecó!")

    lotto_sorsolas(lottotipus, tipp_lista)

if __name__ == "__main__":
    main()



#a try-t, a ValueError-t és a &(and)-et netről néztem, mert nem ment a randomizációs szelekció + baszodt nem kiirni a hibát
#Solti Csongor Péter