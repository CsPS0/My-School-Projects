Console.Title = "lost-puppies";

#region 1.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. feladat");
Console.ResetColor();
string[] file = File.ReadAllLines("kutyak.csv");
List<Dog> all = new List<Dog>();

for (int i = 1; i < file.Length; i++)
{
    string[] seperate = file[i].Split(';');
    string name = seperate[0];
    string sex = seperate[1];
    string type = seperate[2];
    string location = seperate[3];
    DateOnly when = DateOnly.Parse(seperate[4]);

    all.Add(new Dog(name, sex, type, location, when));
}

Console.WriteLine($"Összesen {all.Count} db kutya van eltárolva...");
#endregion

#region 2.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. feladat");
Console.ResetColor();
var time = new DateOnly(2020, 12, 31);
for (int i = 0; i < all.Count; i++)
{
    if (all[i].When == time)
    {
        Console.WriteLine($"{all[i].Name}, {all[i].Sex}, {all[i].Type}, {all[i].Location}, {all[i].When}");
    }
}
#endregion

#region 3.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. feladat");
Console.ResetColor();
int twt = 0;
int twto = 0;

DateOnly t = new DateOnly(2020,01,01);
DateOnly t2 = new DateOnly(2020,12,31);

DateOnly tw = new DateOnly(2021, 01, 01);
DateOnly tw2 = new DateOnly(2021, 12, 31);

for (int i = 0;i < all.Count;i++)
{
    if (all[i].When >= t && all[i].When <= t2)
    {
        twt++;
    }
    if (all[i].When >= tw && all[i].When <= tw2)
    {
        twto++;
    }
}

Console.WriteLine($"Ennyi kutya eltűnését jelentették 2020: {twt}, illetve a rákötvetkező évben 2021-ben: {twto}");
#endregion

#region 4.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. feladat");
Console.ResetColor();
var difftypes = all.Select(x => x.Type).Distinct().ToList();
Console.WriteLine($"A különböző kutyafajtáK: {string.Join($"; {difftypes}")}");
#endregion

#region 5.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. feladat");
Console.ResetColor();
Console.WriteLine("Ismétlődő kutyanevek:");

List<string> seenNames = new List<string>();
List<string> duplicateNames = new List<string>();

for (int i = 0; i < all.Count; i++)
{
    if (seenNames.Contains(all[i].Name) && !duplicateNames.Contains(all[i].Name))
    {
        duplicateNames.Add(all[i].Name);
    }
    else
    {
        seenNames.Add(all[i].Name);
    }
}

foreach (var name in duplicateNames)
{
    Console.WriteLine(name);
}
#endregion

#region 6.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. feladat");
Console.ResetColor();
var female = all.Count(x => x.Sex == "szuka");
var male = all.Count(x => x.Sex == "kan");
Console.WriteLine($"Szukák száma: {female}, kanok száma: {male}");
#endregion

#region 7.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. feladat");
Console.ResetColor();
for (int i = 0; i < all.Count; i++)
{
    if (all[i].Location.Contains("kerület"))
    {
        bool found = false;
        for (int j = 0; j < i; j++)
        {
            if (all[i].Location == all[j].Location)
            {
                found = true;
                break;
            }
        }

        if (!found)
        {
            int count = 0;
            for (int j = 0; j < all.Count; j++)
            {
                if (all[i].Location == all[j].Location)
                {
                    count++;
                }
            }
            Console.WriteLine($"{all[i].Location}: {count}");
        }
    }
}
#endregion

#region 8.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. feladat");
Console.ResetColor();
for (int i = 0; i < all.Count; i++)
{
    bool counted = false;
    for (int j = 0; j < i; j++)
    {
        if (all[i].Type == all[j].Type)
        {
            counted = true;
            break;
        }
    }

    if (!counted)
    {
        int count = 0;
        for (int j = 0; j < all.Count; j++)
        {
            if (all[i].Type == all[j].Type)
            {
                count++;
            }
        }
        if (count >= 5)
        {
            Console.WriteLine(all[i].Type);
        }
    }
}
#endregion

