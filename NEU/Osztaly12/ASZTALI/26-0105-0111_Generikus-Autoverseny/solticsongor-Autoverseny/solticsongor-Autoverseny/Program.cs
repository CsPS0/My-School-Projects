using solticsongor_Autoverseny;

Console.Write("Adja meg a fájl nevét: ");
string? fileName = Console.ReadLine();

if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
{
    Console.WriteLine("A fájl nem található!");
    return;
}

string[] lines = File.ReadAllLines(fileName);
string[] firstLineParts = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
int totalLaps = int.Parse(firstLineParts[0]);
int racerCount = int.Parse(firstLineParts[1]);

List<Racer> racers = new List<Racer>();

for (int i = 1; i <= racerCount; i++)
{
    if (string.IsNullOrWhiteSpace(lines[i])) continue;
    string[] parts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string name = parts[0];
    int type = int.Parse(parts[1]);

    switch (type)
    {
        case 1: racers.Add(new AggressiveRacer(name)); break;
        case 2: racers.Add(new MomentumRacer(name)); break;
        case 3: racers.Add(new DangerousRacer(name)); break;
        case 4: racers.Add(new CautiousRacer(name)); break;
        default: Console.WriteLine($"Ismeretlen típus: {type}"); break;
    }
}

Console.WriteLine("\nKezdődik a verseny!");
List<string> crashedRacersLog = new List<string>();
Random rand = new Random();

    for (int lap = 1; lap <= totalLaps; lap++)
{
    List<Racer> racersToMove = new List<Racer>();
    for (int i = 0; i < racers.Count; i++)
    {
        if (racers[i].ShouldPit())
        {
            racers[i].Refuel();
            racersToMove.Add(racers[i]);
        }
    }

    foreach (var racer in racersToMove)
    {
        int currentIndex = racers.IndexOf(racer);
        if (currentIndex != -1)
        {
            racers.RemoveAt(currentIndex);
            int newIndex = Math.Min(racers.Count, currentIndex + 5);
            racers.Insert(newIndex, racer);
        }
    }

    int ovIndex = 1;
    while (ovIndex < racers.Count)
    {
        Racer attacker = racers[ovIndex];
        Racer defender = racers[ovIndex - 1];

        if (attacker.ShouldOvertake(lap))
        {
            attacker.DecreaseFuel(4);

            bool accidentHappened = false;
            double chance = rand.NextDouble();
            
            double pAttacker = 0.04;
            double pBoth = 0.04;
            double pMass = 0.02;

            if (attacker.IsDangerous())
            {
                pAttacker *= 2;
                pBoth *= 2;
                pMass *= 2;
            }

            double tAttacker = pAttacker;
            double tBoth = tAttacker + pBoth;
            double tMass = tBoth + pMass;

            if (chance < tAttacker)
            {
                attacker.IsOut = true;
                crashedRacersLog.Add($"{attacker.Name} (Kör: {lap}, Támadó kiesett)");
                racers.RemoveAt(ovIndex); 
                accidentHappened = true;
            }
            else if (chance < tBoth)
            {
                attacker.IsOut = true;
                defender.IsOut = true;
                crashedRacersLog.Add($"{attacker.Name} és {defender.Name} (Kör: {lap}, Páros baleset)");
                
                racers.RemoveAt(ovIndex);
                racers.RemoveAt(ovIndex - 1);
                ovIndex = Math.Max(1, ovIndex - 1); 
                accidentHappened = true;
            }
            else if (chance < tMass)
            {
                List<Racer> victims = new List<Racer>();
                victims.Add(attacker);
                victims.Add(defender);
                
                if (ovIndex - 2 >= 0) victims.Add(racers[ovIndex - 2]);
                if (ovIndex + 1 < racers.Count) victims.Add(racers[ovIndex + 1]);

                crashedRacersLog.Add($"Tömegkarambol: {string.Join(", ", victims.Select(v => v.Name))} (Kör: {lap})");

                foreach (var v in victims)
                {
                    v.IsOut = true;
                    racers.Remove(v);
                }
                
                ovIndex = Math.Max(1, ovIndex - 2); 
                accidentHappened = true;
            }

            if (!accidentHappened)
            {
                if (attacker.IsOvertakeSuccessful())
                {
                    racers[ovIndex] = defender;
                    racers[ovIndex - 1] = attacker;
                }
                ovIndex++;
            }
        }
        else
        {
            ovIndex++;
        }
    }

    foreach (var r in racers)
    {
        r.DecreaseFuel(5);
    }

    Console.WriteLine($"\n--- {lap}. Kör ---");
    Console.WriteLine("Sorrend: " + string.Join(", ", racers.Select(r => r.Name)));
    
    var newCrashes = crashedRacersLog.Where(l => l.Contains($"(Kör: {lap},") || l.Contains($"(Kör: {lap})")).ToList();
    if (newCrashes.Any())
    {
        foreach (var crash in newCrashes)
        {
            Console.WriteLine("Baleset: " + crash);
        }
    }
    
    if (racers.Count == 0)
    {
        Console.WriteLine("\nMindenki kiesett! A verseny véget ért.");
        break;
    }
    
    if (racers.Count == 1)
    {
        Console.WriteLine($"\nCsak {racers[0].Name} maradt versenyben! A verseny véget ért.");
        break;
    }
}

Console.WriteLine("\n\n=== VÉGEREDMÉNY ===");
if (racers.Count > 0)
{
    Console.WriteLine("Dobogósok:");
    for (int i = 0; i < Math.Min(3, racers.Count); i++)
    {
        Console.WriteLine($"{i + 1}. hely: {racers[i].Name} (Benzin: {racers[i].Fuel})");
    }
}
else
{
    Console.WriteLine("Nincs befejező versenyző.");
}

Console.WriteLine("\nKiesett versenyzők:");
foreach (var log in crashedRacersLog)
{
    Console.WriteLine(log);
}

Console.WriteLine("\nA verseny véget ért. Nyomjon meg egy gombot a kilépéshez...");
Console.ReadKey();