#region 1.fel
using hataratkelok;

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();

List<Hataratkelo> lista = new List<Hataratkelo>();

StreamReader r = new StreamReader("hataratkelok.csv");
int sorok = 1;
while (!r.EndOfStream)
{
    string[] tomb = r.ReadLine().Split(";");
    string nev = tomb[0];
    string tipus = tomb[1];
    string megye = tomb[2];
    string szomszedtelepules = tomb[3];
    string orszag = tomb[4];
    string atkelotipus = tomb[5];

    lista.Add(new Hataratkelo(nev, tipus, megye, szomszedtelepules, orszag, atkelotipus));
    sorok++;
}
Console.WriteLine("A fájlok beolvasása sikeres!");
#endregion

#region 2.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

var vasutiAtkelok = lista.Where(x => x.AtkeloTipus == "vasúti").Count();
Console.WriteLine($"A vasúti átkelők száma: {vasutiAtkelok}");
#endregion

#region 3.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat");
Console.ResetColor();

var megyeiJoguVarosok = lista.Where(x => x.Tipus == "város" || x.Tipus == "megyei jogú város");
foreach (var v in megyeiJoguVarosok)
{
    Console.WriteLine($"{v.Nev} - {v.Szomszedtelepules}: {v.AtkeloTipus}");
}
#endregion

#region 4.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. Feladat");
Console.ResetColor();

var ausztriaAtkelok = lista.Where(x => (x.Tipus == "város" || x.Tipus == "megyei jogú város") && x.Orszag == "Ausztria");
Console.WriteLine($"A városokból vagy megyei jogú városokból Ausztriába vezető átkelők száma: {ausztriaAtkelok.Count()}");
#endregion

#region 5.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. Feladat");
Console.ResetColor();

var abc_hu_sort = lista.Where(y => y.Orszag == "Ausztria").OrderBy(x => x.Nev).FirstOrDefault();
Console.WriteLine($"Ábécésorendben az első olyan település, amelyikből vezet Ausztiába: {abc_hu_sort?.Nev ?? "Nincs ilyen település"}");
#endregion

#region 6.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. Feladat");
Console.ResetColor();

var orszagok = lista.Where(x => x.Orszag != "Magyarország").Select(x => x.Orszag).Distinct().OrderBy(x => x);
Console.WriteLine(string.Join(", ", orszagok));
#endregion

#region 7.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. Feladat");
Console.ResetColor();

var kozutiEsVasutAtkelok = lista.GroupBy(x => x.Nev).Where(g => g.Any(x => x.AtkeloTipus == "közúti") && g.Any(x => x.AtkeloTipus == "vasúti")).Select(g => g.Key).OrderBy(x => x);

Console.WriteLine(string.Join(", ", kozutiEsVasutAtkelok));
#endregion


#region 8.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. Feladat");
Console.ResetColor();

var orszagokCsoportositva = lista.GroupBy(x => x.Orszag).Select(g => new { Orszag = g.Key, Darab = g.Count() }).OrderBy(x => x.Orszag);

foreach (var item in orszagokCsoportositva)
{
    Console.WriteLine($"{item.Orszag} - {item.Darab}");
}
#endregion

#region 9.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. Feladat");
Console.ResetColor();

var zalaAtkelok = lista.Where(x => x.Megye == "Zala").OrderBy(x => x.Nev).ToList();
var vasAtkelok = lista.Where(x => x.Megye == "Vas").OrderBy(x => x.Nev).ToList();

Console.WriteLine("Vas:");
foreach (var v in vasAtkelok)
{
    Console.WriteLine($"{v.Nev} - {v.Megye} ({v.Orszag})");
}

Console.WriteLine("Zala:");
foreach (var z in zalaAtkelok)
{
    Console.WriteLine($"{z.Nev} - {z.Megye} ({z.Orszag})");
}
#endregion