#region 2.fel
using barlangok;

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

List<Barlang> lista = new List<Barlang>();

StreamReader r = new StreamReader("feljegyzesek.csv");
int kedv = int.Parse(r.ReadLine());
int sorok = 0;
while (!r.EndOfStream)
{
    string[] tomb = r.ReadLine().Split(";");
    string barlangnev = tomb[0];
    float hossz = float.Parse(tomb[1]);
    float melyseg = float.Parse(tomb[2]);
    float magassag = float.Parse(tomb[3]);
    string telepulesnev = tomb[4];
    string voltak = tomb[5];

    lista.Add(new Barlang(barlangnev, hossz, melyseg, magassag, telepulesnev, voltak));
    sorok++;
}
Console.WriteLine("A fájlok beolvasása sikeres!");
#endregion

#region 3.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat");
Console.ResetColor();

Console.WriteLine($"Okoska kedvenc barlangjának adatai:" +
    $"\n\t" +
    $"Név: {lista[kedv].BarlangNev}" +
    $"\n\t" +
    $"Hossz: {lista[kedv].Hossz} m" +
    $"\n\t" +
    $"Mélység: {lista[kedv].Melyseg} m" +
    $"\n\t" +
    $"Magasság: {lista[kedv].Magassag} m" +
    $"\n\t" +
    $"Település: {lista[kedv].TelepulesNev}");
#endregion

#region 4.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. Feladat");
Console.ResetColor();

float telj_hossz = lista.Where(x => x.Voltak == "i").Sum(x => x.Hossz);
Console.WriteLine($"A látogatott barlangok teljes hossza: {Math.Round(telj_hossz / 1000, 2)} km");
#endregion

#region 5.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. Feladat");
Console.ResetColor();

Barlang legmelyebbBarlang = lista.OrderByDescending(x => x.Melyseg).First();
Console.WriteLine($"A legmélyebb barlang adatai:" +
    $"\n\t" +
    $"Név: {legmelyebbBarlang.BarlangNev}" +
    $"\n\t" +
    $"Mélység: {legmelyebbBarlang.Melyseg} m" +
    $"\n\t" +
    $"Település: {legmelyebbBarlang.TelepulesNev}");
#endregion

#region 6.fel || 7.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6/7. Feladat");
Console.ResetColor();

Console.WriteLine("Régies barlangnevek:");
var regiesNevek = lista.Where(x => RegiesNev(x.BarlangNev)).Select(x => x.BarlangNev).ToList();

if (regiesNevek.Any())
{
    Console.WriteLine($"Régies nevű barlangok száma: {regiesNevek.Count} db");
    foreach (var nev in regiesNevek)
    {
        Console.WriteLine(nev);
    }
}
else
{
    Console.WriteLine("Nincs régies barlangnév!");
}

bool RegiesNev(string nev)
{
    return nev.Contains("lyuk") || nev.Contains("zsomboly") || nev.Contains("lik");
}
#endregion

#region 8.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. Feladat");
Console.ResetColor();

string kedvTelepules = lista[kedv].TelepulesNev;
float atlagHossz = lista.Where(x => x.TelepulesNev == kedvTelepules).Average(x => x.Hossz);
Console.WriteLine($"{kedvTelepules} településen található barlangok átlagos hossza: {atlagHossz:F2} m");
#endregion

#region 9.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. Feladat");
Console.ResetColor();

using (StreamWriter writer = new StreamWriter("javitott.csv"))
{
    writer.WriteLine(kedv);
    foreach (var barlang in lista)
    {
        writer.Write(barlang.BarlangNev + ";" + barlang.Hossz + ";" + barlang.Melyseg + ";" + barlang.Magassag + ";" + barlang.TelepulesNev);
        if (barlang.Voltak == "i")
        {
            writer.Write(";már jártunk itt");
        }
        writer.WriteLine();
    }
}
Console.WriteLine("A fájl létrehozása sikeres!");
#endregion

#region 10.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("10. Feladat");
Console.ResetColor();

var telepulesek = lista.GroupBy(x => x.TelepulesNev)
    .Select(g => new { Telepules = g.Key, Barlangok = g.ToList() })
    .OrderByDescending(x => x.Telepules).ToList();

foreach (var telepules in telepulesek)
{
    Console.WriteLine($"{telepules.Telepules}: {telepules.Barlangok.Count} darab");
    for (int i = 0; i < telepules.Barlangok.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {telepules.Barlangok[i].BarlangNev}");
    }
}
#endregion

#region 11.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("11. Feladat");
Console.ResetColor();

Console.WriteLine("Az első 5 leghosszabb hegyi barlang, ahol már jártak a törpök:");
var leghosszabbBarlangok = lista.Where(x => x.Voltak == "i").OrderByDescending(x => x.Hossz).Take(5).ToList();
foreach (var barlang in leghosszabbBarlangok)
{
    Console.WriteLine($"{barlang.BarlangNev}, Hossz: {barlang.Hossz} m");
}
#endregion

#region 12.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("12. Feladat");
Console.ResetColor();

var megNemJartunk = lista.Where(x => x.Voltak == "n")
    .GroupBy(x => x.TelepulesNev)
    .OrderByDescending(g => g.Key)
    .ToList();

using (StreamWriter sw = new StreamWriter("megnemjartunk.csv"))
{
    foreach (var group in megNemJartunk)
    {
        foreach (var barlang in group.OrderBy(x => x.BarlangNev))
        {
            sw.WriteLine($"{group.Key};{barlang.BarlangNev}");
        }
    }
}
Console.WriteLine("A fájl létrehozása sikeres!");
#endregion