dolgok:list[str] = ['kalap', 'kalap2']

keresett:str = input('mit keresel?')

for dolog in dolgok:
    if keresett == dolog:
        print(f'Ezaz a {keresett} dolgok benne vannak!')
        print(f'Az indexe: {dolgok.index(keresett)}')
        break
else:
    print(f'sajnos a {keresett} adat nincs benne!')