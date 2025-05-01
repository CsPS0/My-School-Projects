nevek:list[str] = []
beirt_nev:str = '---'
while len(nevek) < 10 and beirt_nev != '':
    beirt_nev:str = input('irj be egy nevet:')
    if beirt_nev != '': nevek.append(beirt_nev)
    elif beirt_nev != '': beirt_nev.count(10)