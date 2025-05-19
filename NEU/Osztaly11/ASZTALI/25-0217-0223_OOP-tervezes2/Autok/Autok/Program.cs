using Autok;

Auto auto1 = new Auto("Ford", "Kiss Péter", 180);
Auto auto2 = new Auto("BMW", "Nagy Ádám", 200);
string autoMoji = "\uD83D\uDE97";

for (int i = 0; i < 5; i++)
{
    auto1.SebessegMeres();
    auto2.SebessegMeres();
}

if (auto1.Osszehasonlitas(auto2) == 1)
{
    Console.OutputEncoding = System.Text.Encoding.UTF8;
    Console.WriteLine($"Az első {autoMoji} gyorsabb!");
}
else if (auto1.Osszehasonlitas(auto2) == -1)
{
    Console.OutputEncoding = System.Text.Encoding.UTF8;
    Console.WriteLine($"A második {autoMoji} gyorsabb!");
}
else
{
    Console.OutputEncoding = System.Text.Encoding.UTF8;
    Console.WriteLine($"Mindkettő {autoMoji} ugyanolyan sebességű!");
}

List<Auto> list = new List<Auto>()
{
    new Auto("Lotus", "Solti Csongor Péter", 140),
    new Auto("Tesla", "Solti Olivér Ádám", 354),
    new Auto("Skoda", "Turek Ferenc", 232),
    new Auto("Mercedes", "Ganyó Péter", 140),
    new Auto("Citroen", "Turekné Maszár Márta", 120)
};

foreach (var auto in list)
{
    for (int i = 0; i < 5; i++)
    {
        auto.SebessegMeres();
    }
    Console.WriteLine(auto.Log());
}

int max = 0;
int maxid = 0;

for (int i = 0; i < list.Count; i++)
{
    if (list[i].MaxSebesseg > max)
    {
        max = list[i].MaxSebesseg;
        maxid = i;
    }
}

Console.WriteLine("\nA leggyorsabb autó:");
Console.WriteLine($"{list[maxid].Vezeto}, {list[maxid].Tipus}, {list[maxid].MaxSebesseg} km/h.");

Console.WriteLine();