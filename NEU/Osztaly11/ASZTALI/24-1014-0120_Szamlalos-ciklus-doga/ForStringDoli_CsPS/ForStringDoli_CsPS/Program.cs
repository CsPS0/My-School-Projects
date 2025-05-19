#region 1.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat:");
Console.ResetColor();

Console.Write("Kérem az intervallum alsó határát: ");
int alsoHatar = int.Parse(Console.ReadLine()!);
Console.Write("Kérem az intervallum felső határát: ");
int felsoHatar = int.Parse(Console.ReadLine()!);

Console.Write("Az intervallumba tartozó számok csökkenő sorrendben: ");
for (int i = felsoHatar; i >= alsoHatar; i--)
{
    Console.Write(i + " ");
}
Console.WriteLine();

Console.Write("Az intervallumba tartozó öttel osztható számok növekvő sorrendben: ");
for (int i = alsoHatar; i <= felsoHatar; i++)
{
    if (i % 5 == 0)
    {
        Console.WriteLine(i);
    }
}

Random random = new Random();
int veletlenDb = random.Next(5, 11);
Console.Write($"{veletlenDb} darab véletlen valós érték az intervallumból: ");
for (int i = 0; i < veletlenDb; i++)
{
    double veletlenSzam = alsoHatar + (felsoHatar - alsoHatar) * random.NextDouble();
    Console.WriteLine(veletlenSzam);
}
#endregion

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{
    
}
Console.ResetColor();

#region 2.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat:");
Console.ResetColor();

Console.Write("Kérem adja meg a teljes nevét: ");
string teljesNev = Console.ReadLine()!;
Console.Write("Kérem adja meg az osztályát: ");
string osztaly = Console.ReadLine()!;

string[] nevek = teljesNev.Split(' ');
string vezetekNev = nevek[0];
string keresztNev = nevek[nevek.Length - 1];

string azonosito = osztaly.ToUpper()[0].ToString();
azonosito += vezetekNev.Substring(0, 2);

for (int i = 0; i < keresztNev.Length; i++)
{
    if (i % 2 == 0)
    {
        azonosito += keresztNev[i];
    }
}

char jel = random.Next(0, 2) == 0 ? '!' : '-';
azonosito += jel;
azonosito += random.Next(100, 1000);

Console.WriteLine($"Az elkészült felhasználónév: {azonosito}");
#endregion

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Nyomj entert a továbblépéshez!");
while (Console.ReadKey().Key != ConsoleKey.Enter)
{

}
Console.ResetColor();

#region 3.Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat:");
Console.ResetColor();

string jelszo;
bool megfeleloJelszo = false;

do
{
    Console.WriteLine("Kérem adja meg a jelszót!");
    jelszo = Console.ReadLine()!;

    if (jelszo.Length < 8)
    {
        Console.WriteLine("A jelszó túl rövid. Legalább 8 karakter hosszúnak kell lennie.");
    }
    else if (char.IsDigit(jelszo[0]) || char.IsDigit(jelszo[jelszo.Length - 1]))
    {
        Console.WriteLine("A jelszó első vagy utolsó karaktere nem lehet számjegy.");
    }
    else if (!jelszo.Any(char.IsDigit))
    {
        Console.WriteLine("A jelszó nem tartalmaz számjegyet.");
    }
    else
    {
        Console.WriteLine("A jelszó a szabályoknak megfelel.");
        megfeleloJelszo = true;
    }

} while (!megfeleloJelszo);
#endregion