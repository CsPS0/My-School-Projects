using System.Text;
using VedettConsole;
using VedettLib;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

RestApiHandler restApiHandler = new();

var maxS = restApiHandler
    .GetAll()
    .Where(x => int.TryParse(x.Ertek, out _))
    .GroupBy(x => int.Parse(x.Ertek))
    .OrderByDescending(x => x.Key)
    .First();
Console.WriteLine("Legnagyobb ertek: " + maxS.Key + " Ft");
Console.WriteLine("A hozza tartozo allatok: " + string.Join(", ", maxS.Select(x => x.Nev)));

Console.Write("Melyik allat adataira kivancsi? ");
if (int.TryParse(Console.ReadLine(), out int id))
{
    VedettAllat? allat = restApiHandler.GetById(id);
    if (allat != null)
    {
        Console.WriteLine(allat);
    }
    else
    {
        Console.WriteLine("Nem talaltam ezzel az ID-vel allatot.");
    }
}

Console.Write("Melyik ev allataira kivancsi? ");
if (int.TryParse(Console.ReadLine(), out int year))
{
    var allatok = restApiHandler.GetByYear(year).ToList();
    if (allatok.Count > 0)
    {
        Console.WriteLine("Az allatok: " + string.Join(", ", allatok.Select(x => x.Nev)));
    }
    else
    {
        Console.WriteLine("Nem talaltam allatot ebben az evben.");
    }
}

var categories = restApiHandler.GetCategories().ToArray();
Console.WriteLine("A kategoriak: " + string.Join(", ", categories));
foreach (var category in categories)
{
    Console.WriteLine($"Allatok szama a {category} kategoriaban: {restApiHandler.GetByCategory(category).Count()}");
}

Console.ReadLine();
