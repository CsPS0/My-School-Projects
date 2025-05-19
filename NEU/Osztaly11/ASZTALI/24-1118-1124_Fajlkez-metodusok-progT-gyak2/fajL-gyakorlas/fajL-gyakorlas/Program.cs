#region Files
using System.Globalization;

string datas = "adatok.txt";
string[] adatok = File.ReadAllLines(datas);

string upgraduate = "felveteli.csv";
string[] felveteli = File.ReadAllLines(upgraduate);

string pointscores = "pontszamok.txt";
string[] pontszamok = File.ReadAllLines(pointscores);

#endregion

#region 1.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();

//a
Console.WriteLine($"{adatok.Length} db került rögzítésre a diákokból.");

//b

Console.Write("Adj meg egy évszámot (2016-2019): ");
Console.ForegroundColor= ConsoleColor.Yellow;
int evszam = int.Parse(Console.ReadLine()!);
Console.ResetColor();
int db = 0;

for (int i = 1; i <= evszam; i++)
{
    string[] reszek = adatok[1].Split(' ');
    int ev = int.Parse(reszek[0]);
    if (ev == evszam)
    {
        db++;
    }
}

Console.WriteLine($"{evszam} évben {db} diák kezdte az iskolát.");

//c
Console.Write("Adj meg egy nevet: ");
Console.ForegroundColor = ConsoleColor.Yellow;
string nev = Console.ReadLine()?.ToLower();
Console.ResetColor();

bool van = false;
string evOsztaly = "";

for (int i = 0; i < adatok.Length; i++)
{
    string[] reszek = adatok[i].Split(' ');
    string diakNev = reszek[2];
    if (diakNev.Equals(nev, StringComparison.OrdinalIgnoreCase))
    {
        van = true;
        evOsztaly = $"{reszek[0]}-ban, {reszek[1]} osztályban";
        break;
    }
}

if (van)
{
    Console.WriteLine($"{nev} megtalálható az adatok között: {evOsztaly}");
}
else
{
    Console.WriteLine($"{nev} nem található az adatok között.");
}

//d
Console.Write("Adj meg egy név részletet: ");
Console.ForegroundColor = ConsoleColor.Yellow;
string reszlet = Console.ReadLine()?.ToLower();
Console.ResetColor();

bool talalt = false;

for (int i = 0; i < adatok.Length; i++)
{
    string[] reszek = adatok[i].Split(' ');
    string diakNev = reszek[2];
    if (diakNev.IndexOf(reszlet, StringComparison.OrdinalIgnoreCase) >= 0)
    {
        Console.WriteLine($"Találat: {reszek[0]} {reszek[1]} {diakNev}");
        talalt = true;
    }
}

if (!talalt)
{
    Console.WriteLine("Nincs találat a megadott név részletre.");
}

//e

string leghosszabbNev = "";
string evOsztalyE = "";

for (int i = 0; i < adatok.Length; i++)
{
    string[] reszek = adatok[i].Split(' ');
    string diakNev = reszek[2];
    if (diakNev.Length > leghosszabbNev.Length)
    {
        leghosszabbNev = diakNev;
        evOsztaly = $"{reszek[0]} {reszek[1]}";
    }
}

Console.WriteLine($"A leghosszabb nevű diák: {leghosszabbNev}, {evOsztalyE}.");

//f

using (StreamWriter writer = new StreamWriter("nevsor.txt"))
{
    for (int i = 0; i < adatok.Length; i++)
    {
        string[] reszek = adatok[i].Split(' ');
        writer.WriteLine(reszek[2]);
    }
}

Console.WriteLine("A nevsor.txt állomány elkészült.");
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

#region 2.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

//a
for (int i = 0; i < pontszamok.Length; i += 2)
{
    string Anev = pontszamok[i];
    int pontszam = int.Parse(pontszamok[i + 1]);
    Console.WriteLine($"{Anev} - {pontszam} pont");
}
//b
int versenyzokSzama = pontszamok.Length / 2;
Console.WriteLine($"A fájl {versenyzokSzama} versenyző adatait tartalmazza.");
//c
int legkisebb = int.MaxValue;
int legnagyobb = int.MinValue;

