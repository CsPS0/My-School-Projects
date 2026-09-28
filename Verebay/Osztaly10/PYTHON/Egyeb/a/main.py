from modul import *

szamok:list[int] = get_rnd_list(50, 0, 20)

print_table_view(szamok)

pesz:int = get_even_count(szamok)
print(f'páros elemek száma: {pesz}')