#region 9.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. feladat");
Console.ResetColor();
for (int i = 0; i < all.Count; i++)
{
    bool exists = false;
    string currentMonth = $"{all[i].When.Year}.{all[i].When.Month:D2}";

    for (int j = 0; j < i; j++)
    {
        string previousMonth = $"{all[j].When.Year}.{all[j].When.Month:D2}";
        if (currentMonth == previousMonth)
        {
            exists = true;
            break;
        }
    }

    if (!exists)
    {
        int count = 0;
        for (int j = 0; j < all.Count; j++)
        {
            string compareMonth = $"{all[j].When.Year}.{all[j].When.Month:D2}";
            if (currentMonth == compareMonth)
            {
                count++;
            }
        }
        Console.WriteLine($"{currentMonth}: {count}");
    }
}
#endregion

#region 10.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("10. feladat");
Console.ResetColor();
for (int i = 0; i < all.Count; i++)
{
    bool counted = false;
    for (int j = 0; j < i; j++)
    {
        if (all[i].Location == all[j].Location)
        {
            counted = true;
            break;
        }
    }

    if (!counted)
    {
        int maleCount = 0;
        int femaleCount = 0;
        int unknownCount = 0;

        for (int j = 0; j < all.Count; j++)
        {
            if (all[i].Location == all[j].Location)
            {
                if (all[j].Sex == "kan") maleCount++;
                else if (all[j].Sex == "szuka") femaleCount++;
                else unknownCount++;
            }
        }

        Console.WriteLine($"{all[i].Location}: Kanok: {maleCount}, Szukák: {femaleCount}, Nem megadott: {unknownCount}");
    }
}
#endregion

#region 11.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("11. feladat");
Console.ResetColor();
for (int i = 0; i < all.Count; i++)
{
    bool counted = false;
    for (int j = 0; j < i; j++)
    {
        if (all[i].Type == all[j].Type)
        {
            counted = true;
            break;
        }
    }

    if (!counted)
    {
        Console.Write($"{all[i].Type}: ");
        bool first = true;

        for (int j = 0; j < all.Count; j++)
        {
            if (all[i].Type == all[j].Type)
            {
                bool alreadyPrinted = false;
                for (int k = 0; k < j; k++)
                {
                    if (all[k].Type == all[j].Type && all[k].Location == all[j].Location)
                    {
                        alreadyPrinted = true;
                        break;
                    }
                }

                if (!alreadyPrinted)
                {
                    if (!first)
                    {
                        Console.Write(", ");
                    }
                    Console.Write(all[j].Location);
                    first = false;
                }
            }
        }
        Console.WriteLine();
    }
}
#endregion

#region 12.feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("12. feladat");
Console.ResetColor();
for (int i = 0; i < all.Count; i++)
{
    bool counted = false;
    for (int j = 0; j < i; j++)
    {
        if (all[i].Name == all[j].Name)
        {
            counted = true;
            break;
        }
    }

    if (!counted)
    {
        Console.Write($"{all[i].Name}: ");
        bool first = true;

        for (int j = 0; j < all.Count; j++)
        {
            if (all[i].Name == all[j].Name)
            {
                bool alreadyPrinted = false;
                for (int k = 0; k < j; k++)
                {
                    if (all[k].Name == all[j].Name && all[k].Type == all[j].Type)
                    {
                        alreadyPrinted = true;
                        break;
                    }
                }

                if (!alreadyPrinted)
                {
                    if (!first)
                    {
                        Console.Write(", ");
                    }
                    Console.Write(all[j].Type);
                    first = false;
                }
            }
        }
        Console.WriteLine();
    }
}
#endregion

public record Dog(string Name, string Sex, string Type, string Location, DateOnly When);