for (int i = 1; i < pontszamok.Length; i += 2)
{
    int pontszam = int.Parse(pontszamok[i]);
    if (pontszam < legkisebb) legkisebb = pontszam;
    if (pontszam > legnagyobb) legnagyobb = pontszam;
}

Console.WriteLine($"Legkisebb pontszám: {legkisebb}");
Console.WriteLine($"Legnagyobb pontszám: {legnagyobb}");
//d
int osszeg = 0;

for (int i = 1; i < pontszamok.Length; i += 2)
{
    osszeg += int.Parse(pontszamok[i]);
}

double atlag = (double)osszeg / versenyzokSzama;
Console.WriteLine($"Az átlagos pontszám: {atlag:F2}");
//e
Console.Write("Adj meg egy versenyző nevet: ");
string keresettNev = Console.ReadLine();
bool talalat = false;

for (int i = 0; i < pontszamok.Length; i += 2)
{
    if (pontszamok[i].Equals(keresettNev, StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine($"{keresettNev} elért pontszáma: {pontszamok[i + 1]}");
        talalat = true;
        break;
    }
}

if (!talalat)
{
    Console.WriteLine($"Nem találtam a versenyzőt: {keresettNev}");
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

#region 3.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat");
Console.ResetColor();

//a
foreach (string sor in felveteli)
{
    string[] reszek = sor.Split(';');
    string AAnev = reszek[0];
    int hozott = int.Parse(reszek[1]);
    int szerzett = int.Parse(reszek[2]);
    int osszpontszam = hozott + szerzett;

    Console.WriteLine($"{AAnev} - Hozott: {hozott}, Szerzett: {szerzett}, Összpontszám: {osszpontszam}");
}
//b
string legjobbDiak = "";
int maxPontszam = int.MinValue;

foreach (string sor in felveteli)
{
    string[] reszek = sor.Split(';');
    string Bnev = reszek[0];
    int hozott = int.Parse(reszek[1]);
    int szerzett = int.Parse(reszek[2]);
    int osszpontszam = hozott + szerzett;

    if (osszpontszam > maxPontszam)
    {
        maxPontszam = osszpontszam;
        legjobbDiak = Bnev;
    }
}

Console.WriteLine($"A legnagyobb összpontszámú diák: {legjobbDiak} - {maxPontszam} pont");
//c
int Cosszeg = 0;
int diakokSzama = felveteli.Length;

foreach (string sor in felveteli)
{
    string[] reszek = sor.Split(';');
    int hozott = int.Parse(reszek[1]);
    int szerzett = int.Parse(reszek[2]);
    Cosszeg += hozott + szerzett;
}

double Catlag = (double)osszeg / diakokSzama;
Console.WriteLine($"Az átlagos összpontszám: {Catlag:F1}");
//d
Console.Write("Adj meg egy diák nevet: ");
string DkeresettNev = Console.ReadLine();
bool Dtalalat = false;

foreach (string sor in felveteli)
{
    string[] reszek = sor.Split(';');
    if (reszek[0].Equals(DkeresettNev, StringComparison.OrdinalIgnoreCase))
    {
        int hozott = int.Parse(reszek[1]);
        int szerzett = int.Parse(reszek[2]);
        int osszpontszam = hozott + szerzett;

        Console.WriteLine($"{DkeresettNev} - Hozott: {hozott}, Szerzett: {szerzett}, Összpontszám: {osszpontszam}");
        Dtalalat = true;
        break;
    }
}

if (!talalat)
{
    Console.WriteLine($"Nem találtam a diákot: {DkeresettNev}");
}
//e
using (StreamWriter writer = new StreamWriter("osszpontok.csv"))
{
    writer.WriteLine($"Összesen {felveteli.Length} diák adatait tartalmazza.");
    foreach (string sor in felveteli)
    {
        string[] reszek = sor.Split(';');
        string Enev = reszek[0];
        int hozott = int.Parse(reszek[1]);
        int szerzett = int.Parse(reszek[2]);
        int osszpontszam = hozott + szerzett;

        writer.WriteLine($"{Enev};{osszpontszam}");
    }
}

Console.WriteLine("Az osszpontok.csv állomány elkészült.");
#endregion