using nullableLib;

Tanulok csoport1 = new Tanulok("csoport1.csv");
Tanulok csoport2 = new Tanulok("csoport2.csv");

// 1. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. Feladat");
Console.ResetColor();
Console.Write("Add meg, hogy hanyadik csoportot szeretned kiértékelni: ");
Console.ForegroundColor = ConsoleColor.Yellow;
string csoportValasztas = Console.ReadLine() ?? "1";
Console.ResetColor();

if (csoportValasztas == "1" || csoportValasztas == "2")
{
    string filenev = $"csoport{csoportValasztas}.csv";
    if (File.Exists(filenev))
    {
        string[] sorok = File.ReadAllLines(filenev);
        if (sorok.Length > 1)
        {
            int diakokSzama = 0;
            int hianyzokSzama = 0;
            for (int i = 1; i < sorok.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(sorok[i].Replace(";", "")))
                {
                    diakokSzama++;
                }
                else
                {
                    hianyzokSzama++;
                }
            }
            Console.WriteLine($"A(z) {csoportValasztas}. csoportban {diakokSzama} diák írta meg a tesztet, és {hianyzokSzama} diák hiányzott.");
        }
        else
        {
            Console.WriteLine("A fájl létezik, de a csoport még nem írta meg a tesztet.");
        }
    }
    else
    {
        Console.WriteLine("Nincs ilyen csoport azonosítóval fájl.");
    }
}
else
{
    Console.WriteLine("Nincs ilyen csoport azonosítóval fájl.");
}


// 2. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n2. Feladat");
Console.ResetColor();
Console.Write("Adj meg egy nevet: ");
Console.ForegroundColor = ConsoleColor.Yellow;
string keresettNev = (Console.ReadLine() ?? "").Trim();
Console.ResetColor();

if (!string.IsNullOrEmpty(keresettNev))
{
    List<Tanulo> talalatok1 = csoport1.KeresesNevvel(keresettNev);
    List<Tanulo> talalatok2 = csoport2.KeresesNevvel(keresettNev);

    if (talalatok1.Count > 0)
    {
        Console.WriteLine($"Találatok az 1. csoportban a(z) '{keresettNev}' névre:");
        foreach (var tanulo in talalatok1)
        {
            Console.WriteLine($"- {tanulo.Nev}:");
            for (int i = 0; i < tanulo.Pontok.Length; i++)
            {
                Console.WriteLine($"\tFeladat {i + 1}: {(tanulo.Pontok[i] 
                    != null 
                    ? tanulo.Pontok[i].ToString() 
                    : "-")}");
            }
            Console.WriteLine($"\tÖsszesen: {tanulo.Osszpontszam} pont\n");
        }
    }

    if (talalatok2.Count > 0)
    {
        Console.WriteLine($"Találatok a 2. csoportban a(z) '{keresettNev}' névre:");
        foreach (var tanulo in talalatok2)
        {
            Console.WriteLine($"- {tanulo.Nev}:");
            for (int i = 0; i < tanulo.Pontok.Length; i++)
            {
                Console.WriteLine($"\tFeladat {i + 1}: {(tanulo.Pontok[i] 
                    != null 
                    ? tanulo.Pontok[i].ToString() 
                    : "-")}");
            }
            Console.WriteLine($"\tÖsszesen: {tanulo.Osszpontszam} pont");
        }
    }

    if (talalatok1.Count == 0 && talalatok2.Count == 0)
    {
        Console.WriteLine("Nincs ilyen nevű személy.");
    }
}
else
{
    Console.WriteLine("Kérem, adjon meg egy nevet a kereséshez.");
}

// 3. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n3. Feladat");
Console.ResetColor();

var osszesTanulo = csoport1.Lista.Concat(csoport2.Lista).ToList();

for (int i = 0; i < 5; i++)
{
    int megoldottak = osszesTanulo
        .Count(t => t.Pontok[i] != null);
    double atlagPontszam = osszesTanulo
        .Where(t => t.Pontok[i] != null)
        .Average(t => t.Pontok[i]) ?? 0;
    Console.WriteLine($"A(z) {i + 1}. feladatot {megoldottak} diák oldotta meg, átlagosan {atlagPontszam:F2} pontot szereztek.");
}

int nemOldottMegSemmit = osszesTanulo.Count(t => t.Osszpontszam == 0);
Console.WriteLine($"{nemOldottMegSemmit} diák nem oldott meg semmit a teszten.");


// 4. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("\n4. Feladat");
Console.ResetColor();

for (int i = 1; i <= 2; i++)
{
    string szazalekFile = $"szazalek{i}.csv";
    using (StreamWriter sw = new StreamWriter(szazalekFile))
    {
        sw.WriteLine("sorszam;nev;szazalek;eredmeny");
        var csoport = i == 1 
            ? csoport1 
            : csoport2;
        int sorszam = 1;
        foreach (var tanulo in csoport.Lista)
        {
            sw.WriteLine($"{sorszam};{tanulo.Nev};{tanulo.Szazalek}%;{tanulo.Eredmeny}");
            sorszam++;
        }
    }
    Console.WriteLine($"A(z) {szazalekFile} fájl sikeresen létrehozva.");
}