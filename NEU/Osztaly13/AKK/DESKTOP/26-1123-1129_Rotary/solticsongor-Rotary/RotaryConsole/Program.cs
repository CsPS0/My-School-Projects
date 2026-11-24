using System.Text;
using RotaryConsole;
using RotaryLib;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

RestApiHandler restApiHandler = new();
List<Versenyzo> versenyzok = restApiHandler.GetAll().ToList();

Console.WriteLine("3.a feladat:");
int kulfoldiek = versenyzok.Count(x => x.Orszagkod != "HU");
Console.WriteLine("\tKulfoldi versenyzok szama: " + kulfoldiek);

Console.WriteLine("3.b feladat:");
Console.Write("\tAdjon meg egy rajtszamot: ");
string id = Console.ReadLine() ?? "";
Versenyzo? versenyzo = restApiHandler.GetById(id);
if (versenyzo != null)
{
    Console.WriteLine("\t" + versenyzo);
}
else
{
    Console.WriteLine("\tNem talaltam ilyen rajtszamu versenyzot.");
}

Console.WriteLine("3.c feladat:");
Versenyzo? elsoNo = versenyzok
    .Where(x => x.Nem == "Nő")
    .OrderBy(x => x.Poz)
    .FirstOrDefault();
if (elsoNo != null)
{
    Console.WriteLine("\t" + elsoNo);
}
else
{
    Console.WriteLine("\tNem ert celba noi versenyzo.");
}

Console.WriteLine("3.d feladat:");
var gyoztesek = versenyzok
    .Where(x => x.Kpoz == 1)
    .OrderBy(x => x.Kategoria)
    .ThenBy(x => x.Poz);
foreach (var gyoztes in gyoztesek)
{
    Console.WriteLine("\t" + gyoztes);
}

Console.WriteLine("3.e feladat:");
var kategoriak = versenyzok
    .GroupBy(x => x.Kategoria)
    .OrderBy(x => x.Key);
foreach (var kategoria in kategoriak)
{
    Console.WriteLine($"\t{kategoria.Key}. kategoria: {kategoria.Count()} fo");
}

Console.ReadLine();
