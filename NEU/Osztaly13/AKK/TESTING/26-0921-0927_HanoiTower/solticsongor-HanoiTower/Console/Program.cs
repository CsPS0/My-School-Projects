using hanoiLib;

Console.Write("Korongok száma (Enter = 3): ");
string? diskInput = Console.ReadLine();
int diskCount = int.TryParse(diskInput, out int n) ? n : 3;

HanoiGame game = new(diskCount);

while (!game.IsSolved())
{
    PrintBoard(game);

    Console.Write("Honnan hova (pl. 0 2): ");
    string? input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        continue;
    }

    string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (parts.Length != 2 || !int.TryParse(parts[0], out int from) || !int.TryParse(parts[1], out int to))
    {
        Console.WriteLine("Adj meg két számot szóközzel elválasztva.");
        continue;
    }

    try
    {
        game.Move(from, to);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Hiba: {ex.Message}");
    }
}

PrintBoard(game);
Console.WriteLine("Kész, sikeres megoldás.");

static void PrintBoard(HanoiGame game)
{
    Console.WriteLine("\n--- Állás ---");
    for (int i = 0; i < game.Pegs.Count; i++)
    {
        Console.WriteLine($"Rúd {i}: {string.Join(", ", game.Pegs[i].Reverse())}");
    }
    Console.WriteLine();
}