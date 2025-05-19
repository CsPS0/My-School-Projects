#region files
string[] madar1 = File.ReadAllLines("madarak.txt");
string[] madar2 = File.ReadAllLines("madarak2.txt");

int sor1 = int.Parse(madar1[0]);
int oszlop1 = int.Parse(madar1[1]);

int[,] adat1 = new int[sor1, oszlop1];

for (int i = 0; i < sor1; i++)
{
    string[] sorok1 = madar1[i + 1].Split(" ");
    for (int j = 0; j < oszlop1; j++)
    {
        adat1[i, j] = int.Parse(sorok1[j]);
    }
}

int sor2 = int.Parse(madar2[0]);
int oszlop2 = int.Parse(madar2[1]);

int[,] adat2 = new int[sor2, oszlop2];

for (int i = 0; i < sor2; i++)
{
    string[] sorok2 = madar2[i + 1].Split(" ");
    for (int j = 0; j < oszlop2; j++)
    {
        adat2[i, j] = int.Parse(sorok2[j]);
    }
}

#endregion

#region 1.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();

int osszesMadar = 0;

for (int i = 0; i < sor1; i++)
{
    for (int j = 0; j < oszlop1; j++)
    {
        osszesMadar += adat1[i, j];
    }
}

Console.WriteLine($"Az összes madár száma: {osszesMadar}.");
#endregion

Pause();

#region 2.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

int legkisebbSzam = int.MaxValue;
int legkisebbFajIndex = -1;

for (int j = 0; j < oszlop1; j++)
{
    int fajOsszes = 0;
    for (int i = 0; i < sor1; i++)
    {
        fajOsszes += adat1[i, j];
    }
    if (fajOsszes < legkisebbSzam)
    {
        legkisebbSzam = fajOsszes;
        legkisebbFajIndex = j + 1;
    }
}

Console.WriteLine($"A legkevesebb madár a(z) {legkisebbFajIndex} fajból volt: {legkisebbSzam}.");
#endregion

Pause();

#region 3.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat");
Console.ResetColor();

int legtobbMadaratVaros = -1;
int legtobbMadaratSzam = 0;

for (int i = 0; i < sor1; i++)
{
    int varosMadarSzam = 0;
    for (int j = 0; j < oszlop1; j++)
    {
        varosMadarSzam += adat1[i, j];
    }
    if (varosMadarSzam > legtobbMadaratSzam)
    {
        legtobbMadaratSzam = varosMadarSzam;
        legtobbMadaratVaros = i + 1;
    }
}

Console.WriteLine($"A legtöbb madarat a(z) {legtobbMadaratVaros} helységben figyelték meg: {legtobbMadaratSzam}.");
#endregion

Pause();

#region 4.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. Feladat");
Console.ResetColor();

double atlag = (double)osszesMadar / sor1;
int atlagFolott = 0;

for (int i = 0; i < sor1; i++)
{
    int varosOsszes = 0;
    for (int j = 0; j < oszlop1; j++)
    {
        varosOsszes += adat1[i, j];
    }
    if (varosOsszes > atlag)
    {
        atlagFolott++;
    }
}

Console.WriteLine($"Az átlagnál több madarat {atlagFolott} helységben figyeltek meg.");
#endregion

Pause();

#region 5.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. Feladat");
Console.ResetColor();

bool uresVarosVan = false;

for (int i = 0; i < sor1; i++)
{
    int varosMadarSzam = 0;
    for (int j = 0; j < oszlop1; j++)
    {
        varosMadarSzam += adat1[i, j];
    }
    if (varosMadarSzam == 0)
    {
        Console.WriteLine($"A(z) {i + 1} helységben egyetlen madarat sem figyeltek meg.");
        uresVarosVan = true;
    }
}

if (!uresVarosVan)
{
    Console.WriteLine("Nem volt olyan helység, ahol egyetlen madarat sem láttak.");
}
#endregion

Pause();

#region 6.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. Feladat");
Console.ResetColor();

for (int j = 0; j < oszlop1; j++)
{
    int maxMadarszam6 = 0;
    int maxVarosIndex = -1;

    for (int i = 0; i < sor1; i++)
    {
        if (adat1[i, j] > maxMadarszam6)
        {
            maxMadarszam6 = adat1[i, j];
            maxVarosIndex = i + 1;
        }
    }

    Console.WriteLine($"A(z) { j + 1} fajból a legtöbbet a(z) { maxVarosIndex} helységben figyelték meg: { maxMadarszam6}");
}
#endregion

Pause();

#region 7.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. Feladat");
Console.ResetColor();

int mindegyikVarosbanFajok = 0;

for (int j = 0; j < oszlop1; j++)
{
    bool mindegyikVarosban = true;

    for (int i = 0; i < sor1; i++)
    {
        if (adat1[i, j] == 0)
        {
            mindegyikVarosban = false;
            break;
        }
    }

    if (mindegyikVarosban)
    {
        mindegyikVarosbanFajok++;
    }
}

Console.WriteLine($"Ennyi madárfaj fordult elő mindegyik helységben: {mindegyikVarosbanFajok}");
#endregion

Pause();

#region 8.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. Feladat");
Console.ResetColor();

int maxMadarszam = 0;

for (int i = 0; i < sor1; i++)
{
    int varosMadarSzam = 0;
    for (int j = 0; j < oszlop1; j++)
    {
        varosMadarSzam += adat1[i, j];
    }
    if (varosMadarSzam > maxMadarszam)
    {
        maxMadarszam = varosMadarSzam;
    }
}

for (int i = 0; i < sor1; i++)
{
    int varosMadarSzam = 0;
    for (int j = 0; j < oszlop1; j++)
    {
        varosMadarSzam += adat1[i, j];
    }
    if (varosMadarSzam == maxMadarszam)
    {
        Console.WriteLine($"Maximális számú madarat a(z) {i + 1} helységben figyelték meg.");
    }
}
#endregion

Pause();

#region 9.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. Feladat");
Console.ResetColor();

for (int i = 0; i < sor1; i++)
{
    bool mindenMadarfajVan = true;

    for (int j = 0; j < oszlop1; j++)
    {
        if (adat1[i, j] == 0)
        {
            mindenMadarfajVan = false;
            break;
        }
    }

    if (mindenMadarfajVan)
    {
        Console.WriteLine($"A(z) {i + 1} helységben mindenféle madár előfordult.");
    }
}
#endregion

Pause();

#region 10.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("10. Feladat");
Console.ResetColor();

bool csakEgyHelyenVolt = false;

for (int j = 0; j < oszlop1; j++)
{
    int helyszinekSzama = 0;

    for (int i = 0; i < sor1; i++)
    {
        if (adat1[i, j] > 0)
        {
            helyszinekSzama++;
        }
    }

    if (helyszinekSzama == 1)
    {
        Console.WriteLine($"A(z) {j + 1} madárfaj csak 1 helyen fordult elő.");
        csakEgyHelyenVolt = true;
    }
}

if (!csakEgyHelyenVolt)
{
    Console.WriteLine("Nem volt olyan madárfaj, amely csak 1 helyen fordult elő.");
}
#endregion

Pause();
void Pause()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Nyomj entert a továbblépéshez!");
    while (Console.ReadKey().Key != ConsoleKey.Enter)
    {
        //skidibi metódus warning bypass
    }
    Console.WriteLine("1 másodperc...");
    Thread.Sleep(1000);
    Console.ResetColor();
}