using kincsesladaLib;
using System.Text;

KincsFactory factory = new KincsFactory();

List<IKincs> kincsesLada = new List<IKincs>();
for (int i = 0; i < 10; i++)
{
    kincsesLada.Add(factory.Create());
}

// 6. feladat
StringBuilder contentBuilder = new StringBuilder();
foreach (var kincs in kincsesLada)
{
    contentBuilder.AppendLine(kincs.ToString());
}

// 7. feladat
int osszErtek = kincsesLada.Sum(k => k.Ertek);

// 8. feladat
var nevCsoportok = kincsesLada
    .GroupBy(k => k.Nev)
    .Select(g => $"{g.Key}: {g.Count()} db")
    .ToList();
StringBuilder nevCsoportBuilder = new StringBuilder();
nevCsoportBuilder.AppendLine(string.Join("\n\t", nevCsoportok));

// 9. feladat
var tipusCsoportok = kincsesLada
    .GroupBy(k => k.Tipus)
    .Select(g => $"{g.Key}: {g.Count()} db")
    .ToList();
StringBuilder tipusCsoportBuilder = new StringBuilder();
tipusCsoportBuilder.AppendLine(string.Join("\n\t", tipusCsoportok));

// 6. feladat
Console.WriteLine($"A kincsesláda tartalma:" +
    $"\n\t" +
    $"{contentBuilder}");
Console.WriteLine($"A kincsesláda értéke: {osszErtek}");
Console.WriteLine($"A kincsesláda tartalma név szerint csoportosítva:" +
    $"\n\t" +
    $"{nevCsoportBuilder}");
Console.WriteLine($"A kincsesláda tartalma típus szerint csoportosítva:" +
    $"\n\t" +
    $"{tipusCsoportBuilder}");