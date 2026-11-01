#region 1.fel
using Snooker;

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("1. feladat");
Console.ResetColor();

List<Versenyzok> lista = new List<Versenyzok>();

StreamReader r = new StreamReader("snooker.txt");
r.ReadLine();

while (!r.EndOfStream)
{
    string[] tomb = r.ReadLine().Split(';');
    int helyezes = int.Parse(tomb[0]);
    string nev = tomb[1];
    string orszag = tomb[2];
    int nyeremeny = int.Parse(tomb[3]);

    lista.Add(new Versenyzok(helyezes, nev, orszag, nyeremeny));
}

r.Close();

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("A fájlok beolvasása sikeres!");
Console.ResetColor();
#endregion

#region 2.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("2. feladat");
Console.ResetColor();

Console.WriteLine($"A világranglistán {lista.Count} versenyző szerepel");
#endregion

#region 3.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("3. feladat");
Console.ResetColor();

var nyertes = lista.OrderBy(x => x.Helyezes).First();
Console.WriteLine("Az első helyen áll:");
Console.WriteLine($"\tNév: {nyertes.Nev}");
Console.WriteLine($"\tOrszág: {nyertes.Orszag}");
Console.WriteLine($"\tNyeremény: {nyertes.Nyeremeny} font");
#endregion

#region 4.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("4. feladat");
Console.ResetColor();

double atlag = lista.Average(x => x.Nyeremeny);
Console.WriteLine($"A versenyzők átlagosan {atlag:F2} fontot kerestek");
#endregion

#region 5.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("5. feladat");
Console.ResetColor();

int nemAnglia = lista.Count(x => x.Orszag != "Anglia");
Console.WriteLine($"{nemAnglia} versenyző nem Anglia színeiben indul");
#endregion

#region 6.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("6. feladat");
Console.ResetColor();

var legjobbKina = lista.Where(x => x.Orszag == "Kína").OrderByDescending(x => x.Nyeremeny).First();
Console.WriteLine("A legjobban kereső kínai versenyző:");
Console.WriteLine($"\tHelyezés: {legjobbKina.Helyezes}");
Console.WriteLine($"\tNév: {legjobbKina.Nev}");
Console.WriteLine($"\tNyeremény összege: {legjobbKina.Nyeremeny * 380} Ft");
#endregion

#region 7.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("7. feladat");
Console.ResetColor();

Console.Write("A versenyző országa: ");
Console.ForegroundColor = ConsoleColor.Yellow;
string keresettOrszag = Console.ReadLine();
Console.ResetColor();

bool vanE = lista.Any(x => x.Orszag == keresettOrszag);
Console.WriteLine(vanE ? $"Van {keresettOrszag} színeiben induló versenyző." : $"Nincs {keresettOrszag} színeiben induló versenyző.");
#endregion

#region 8.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("8. feladat");
Console.ResetColor();

var top3Angol = lista.Where(x => x.Orszag == "Anglia").OrderBy(x => x.Helyezes).Take(3).ToList();
Console.WriteLine("A 3 legelőkelőbb helyen álló angol versenyző:");
for (int i = 0; i < top3Angol.Count; i++)
{
    Console.WriteLine($"\t{i + 1}. {top3Angol[i].Nev}");
}
#endregion

#region 9.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("9. feladat");
Console.ResetColor();

var orszagok = lista.Select(x => x.Orszag).Distinct().OrderBy(x => x);
Console.WriteLine("Az országok nevei: " + string.Join(", ", orszagok));
#endregion

#region 10.fel
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("10. feladat");
Console.ResetColor();

var skotok = lista.Where(x => x.Orszag == "Skócia").OrderBy(x => x.Nev).Select(x => x.Nev).ToList();
Console.WriteLine("Skócia színeiben induló versenyzők: " + string.Join(", ", skotok));
#endregion