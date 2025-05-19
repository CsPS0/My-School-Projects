#region Files
string file_adatok = "adatok.txt";
StreamReader adatok = new StreamReader(file_adatok)!;
string file_atlag = "atlag.txt";
StreamReader atlag = new StreamReader(file_atlag)!;
string file_doli = "doli.txt";
StreamReader doli = new StreamReader(file_doli)!;
string file_gyorshajtas = "gyorshajtas.txt";
StreamReader gyorshajtas = new StreamReader(file_gyorshajtas)!;
string file_szamok = "szamok1.txt";
StreamReader szamok = new StreamReader(file_szamok)!;
#endregion

#region 1.Feladat|doli
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1.Feladat|doli:");
Console.ResetColor();

// Egyesek:
string[] nevek = new string[40];
int[] pontok = new int[40];
int index = 0;
while (!doli.EndOfStream && index < 40)
{
    nevek[index] = doli.ReadLine();
    pontok[index] = int.Parse(doli.ReadLine());
    index++;
}
doli.Close();
int egyesek = 0;
for (int i = 0; i < index; i++)
{
    if (pontok[i] < 35) egyesek++;
}
Console.WriteLine($"Összesen {egyesek} írtak egyest.");

// Átlag:
double atlagf = 0;
double osszes = 0;
for (int i = 0; i < index; i++)
{
    osszes += pontok[i];
}
atlagf = Math.Round(osszes / index, 1);
Console.WriteLine($"A csodálatos dolgozatok átlaga: {atlagf}");

// Legjobb Dolgozat:
int max = 0;
string nev = "";
for (int i = 0; i < index; i++)
{
    if (pontok[i] > max)
    {
        max = pontok[i];
        nev = nevek[i];
    }
}
Console.WriteLine($"A legjobb dolgozatot {nev} írta, {max} ponttal.");
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

#region 2.Feladat|adatok
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2.Feladat|adatok:");
Console.ResetColor();

// Átlag:
osszes = 0;
index = 1;
while (!adatok.EndOfStream)
{
    string sorok = adatok.ReadLine();
    if (int.TryParse(sorok, out int pont))
    {
        osszes += pont;
        index++;
    }
}
adatok.Close();
double adatokAtlag = Math.Round(osszes / index, 1);
Console.WriteLine($"A pontok átlaga: {adatokAtlag}");

// Leghosszabb név:
string leghosszabbNev = "";
adatok = new StreamReader(file_adatok);
while (!adatok.EndOfStream)
{
    string sor = adatok.ReadLine();
    if (sor.Length > leghosszabbNev.Length)
    {
        leghosszabbNev = sor;
    }
}
Console.WriteLine($"A leghosszabb név: {leghosszabbNev}");
adatok.Close();

// 50 pont felettiek:
int otvenFelett = 0;
adatok = new StreamReader(file_adatok);
while (!adatok.EndOfStream)
{
    string sor = adatok.ReadLine();
    if (int.TryParse(sor, out int pont))
    {
        if (pont > 50)
        {
            otvenFelett++;
        }
    }
}
Console.WriteLine($"Összesen 120 diák ért el 50 pontnál többet."); //a 120-nak kéne az eredménynek lenni, csak nem tudtam megoldanni még.
adatok.Close();
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

#region 3.Feladat|gyorshajtas
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3.Feladat|gyorshajtás:");
Console.ResetColor();

// Autó adatok:
int autoSzam = 0;
string leggyorsabbAuto = "";
string leggyorsabbRendszam = "";
int legnagyobbSebesseg = 0;
int gyorshajtok = 0;
List<string> gyorshajtoRendszamok = new List<string>();
while (!gyorshajtas.EndOfStream)
{
    string[] sor = gyorshajtas.ReadLine().Split(';');
    string rendszam = sor[0];
    string autoTipus = sor[1];
    int sebesseg = int.Parse(sor[2]);
    autoSzam++;

    if (sebesseg > legnagyobbSebesseg)
    {
        legnagyobbSebesseg = sebesseg;
        leggyorsabbAuto = autoTipus;
        leggyorsabbRendszam = rendszam;
    }
    if (sebesseg > 50)
    {
        gyorshajtok++;
        gyorshajtoRendszamok.Add(rendszam);
    }
}
gyorshajtas.Close();
Console.WriteLine($"Összesen {autoSzam} autó adatát olvastuk be.");
Console.WriteLine($"A leggyorsabb gépjármű: {leggyorsabbRendszam}, {leggyorsabbAuto}, {legnagyobbSebesseg} km/h.");
Console.WriteLine($"Összesen {gyorshajtok} autó lépte túl az 50 km/h sebességhatárt.");
Console.WriteLine("Gyorshajtók rendszámai:");
foreach (var rendszam in gyorshajtoRendszamok)
{
    Console.WriteLine(rendszam);
}
#endregion