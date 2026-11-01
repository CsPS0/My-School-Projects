using szoftverLib;

var repository = new Bekeresek();

try
{
    Console.WriteLine($"4. feladat: összesen {repository.GetOsszesGepSzama()} gép szerepel a nyilvántartásban");

    Console.WriteLine("6. feladat: Gépek száma típusonként:");
    foreach (var group in repository.GetGepekTipusSzerint())
    {
        Console.WriteLine($"   {group.Key} - {group.Count()} db");
    }

    Console.Write("7. feladat: Adjon meg egy gép ID-t: ");
    string idInput = Console.ReadLine()?.Trim() ?? string.Empty;
    
    if (int.TryParse(idInput, out int machineId))
    {
        var machine = repository.GetGepById(machineId);
        if (machine != null)
        {
            int szoftverekSzama = repository.GetTelepitettSzoftverekSzama(machineId);
            Console.WriteLine($"   IP cím: {machine.IpAddress}");
            Console.WriteLine($"   Hely: {machine.Location}");
            Console.WriteLine($"   Telepített szoftverek száma: {szoftverekSzama} db");
        }
        else
        {
            Console.WriteLine("   Nincs ilyen gép.");
        }
    }
    else
    {
        Console.WriteLine("   Csak számot lehet megadni!");
    }

    string category = string.Empty;
    var validCategories = repository.GetKategoriak().ToList();
    ISzovegHasonlito stringMatcher = new LevenshteinHasonlito();

    while (true)
    {
        Console.Write("8. feladat: Kérem adjon meg egy kategóriát: ");
        category = Console.ReadLine()?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(category)) break;

        if (category.All(char.IsDigit))
        {
            Console.WriteLine("    Csak szöveget lehet megadni kategóriaként! Kérem próbálja újra.");
            continue;
        }

        bool exactMatch = validCategories.Any(k => k.Equals(category, StringComparison.OrdinalIgnoreCase));
        
        if (exactMatch)
        {
            break; 
        }

        string closestMatch = validCategories
            .OrderBy(k => stringMatcher.TávolságKiszámítása(category, k))
            .FirstOrDefault() ?? string.Empty;

        Console.Write($"    Erre gondolt: {closestMatch}? (y/n) ");
        string answer = Console.ReadLine()?.Trim().ToLower() ?? "";
        if (answer == "y" || answer == "yes" || answer == "igen")
        {
            category = closestMatch;
            break; 
        }
    }

    if (!string.IsNullOrEmpty(category))
    {
        var matchingMachines = repository.GetGepekByKategoria(category);

        if (matchingMachines.Any())
        {
            foreach (var machine in matchingMachines)
            {
                Console.WriteLine($"    {machine.IpAddress} - {machine.Location}");
            }
        }
        else
        {
            Console.WriteLine("    Nincs ilyen kategóriájú szoftver telepítve.");
        }
    }
}
catch (FileNotFoundException ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\nHIBA: {ex.Message}");
    Console.ResetColor();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\nVÁRATLAN HIBA: {ex.Message}");
    Console.ResetColor();
}
