#region File
string kondi = "kondi.txt";

string[] sorok = File.ReadAllLines(kondi);

int sportolokSzama = sorok.Length;
double osszSuly = 0;
string legkonnyebbNev = "";
double legkonnyebbSuly = double.MaxValue;
double legkonnyebbMagassag = 0;
#endregion

// 1.Feladat|Sportolók számának megjelenítése
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("1. feladat: ");
Console.ResetColor();

Console.ForegroundColor= ConsoleColor.Yellow;
Console.Write(sportolokSzama);
Console.ResetColor();
Console.WriteLine(" sportoló adatait rögzítettük.");

Thread.Sleep(500);

// 2.Feladat|Tömegek összegyűjtése és legkönnyebb sportoló keresése
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("2. feladat: ");
Console.ResetColor();

foreach (string sor in sorok)
{
    var adatok = sor.Split(';');
    string nev = adatok[0];
    double suly = double.Parse(adatok[1]);
    double magassag = double.Parse(adatok[2]);

    osszSuly += suly;

    if (suly < legkonnyebbSuly)
    {
        legkonnyebbSuly = suly;
        legkonnyebbNev = nev;
        legkonnyebbMagassag = magassag;
    }
}

double atlagTomeg = Math.Round(osszSuly / sportolokSzama, 1);
Console.Write("Az átlagos tömeg: ");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write(atlagTomeg);
Console.ResetColor();
Console.WriteLine(" kg.");

Thread.Sleep(500);

// 3.Feladat|Magasság bekérése és alacsonyabbak megszámolása
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("3. feladat: ");
Console.ResetColor();

Console.Write("Kérek egy magasság adatot! ");
Console.ForegroundColor = ConsoleColor.Yellow;
double bekertMagassag = double.Parse(Console.ReadLine()!);
Console.ResetColor();
int alacsonyabbakSzama = 0;

foreach (string sor in sorok)
{
    double magassag = double.Parse(sor.Split(';')[2]);
    if (magassag < bekertMagassag)
    {
        alacsonyabbakSzama++;
    }
}

Console.Write($"\t   {bekertMagassag} cm-nél ");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write(alacsonyabbakSzama);
Console.ResetColor();
Console.WriteLine(" sportoló alacsonyabb.");

Thread.Sleep(500);

// 4.Feladat|Legkönnyebb ember adatainak kiírása
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("4. feladat: ");
Console.ResetColor();

Console.WriteLine("A legkönnyebb ember:");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"\t   Neve: {legkonnyebbNev}");
Console.WriteLine($"\t   Tömege: {legkonnyebbSuly} kg");
Console.WriteLine($"\t   Magassága: {legkonnyebbMagassag} cm");
Console.ResetColor();

Thread.Sleep(500);

// 5.Feladat|BMI értékek számítása
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("5. feladat: ");
Console.ResetColor();

string[] bmiSorok = new string[sportolokSzama];
for (int i = 0; i < sorok.Length; i++)
{
    var adatok = sorok[i].Split(';');
    string nev = adatok[0];
    double suly = double.Parse(adatok[1]);
    double magassag = double.Parse(adatok[2]);

    double bmi = suly / Math.Pow(magassag / 100, 2);
    bmiSorok[i] = $"{nev} {bmi:F2}";
}

// BMI fájlba írása
// itt találod (remélem): \szorgalmi_sportolok\bin\Debug\net8.0
File.WriteAllLines("bmi.txt", bmiSorok);

Console.Write("BMI adatok sikeresen kiírva a");
Console.ForegroundColor= ConsoleColor.Yellow;
Console.Write(" bmi.txt ");
Console.ResetColor();
Console.Write("fájlba. | A fájlt itt találod: '\\szorgalmi_sportolok\\bin\\Debug\\net8.0'.");

Thread.Sleep(500);
Console.WriteLine("");