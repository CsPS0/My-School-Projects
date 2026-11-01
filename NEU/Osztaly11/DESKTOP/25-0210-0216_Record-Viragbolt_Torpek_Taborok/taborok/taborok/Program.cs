using taborok;

#region 1. feladat - Fájl beolvasása
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. feladat");
Console.ResetColor();

List<Tabor> taborok = new();
string[] file = File.ReadAllLines("taborok.txt");

foreach (string line in file)
{
    string[] datas = line.Split('\t');
    taborok.Add(new Tabor(int.Parse(datas[0]), int.Parse(datas[1]), int.Parse(datas[2]), int.Parse(datas[3]), datas[4], datas[5]));
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("A fájl beolvasása sikeres!");
Console.ResetColor();
#endregion

#region 2. feladat - Táborok száma, első és utolsó tábor témája
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. feladat");
Console.ResetColor();

Console.WriteLine($"Összesen {taborok.Count} tábor adatait tartalmazza a fájl.");
Console.WriteLine($"Az első tábor témája: {taborok.First().CampType}");
Console.WriteLine($"Az utolsó tábor témája: {taborok.Last().CampType}");
#endregion

#region 3. feladat - Zenei táborok keresése
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. feladat");
Console.ResetColor();

var zeneiTaborok = taborok.Where(t => t.CampType == "zenei").ToList();

if (zeneiTaborok.Count > 0)
{
    foreach (var tabor in zeneiTaborok)
    {
        Console.WriteLine($"Zenei tábor kezdődik {tabor.StartingMonth}. hó {tabor.StartingDay}. napján.");
    }
}
else
{
    Console.WriteLine("Nem volt zenei tábor.");
}
#endregion

#region 4. feladat - Legnépszerűbb tábor(ok) keresése
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. feladat");
Console.ResetColor();

int maxJelentkezes = taborok.Max(t => t.StudentsABC.Length);
var legnepszerubbTaborok = taborok.Where(t => t.StudentsABC.Length == maxJelentkezes).ToList();

Console.WriteLine("Legnépszerűbbek:");
foreach (var tabor in legnepszerubbTaborok)
{
    Console.WriteLine($"{tabor.StartingMonth} {tabor.StartingDay} {tabor.CampType}");
}
#endregion

#region 5. feladat - Nyári szünet napjának meghatározása függvénnyel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. feladat");
Console.ResetColor();

static int sorszam(int month, int day)
{
    int[] daysBefore = { 0, 0, 30, 61 };
    return daysBefore[month - 6] + (day - 16 + 1);
}
#endregion

#region 6. feladat - Táborok száma adott napon
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. feladat");
Console.ResetColor();

Console.Write("hó: ");
int keresettHo = int.Parse(Console.ReadLine());
Console.Write("nap: ");
int keresettNap = int.Parse(Console.ReadLine());

int aktivTaborok = taborok.Count(t =>
    sorszam(t.StartingMonth, t.StartingDay) <= sorszam(keresettHo, keresettNap) &&
    sorszam(t.EndingMonth, t.EndingDay) >= sorszam(keresettHo, keresettNap));

Console.WriteLine($"Ekkor éppen {aktivTaborok} tábor tart.");
#endregion

#region 7. feladat - Tanuló keresése és táborai
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. feladat");
Console.ResetColor();

Console.Write("Adja meg egy tanuló betűjelét: ");
char keresettTanulo = char.Parse(Console.ReadLine());

var tanuloTaborai = taborok
    .Where(t => t.StudentsABC.Contains(keresettTanulo)).OrderBy(t => sorszam(t.StartingMonth, t.StartingDay)).ToList();

if (tanuloTaborai.Count == 0)
{
    Console.WriteLine("A megadott tanuló nem jelentkezett táborba.");
}
else
{
    using StreamWriter sw = new("egytanulo.txt");
    bool lehetseges = true;

    for (int i = 0; i < tanuloTaborai.Count - 1; i++)
    {
        if (sorszam(tanuloTaborai[i].EndingMonth, tanuloTaborai[i].EndingDay) >= sorszam(tanuloTaborai[i + 1].StartingMonth, tanuloTaborai[i + 1].StartingDay))
        {
            lehetseges = false;
        }
    }

    foreach (var tabor in tanuloTaborai)
    {
        sw.WriteLine($"{tabor.StartingMonth}.{tabor.StartingDay}-{tabor.EndingMonth}.{tabor.EndingDay}. {tabor.CampType}");
    }

    Console.WriteLine(lehetseges ? "A tanuló minden táborba el tud menni." : "Nem mehet el mindegyik táborba.");
}
#endregion
