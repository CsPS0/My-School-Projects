#region 1.fel
using viragbolt;

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. feladat");
Console.ResetColor();

List<Virag> viragok = new();
string[] file = File.ReadAllLines("viragok.txt");

foreach (var line in file)
{
    string[] datas = line.Split(';');
    viragok.Add(new Virag(datas[0], int.Parse(datas[1])));
}
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("A fájl beolvasása sikeres!");
Console.ResetColor();
#endregion

#region 2.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. feladat");
Console.ResetColor();
foreach (var virag in viragok)
{
    Console.WriteLine(virag.Kiiras1);
}
#endregion

#region 3.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. feladat");
Console.ResetColor();
Console.WriteLine($"Átlagosan {Virag.AtlagAr(viragok):F2} Ft-ba kerül egy szál virág.");
#endregion

#region 4.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. feladat");
Console.ResetColor();
var legdragabb = Virag.Legdragabb(viragok);

if (legdragabb != null)
{
    Console.WriteLine($"A legdrágább virág: {legdragabb.Name} ({legdragabb.Price} Ft)");
}
else
{
    Console.WriteLine("Nincs elérhető virág az üzletben.");
}
#endregion

#region 5.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. feladat");
Console.ResetColor();
Console.WriteLine($"{Virag.LegalabbEzerForintos(viragok)}");
#endregion

#region 6.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. feladat");
Console.ResetColor();
Console.Write("Add meg egy virág nevét: ");
string keresett = Console.ReadLine();
Console.WriteLine(Virag.KeresVirag(viragok, keresett));
#endregion