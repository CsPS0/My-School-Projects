#region 1.fel
using Repak;

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();

List<Repa> lista_repa = new List<Repa>();
List<Nyul> lista_nyul = new List<Nyul>();

Random rnd = new Random();

// 10 véletlen répa létrehozása
for (int i = 0; i < 10; i++)
{
    if (rnd.Next(0, 2) == 0)
        lista_repa.Add(new Repa());
    else
        lista_repa.Add(new Repa("sárgarépa"));
}

// 5 véletlen nyuszi létrehozása
for (int i = 0; i < 5; i++)
{
    lista_nyul.Add(new Nyul());
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("A fájlok létrehozása sikeres");
Console.ResetColor();
#endregion

#region 2.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

Console.WriteLine("Répák:");
foreach (var repa in lista_repa)
{
    repa.Informacio();
}

Console.WriteLine("Nyulak:");
foreach (var nyul in lista_nyul)
{
    nyul.Informacio();
}
#endregion

#region 3.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat");
Console.ResetColor();

Console.WriteLine("Répák öregedése:");
foreach (var repa in lista_repa)
{
    bool megmaradt = repa.Tick(5, 0.05);
    Console.WriteLine($"{repa.Fajta} - Kor: {repa.Kor}, Minőség: {repa.Minoseg}, Megmaradt? {megmaradt}");
}

Console.WriteLine("Nyulak etetése:");
foreach (var nyul in lista_nyul)
{
    bool sikeres = nyul.Eszik(30);
    Console.WriteLine($"Nyúl - Kor: {nyul.Kor}, Jóllakottság: {nyul.Jollakottsag}, Sikeres etetés? {sikeres}");
}
#endregion