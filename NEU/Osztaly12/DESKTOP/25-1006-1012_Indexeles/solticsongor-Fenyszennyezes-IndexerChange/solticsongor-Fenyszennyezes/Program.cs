#region 1.fel
//1. Feladat
using solticsongor_Fenyszennyezes;

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();

FenyTerkep fenyTerkep = new FenyTerkep("terkep.txt");

Console.Write("A ");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.Write("terkep.txt ");
Console.ResetColor();
Console.WriteLine("fájl a sikeresen beolvasva.");
#endregion

#region 2.fel
//2. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n2. Feladat");
Console.ResetColor();

int input_row;
while (true)
{
    Console.Write($"A mérés sorának azonosítója (1-{fenyTerkep.N}): ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    string input = Console.ReadLine() ?? "";
    Console.ResetColor();
    if (int.TryParse(input, out input_row) && input_row >= 1 && input_row <= fenyTerkep.N)
    {
        break;
    }
    Console.WriteLine("Hibás bemenet. Kérem, adjon meg egy érvényes sorszámot.");
}

int input_col;
while (true)
{
    Console.Write($"A mérés oszlopának azonosítója (1-{fenyTerkep.M}): ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    string input = Console.ReadLine() ?? "";
    Console.ResetColor();
    if (int.TryParse(input, out input_col) && input_col >= 1 && input_col <= fenyTerkep.M)
    {
        break;
    }
    Console.WriteLine("Hibás bemenet. Kérem, adjon meg egy érvényes oszlopszámot.");
}

int kiszamolas = fenyTerkep[input_row - 1, input_col - 1];
Console.WriteLine($"Az adott helyen {kiszamolas} a mért fényesség értéke.");
#endregion

#region 3.fel
//3. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n3. Feladat");
Console.ResetColor();

int db = 0;
for (int i = 0; i < fenyTerkep.N; i++)
{
    for (int j = 0; j < fenyTerkep.M; j++)
    {
        if (fenyTerkep[i, j] == 0)
        {
            db++;
        }
    }
}
double percent = db * 100.0 / (fenyTerkep.N * fenyTerkep.M);
Console.WriteLine($"A terület {percent:F1} %-a teljesen sötét.");
#endregion

#region 4.fel
//4. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n4. Feladat");
Console.ResetColor();

int max = int.MinValue;
List<(int, int)> kordikak = new List<(int, int)>();

for (int i = 0; i < fenyTerkep.N; i++)
{
    for (int j = 0; j < fenyTerkep.M; j++)
    {
        if (fenyTerkep[i, j] > max)
        {
            max = fenyTerkep[i, j];
            kordikak.Clear();
            kordikak.Add((i + 1, j + 1));
        }
        else if (fenyTerkep[i, j] == max)
        {
            kordikak.Add((i + 1, j + 1));
        }
    }
}
Console.WriteLine($"A legnagyobb fényességérték: {max}");
Console.WriteLine("A legfényesebb helyek koordinátái:");
foreach (var kord in kordikak)
{
    Console.Write($"({kord.Item1}, {kord.Item2}) ");
}
#endregion

#region 5.fel
//5. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n\n5. Feladat");
Console.ResetColor();

int fenyesPontok = 0;
int[,] dirs = { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
for (int i = 0; i < fenyTerkep.N; i++)
{
    for (int j = 0; j < fenyTerkep.M; j++)
    {
        bool fenyes = true;
        for (int d = 0; d < 4; d++)
        {
            int ni = i + dirs[d, 0], nj = j + dirs[d, 1];
            if (ni >= 0 && ni < fenyTerkep.N && nj >= 0 && nj < fenyTerkep.M)
                if (fenyTerkep[i, j] <= fenyTerkep[ni, nj])
                    fenyes = false;
        }
        if (fenyes)
            fenyesPontok++;
    }
}
Console.WriteLine($"A fenyes meresi pontok szama: {fenyesPontok} db.");
#endregion

#region 6.fel
//6. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n6. Feladat");
Console.ResetColor();

List<(int, int)> fenyesList = new List<(int, int)>();
for (int i = 0; i < fenyTerkep.N; i++)
{
    for (int j = 0; j < fenyTerkep.M; j++)
    {
        bool fenyes = true;
        for (int d = 0; d < 4; d++)
        {
            int ni = i + dirs[d, 0], nj = j + dirs[d, 1];
            if (ni >= 0 && ni < fenyTerkep.N && nj >= 0 && nj < fenyTerkep.M)
                if (fenyTerkep[i, j] <= fenyTerkep[ni, nj])
                    fenyes = false;
        }
        if (fenyes)
            fenyesList.Add((i + 1, j + 1));
    }
}
int minx = fenyTerkep.N, miny = fenyTerkep.M, maxx = 1, maxy = 1;
foreach (var p in fenyesList)
{
    if (p.Item1 < minx) minx = p.Item1;
    if (p.Item2 < miny) miny = p.Item2;
    if (p.Item1 > maxx) maxx = p.Item1;
    if (p.Item2 > maxy) maxy = p.Item2;
}
Console.WriteLine($"A legkisebb teglalap, amely az osszes fenyes pontot tartalmazza:");
Console.WriteLine($"bal-felso: ({minx}, {miny}), jobb-also: ({maxx}, {maxy})");
#endregion

#region 7.fel
//7. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n7. Feladat");
Console.ResetColor();

int oszlopIdx;
while (true)
{
    Console.Write($"A vizsgált oszlop sorszáma (1-{fenyTerkep.M}): ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    string input = Console.ReadLine() ?? "";
    Console.ResetColor();
    if (int.TryParse(input, out oszlopIdx) && oszlopIdx >= 1 && oszlopIdx <= fenyTerkep.M)
    {
        break;
    }
    Console.WriteLine("Hibás bemenet. Kérem, adjon meg egy érvényes oszlopszámot.");
}
string[] diagram = new string[fenyTerkep.N];
for (int i = 0; i < fenyTerkep.N; i++)
{
    int csillagDb = (int)Math.Round(fenyTerkep[i, oszlopIdx - 1] / 10.0, MidpointRounding.AwayFromZero);
    diagram[i] = new string('*', csillagDb);
}
File.WriteAllLines("diagram.txt", diagram);
Console.Write("A ");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.Write("diagram.txt ");
Console.ResetColor();
Console.WriteLine("fájl a projekt mappájában lett létrehozva.");
#endregion