using brainrotLib;

Console.Write("Adja meg a terület méretét (N): ");
Console.ForegroundColor = ConsoleColor.Yellow;
int n = int.Parse(Console.ReadLine() ?? "10");
Console.ResetColor();

BrainrotFactory factory = new BrainrotFactory();

List<IBrainrot> novenyLista = new List<IBrainrot>();
IBrainrot[,] terulet = new IBrainrot[n, n];

for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        terulet[i, j] = factory.Create();
        novenyLista.Add(terulet[i, j]);
    }
}

Console.WriteLine();

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