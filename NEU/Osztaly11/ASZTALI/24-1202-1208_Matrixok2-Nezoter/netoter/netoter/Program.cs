#region 1.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();

string[] foglaltsagSorok = File.ReadAllLines("foglaltsag.txt");
string[] kategoriaSorok = File.ReadAllLines("kategoria.txt");

int sorokSzama = foglaltsagSorok.Length;
int szekekSzama = foglaltsagSorok[0].Length;

char[,] foglaltsag = new char[sorokSzama, szekekSzama];
char[,] kategoria = new char[sorokSzama, szekekSzama];

for (int i = 0; i < sorokSzama; i++)
{
    for (int j = 0; j < szekekSzama; j++)
    {
        foglaltsag[i, j] = foglaltsagSorok[i][j];
        kategoria[i, j] = kategoriaSorok[i][j];
    }
}

Console.WriteLine("A fájlok beolvasása sikeres!");
#endregion

Pause();

#region 2.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

Console.ForegroundColor= ConsoleColor.White;
Console.Write("Add meg egy sor számát(0-15): ");
Console.ForegroundColor = ConsoleColor.Yellow;
int bekertSor = int.Parse(Console.ReadLine()!);
Console.ResetColor();

Console.ForegroundColor = ConsoleColor.White;
Console.Write("Add meg egy szék számát(1-20): ");
Console.ForegroundColor = ConsoleColor.Yellow;
int bekertSzek = int.Parse(Console.ReadLine()!);
Console.ResetColor();

if (foglaltsag[bekertSor, bekertSzek] == 'x')
{
    Console.WriteLine("A megadott hely foglalt.");
}
else
{
    Console.WriteLine("A megadott hely szabad.");
}
#endregion

Pause();

#region 3.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat");
Console.ResetColor();


int eladottJegyek = 0;

for (int i = 0; i < sorokSzama; i++)
{
    for (int j = 0; j < szekekSzama; j++)
    {
        if (foglaltsag[i, j] == 'x')
        {
            eladottJegyek++;
        }
    }
}

int teljesKapacitas = sorokSzama * szekekSzama;
int szazalek = (int)Math.Round((double)(eladottJegyek * 100) / teljesKapacitas);

Console.WriteLine($"Az előadásra eddig {eladottJegyek} jegyet adtak el, ez a nézőtér {szazalek}%-a.");
#endregion

Pause();

#region 4.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. Feladat");
Console.ResetColor();

int[] kategoriakSzama = new int[5];

for (int i = 0; i < sorokSzama; i++)
{
    for (int j = 0; j < szekekSzama; j++)
    {
        if (foglaltsag[i, j] == 'x')
        {
            int kategoriaIndex = kategoria[i, j] - '1';
            kategoriakSzama[kategoriaIndex]++;
        }
    }
}

int legtobbKategoriaban = 0;
int legtobbIndex = 0;
for (int i = 0; i < kategoriakSzama.Length; i++)
{
    if (kategoriakSzama[i] > legtobbKategoriaban)
    {
        legtobbKategoriaban = kategoriakSzama[i];
        legtobbIndex = i;
    }
}

Console.WriteLine($"A legtöbb jegyet a(z) {legtobbIndex + 1}. árkategóriában értékesítették.");
#endregion

Pause();

#region 5.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. Feladat");
Console.ResetColor();

int[] arak = { 5000, 4000, 3000, 2000, 1500 };
int bevetel = 0;

for (int i = 0; i < sorokSzama; i++)
{
    for (int j = 0; j < szekekSzama; j++)
    {
        if (foglaltsag[i, j] == 'x')
        {
            int kategoriaIndex = kategoria[i, j] - '1';
            bevetel += arak[kategoriaIndex];
        }
    }
}

Console.WriteLine($"A színház bevétele a pillanatnyilag eladott jegyek alapján: {bevetel} Ft.");
#endregion

Pause();

#region 6.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. Feladat");
Console.ResetColor();

int egyedulalloHelyek = 0;

for (int i = 0; i < sorokSzama; i++)
{
    for (int j = 0; j < szekekSzama; j++)
    {
        if (foglaltsag[i, j] == 'o')
        {
            bool egyedulallo = false;

            if (j == 0)
            {
                if (foglaltsag[i, j + 1] == 'x')
                {
                    egyedulallo = true;
                }
            }
            else if (j == szekekSzama - 1)
            {
                if (foglaltsag[i, j - 1] == 'x')
                {
                    egyedulallo = true;
                }
            }
            else
            {
                if (foglaltsag[i, j - 1] == 'x' && foglaltsag[i, j + 1] == 'x')
                {
                    egyedulallo = true;
                }
            }

            if (egyedulallo)
            {
                egyedulalloHelyek++;
            }
        }
    }
}

Console.WriteLine($"A nézőtéren {egyedulalloHelyek} egyedülálló üres hely van.");
#endregion

Pause();

#region 7.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. Feladat");
Console.ResetColor();

string[] szabadHelyekFajl = new string[sorokSzama];

for (int i = 0; i < sorokSzama; i++)
{
    string sor = "";
    for (int j = 0; j < szekekSzama; j++)
    {
        if (foglaltsag[i, j] == 'x')
        {
            sor += 'x';
        }
        else
        {
            sor += kategoria[i, j];
        }
    }
    szabadHelyekFajl[i] = sor;
}

File.WriteAllLines("szabad.txt", szabadHelyekFajl);
Console.WriteLine("A szabad.txt fájl elkészült.");
#endregion

void Pause()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Nyomj entert a továbblépéshez!");
    while (Console.ReadKey().Key != ConsoleKey.Enter)
    {
        // skibidi hiba bypass
    }
    Console.WriteLine("1 másodperc...");
    Thread.Sleep(1000);
    Console.ResetColor();
}