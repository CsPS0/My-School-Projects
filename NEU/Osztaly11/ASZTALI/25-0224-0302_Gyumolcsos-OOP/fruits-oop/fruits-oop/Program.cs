#region 2.fel
using fruits_oop;

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. Feladat");
Console.ResetColor();

string[] file = File.ReadAllLines("gyumolcsok.txt");
List<Fruit> fruitList = new List<Fruit>();

foreach (var line in file)
{
    string[] parts = line.Split(';');
    string name = parts[0];
    double weight = Convert.ToDouble(parts[1]);
    fruitList.Add(new Fruit(name, weight));
}
Console.WriteLine("A fájl beolvasása sikeres!");
#endregion

#region 3.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. Feladat");
Console.ResetColor();

foreach (var fruit in fruitList)
{
    Console.WriteLine(fruit.DisplayInfo());
}
#endregion

#region 4.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. Feladat");
Console.ResetColor();

double totalWeight = fruitList.Sum(f => f.Weight);
Console.WriteLine($"Összesen {totalWeight} kg gyümölcs termett.");
#endregion

#region 5.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. Feladat");
Console.ResetColor();

double averageWeight = totalWeight / fruitList.Count;
Console.WriteLine($"Átlagosan {averageWeight:F2} kg gyümölcs termett.");
#endregion

#region 6.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. Feladat");
Console.ResetColor();

int count10kg = fruitList.Count(f => f.Weight == 10);
Console.WriteLine($"Pontos 10 kg termett: {count10kg} fajta.");
#endregion

#region 7.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. Feladat");
Console.ResetColor();

var maxWeightFruit = fruitList.OrderByDescending(f => f.Weight).FirstOrDefault();
Console.WriteLine($"A legtöbb termett gyümölcs: {maxWeightFruit.Name}, összesen {maxWeightFruit.Weight} kg.");
#endregion

#region 8.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. Feladat");
Console.ResetColor();

var fruitsOver30kg = fruitList.Where(f => f.Weight >= 30).Select(f => f.Name);
Console.WriteLine("Legalább 30 kg gyümölcsök: " + string.Join(", ", fruitsOver30kg));
#endregion

#region 9.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. Feladat");
Console.ResetColor();

var lessThan10kgFruit = fruitList.FirstOrDefault(f => f.Weight < 10);
if (lessThan10kgFruit != null)
{
    Console.WriteLine($"A legelső gyümölcs, amelyikből kevesebb, mint 10 kg termett: {lessThan10kgFruit.Name}");
}
else
{
    Console.WriteLine("Nem volt olyan gyümölcs, amelyből kevesebb, mint 10 kg termett.");
}
#endregion