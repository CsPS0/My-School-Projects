using Torpek;

List<Torpe> torpek = new List<Torpe>();

#region 1.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. feladat");
Console.ResetColor();

StreamReader sr = new StreamReader("torpek.txt");
sr.ReadLine(); // Bence megoldás
while (!sr.EndOfStream)
{
    string[] adatok = sr.ReadLine().Split(';');
    torpek.Add(new Torpe(adatok[0], adatok[1], adatok[2][0], int.Parse(adatok[3]), int.Parse(adatok[4])));
}
sr.Close();
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("A fájlok beolvasása sikeres!");
Console.ResetColor();
#endregion

#region 2.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. feladat");
Console.ResetColor();

Console.WriteLine($"Az állományban található törpék száma: {torpek.Count} db");
#endregion

#region 3.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. feladat");
Console.ResetColor();

double atlagSuly = torpek.Average(t => t.Suly);
Console.WriteLine($"A törpék átlagos súlya: {atlagSuly:F1} kg");
#endregion

#region 4.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. feladat");
Console.ResetColor();

Torpe legmagasabb = torpek[0];
foreach (var torpe in torpek)
{
    if (torpe.Magassag > legmagasabb.Magassag)
    {
        legmagasabb = torpe;
    }
}
Console.WriteLine("A legmagasabb törpe adatai:");
Console.WriteLine($"Név: {legmagasabb.Nev}");
Console.WriteLine($"Klán: {legmagasabb.Klan}");
Console.WriteLine($"Nem: {(legmagasabb.Nem == 'F' ? "férfi" : "nő")}");
Console.WriteLine($"Súly: {legmagasabb.Suly} kg");
Console.WriteLine($"Magasság: {legmagasabb.Magassag} cm");
#endregion

#region 5.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. feladat");
Console.ResetColor();

Console.Write("A klán neve: ");
string keresettKlan = Console.ReadLine();
bool vanE = false;
foreach (var torpe in torpek)
{
    if (torpe.Klan.Equals(keresettKlan, StringComparison.OrdinalIgnoreCase))
    {
        vanE = true;
        break;
    }
}
Console.WriteLine(vanE ? $"Van {keresettKlan} nevű klánba tartozó törpe." : $"Nincs {keresettKlan} nevű klánba tartozó törpe.");
#endregion

#region 6.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. feladat");
Console.ResetColor();

double legkisebbTTI = torpek[0].TTI();
foreach (var torpe in torpek)
{
    if (torpe.TTI() < legkisebbTTI)
    {
        legkisebbTTI = torpe.TTI();
    }
}
Console.WriteLine($"A legkisebb TTI érték: {legkisebbTTI:F2}");
#endregion

#region 7.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. feladat");
Console.ResetColor();

Console.Write("Nőnemű törpék: ");
bool elso = true;
foreach (var torpe in torpek)
{
    if (torpe.Nem == 'N')
    {
        if (!elso) Console.Write(", ");
        Console.Write($"{torpe.Nev} ({torpe.Klan})");
        elso = false;
    }
}
Console.WriteLine();
#endregion

#region 8.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. feladat");
Console.ResetColor();

torpek.Sort((a, b) =>
{
    int klanCompare = a.Klan.CompareTo(b.Klan);
    if (klanCompare != 0) return klanCompare;
    return a.Nev.CompareTo(b.Nev);
});
Console.WriteLine("Résztvevők:");
foreach (var torpe in torpek)
{
    Console.WriteLine($"{torpe.Nev} ({torpe.Klan})");
}
#endregion

#region 9.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. feladat");
Console.ResetColor();

HashSet<string> klanok = new HashSet<string>();
foreach (var torpe in torpek)
{
    klanok.Add(torpe.Klan);
}
Console.WriteLine($"Klánok: {string.Join(", ", klanok)}");
#endregion

#region 10.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("10. feladat");
Console.ResetColor();

torpek.Sort((a, b) => b.TTI().CompareTo(a.TTI()));
Console.WriteLine("A 3 legnagyobb TTI-vel rendelkező törpe:");
for (int i = 0; i < Math.Min(3, torpek.Count); i++)
{
    Console.WriteLine($"{torpek[i].Nev} ({torpek[i].Klan})");
}
#endregion