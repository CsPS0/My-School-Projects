using System.Text;
using VedettLibrary;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

AnimalApiHandler api = new AnimalApiHandler("http://localhost:5052/");

List<Animal> animals = await api.GetAnimals();

Console.WriteLine("a) feladat");
int max = 0;
foreach (Animal a in animals)
{
    int value = int.Parse(a.Value);
    if (value > max)
    {
        max = value;
    }
}
Console.WriteLine("A legnagyobb eszmei érték: " + max + " Ft");
foreach (Animal a in animals)
{
    if (int.Parse(a.Value) == max)
    {
        Console.WriteLine("\t" + a.Name + " (" + a.Category + ")");
    }
}

Console.WriteLine();
Console.WriteLine("b) feladat");
Console.Write("Adj meg egy ID-t: ");
int id = int.Parse(Console.ReadLine()!);
Animal? animal = await api.GetAnimalById(id);
if (animal == null)
{
    Console.WriteLine("Nincs ilyen ID-jű állat.");
}
else
{
    Console.WriteLine("ID: " + animal.Id);
    Console.WriteLine("Név: " + animal.Name);
    Console.WriteLine("Érték: " + animal.Value + " Ft");
    Console.WriteLine("Év: " + animal.Year);
    Console.WriteLine("Kategória: " + animal.Category);
}

Console.WriteLine();
Console.WriteLine("c) feladat");
Console.Write("Adj meg egy évet: ");
int year = int.Parse(Console.ReadLine()!);
List<Animal> byYear = await api.GetAnimalsByYear(year);
if (byYear.Count == 0)
{
    Console.WriteLine("Abban az évben nem nyilvánítottak védetté állatot.");
}
else
{
    foreach (Animal a in byYear)
    {
        Console.WriteLine("\t" + a.Name);
    }
}

Console.WriteLine();
Console.WriteLine("d) feladat");
List<string> categories = await api.GetCategories();
Console.WriteLine(string.Join(", ", categories));

Console.WriteLine();
Console.WriteLine("e) feladat");
foreach (string category in categories)
{
    int count = 0;
    foreach (Animal a in animals)
    {
        if (a.Category == category)
        {
            count++;
        }
    }
    Console.WriteLine("\t" + category + ": " + count + " db");
}
