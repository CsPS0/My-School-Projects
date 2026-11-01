using novenygyujtesLib;

Console.Write("Adja meg a terület méretét (N): ");
Console.ForegroundColor = ConsoleColor.Yellow;
int n = int.Parse(Console.ReadLine() ?? "10"); //warning message bypass
Console.ResetColor();

NovenyFactory factory = new NovenyFactory();

List<INoveny> novenyLista = new List<INoveny>();
INoveny[,] terulet = new INoveny[n, n];

for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        terulet[i, j] = factory.Create();
        novenyLista.Add(terulet[i, j]);
    }
}

Console.WriteLine();

// 6. feladat
Console.WriteLine($"A terület növényei (kód alapján):");

for (int i = 0; i < n; i++)
{
    Console.Write("\t");
    for (int j = 0; j < n; j++)
    {
        Console.Write(terulet[i, j].Megjelenites);
    }
    Console.WriteLine();
}

Console.WriteLine();

// 7. feladat
int osszErtek = novenyLista.Sum(n => n.Ertek);
Console.WriteLine($"A területen található növények összértéke: {osszErtek}");

Console.WriteLine();

// 8. feladat
var nevCsoportok = novenyLista
    .GroupBy(n => n.Nev)
    .Select(g => $"{g.Key}: {g.Count()} db")
    .ToList();

Console.WriteLine("A területen található növények név szerint csoportosítva:");
foreach (var sor in nevCsoportok)
{
    Console.WriteLine($"\t{sor}");
}

Console.WriteLine();

// 9. feladat
var tipusCsoportok = novenyLista
    .GroupBy(n => n.Tipus)
    .Select(g => $"{g.Key}: {g.Count()} db")
    .ToList();

Console.WriteLine("A területen található növények típus szerint csoportosítva:");
foreach (var sor in tipusCsoportok)
{
    Console.WriteLine($"\t{sor}");
}