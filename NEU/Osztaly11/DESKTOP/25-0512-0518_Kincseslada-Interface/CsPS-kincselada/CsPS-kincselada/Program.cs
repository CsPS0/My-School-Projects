using kincsesladaLib;

KincsFactory gyar = new KincsFactory();

List<IKincs> kincsesLada = new List<IKincs>();
for (int i = 0; i < 10; i++)
{
    kincsesLada.Add(gyar.Create());
}

// 6. feladat
Console.WriteLine($"A kincsesláda tartalma:");
foreach (var i in kincsesLada)
{
    Console.WriteLine($"\t{i.Leiras}");
}

Console.WriteLine();

// 7. feladat
int osszErtek = kincsesLada.Sum(k => k.Ertek);
Console.WriteLine($"A kincsesláda értéke: {osszErtek}");

Console.WriteLine();

// 8. feladat
var nevCsoportok = kincsesLada
    .GroupBy(k => k.Nev)
    .Select(g => $"{g.Key}: {g.Count()} db")
    .ToList();

Console.WriteLine("A kincsesláda tartalma név szerint csoportosítva:");
foreach (var sor in nevCsoportok)
{
    Console.WriteLine($"\t{sor}");
}

Console.WriteLine();

// 9. feladat
var tipusCsoportok = kincsesLada
    .GroupBy(k => k.Tipus)
    .Select(g => $"{g.Key}: {g.Count()} db")
    .ToList();

Console.WriteLine("A kincsesláda tartalma típus szerint csoportosítva:");
foreach (var sor in tipusCsoportok)
{
    Console.WriteLine($"\t{sor}");
}