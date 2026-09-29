using starTrekLib;

// 3. feladat
Tablak tablak = new Tablak();

foreach (string sor in File.ReadAllLines("fajok.csv").Skip(1))
{
    string[] m = sor.Split(';');
    tablak.Fajok.Rows.Add(int.Parse(m[0]), m[1].Trim());
}

foreach (string sor in File.ReadAllLines("hajo_szerepek.csv").Skip(1))
{
    string[] m = sor.Split(';');
    tablak.HajoSzerepek.Rows.Add(int.Parse(m[0]), m[1].Trim());
}

foreach (string sor in File.ReadAllLines("hajo_osztalyok.csv").Skip(1))
{
    string[] m = sor.Split(';');
    tablak.HajoOsztalyok.Rows.Add(int.Parse(m[0]), m[1].Trim(), int.Parse(m[2]));
}

foreach (string sor in File.ReadAllLines("urhajok.csv").Skip(1))
{
    string[] m = sor.Split(';');
    tablak.Urhajok.Rows.Add(int.Parse(m[0]), m[1].Trim(), m[2].Trim(), int.Parse(m[3]), int.Parse(m[4]));
}

// 4. feladat
Console.WriteLine($"4. feladat: {tablak.EnterpriseDarab()} darab űrhajó nevében szerepel az Enterprise név.");

// 5. feladat
Console.Write("5. feladat: A szerep neve: ");
string szerepNev = Console.ReadLine() ?? "";
int osztalyDb = tablak.OsztalyokSzerepSzerint(szerepNev);
if (osztalyDb == -1)
{
    Console.WriteLine("\tIlyen szerep nincs az adatbázisban.");
}
else
{
    Console.WriteLine($"\t{osztalyDb} hajóosztály rendeltetése a megadott szerep.");
}

// 6. feladat
Console.WriteLine("6. feladat:");
foreach (var (nev, darab) in tablak.LegtobbHajosOsztalyok(3))
{
    Console.WriteLine($"\t{nev}: {darab} űrhajó");
}