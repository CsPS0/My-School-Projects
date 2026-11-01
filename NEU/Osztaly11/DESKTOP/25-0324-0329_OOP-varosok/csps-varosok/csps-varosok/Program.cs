using VarosokLib;

Varosok varosok = new Varosok(File.ReadAllLines("varosok.txt").Skip(1));

// 2.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. feladat:");
Console.ResetColor();

Console.WriteLine($"Magyarország {varosok.HanyVaros} városa szerepel a listában");

// 3.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. feladat:");
Console.ResetColor();

var elsoKisVaros = varosok.Keves2K;
if (elsoKisVaros != null)
{
    Console.WriteLine("Az első 2000 főnél kevesebb lakosú város:");
    Console.WriteLine($"Név: {elsoKisVaros.Nev}");
    Console.WriteLine($"Vármegye: {elsoKisVaros.Varmegye}");
    Console.WriteLine($"Lakosság: {elsoKisVaros.Nepesseg} fő");
    Console.WriteLine($"Várossá nyilvánítás éve: {(elsoKisVaros.Alapitas?.ToString() ?? "1885 előtt")}");
}

// 4.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. feladat:");
Console.ResetColor();

Console.WriteLine($"A városok átlagos területe {varosok.TeruletAVG} km2");

// 5.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. feladat:");
Console.ResetColor();

Console.WriteLine($"A rendszerváltás után {varosok.Rendszervaltas} település kapott városi rangot");

// 6.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. feladat:");
Console.ResetColor();

var legritkabb = varosok.MinNepsuruseg;
Console.WriteLine($"A legkisebb népsűrűségű város: {legritkabb.Nev}");
Console.WriteLine($"Népsűrűség: {legritkabb.Nepsuruseg:F2} fő/km2");
Console.WriteLine($"Vármegye: {legritkabb.Varmegye}");
Console.WriteLine($"Várossá nyilvánítás éve: {(legritkabb.Alapitas?.ToString() ?? "1885 előtt")}");

// 7.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. feladat:");
Console.ResetColor();

Console.Write("Kérek egy évszámot: ");
int evszam = int.Parse(Console.ReadLine());
if (varosok.VoltVarossa(evszam))
{
    Console.WriteLine("Volt ebben az évben várossá nyilvánított település");
}
else
{
    Console.WriteLine("Nem volt ebben az évben várossá nyilvánított település");
}

// 8.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. feladat:");
Console.ResetColor();

Console.WriteLine("A 3 legsűrűbben lakott Pest vármegyei város:");
int i = 1;
foreach (var v in varosok.LegsurubbPest)
{
    Console.WriteLine($"{i}. {v.Nev}");
    i++;
}

// 9.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. feladat:");
Console.ResetColor();

Console.WriteLine("Nógrád vármegyei városok:");
Console.WriteLine(varosok.NogradVarosok);

// 10.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("10. feladat:");
Console.ResetColor();

foreach (var kvp in varosok.Megyenkent)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value} város");
}