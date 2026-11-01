#region 1.feladat|files
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();

string file = "gyumolcsok.txt";
string[] gyumik = File.ReadAllLines(file);
int sorok = gyumik.Length;
string[] faj = new string[sorok];
int[] kg = new int[sorok];

for (int i = 0; i < sorok; i++)
{
    string[] adatok = gyumik[i].Split(";");
    faj[i] = adatok[0];
    kg[i] = Convert.ToInt32(adatok[1]);
}

Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write($"{file} ");
Console.ResetColor();
Console.WriteLine("sikeresen beolvasva!");
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 2.feladat|?kg
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

for (int i = 0; i < sorok; i++)
{
    Console.WriteLine($"{faj[i]}: {kg[i]} kg");
}
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 3.feladat|?összes
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat");
Console.ResetColor();

int osszKg = 0;
for (int i = 0; i < sorok; i++)
{
    osszKg += kg[i];
}

Console.WriteLine($"Összesen {osszKg} kg gyümölcs termett!");
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 4.feladat|?átlag
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. Feladat");
Console.ResetColor();

double atlag = (double)osszKg / sorok;
Console.WriteLine($"A gyümölcsök mennyiségének átlaga: {atlag:F2} kg");
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 5.feladat|?fajta
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. Feladat");
Console.ResetColor();

int tizesDb = 0;
for (int i = 0; i < sorok; i++)
{
    if (kg[i] == 10)
    {
        tizesDb++;
    }
}

Console.WriteLine($"Pontosan 10 kg gyümölcsből {tizesDb} féle termett!");
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 6.feladat|?max
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. Feladat");
Console.ResetColor();

int maxKg = kg[0];
int maxIndex = 0;
for (int i = 1; i < sorok; i++)
{
    if (kg[i] > maxKg)
    {
        maxKg = kg[i];
        maxIndex = i;
    }
}

Console.WriteLine($"A legtöbb gyümölcs: {faj[maxIndex]}, {maxKg} kg");
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 7.feladat|?x>=30kg
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. Feladat");
Console.ResetColor();

Console.WriteLine("Legalább 30 kg termett az alábbi gyümölcsökből:");
for (int i = 0; i < sorok; i++)
{
    if (kg[i] >= 30)
    {
        Console.WriteLine($"{faj[i]}");
    }
}
#endregion

#region pause
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
}
Console.WriteLine("1 másodperc...");
Thread.Sleep(1000);
Console.ResetColor();
#endregion

#region 8.feladat|x<10kg
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. Feladat");
Console.ResetColor();

bool vanKevesebbMint10 = false;
for (int i = 0; i < sorok; i++)
{
    if (kg[i] < 10)
    {
        Console.WriteLine($"Az első 10 kg-nál kevesebb termett gyümölcs: {faj[i]}");
        vanKevesebbMint10 = true;
        break;
    }
}

if (!vanKevesebbMint10)
{
    Console.WriteLine("Nem volt 10 kg-nál kevesebb gyümölcs.");
}
#endregion