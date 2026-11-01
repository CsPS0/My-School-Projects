using formaLib;

var sorok1 = File.ReadAllLines("eredmenyek.csv").Skip(1);
var sorok2 = File.ReadAllLines("eredmenyek_v2.csv").Skip(1);
var osszesSor = sorok1.Concat(sorok2).Where(sor => !string.IsNullOrWhiteSpace(sor) && sor.Count(c => c == ';') == 10); //a 2 csv egybevonva

var versenyek = new Versenyzok(osszesSor);

// 2. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("2. feladat: ");
Console.ResetColor();
Console.WriteLine("Hill vezetéknevűek:");
var hillVersenyzok = versenyek.GetHillVersenyzok();
foreach (var v in hillVersenyzok)
{
    Console.WriteLine($"\t- {v.Nev} ({v.Nemzet}) {v.Szuldat:yyyy.MM.dd.}");
}

// 3. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("\n3. feladat: ");
Console.ResetColor();
Console.WriteLine("futamgyőztesek:");
Console.WriteLine($"\t- {versenyek.GetFutamgyoztesekAsString()}");

// 4. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("\n4. feladat: ");
Console.ResetColor();
Console.Write("Juan-Manuel Fangio ");
var fangioKora = versenyek.GetFangioElsoVersenyKora();
if (fangioKora.HasValue)
{
    Console.Write($"{fangioKora} éves volt ");
}
Console.WriteLine("az első versenyén.");

// 5. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("\n5. feladat: ");
Console.ResetColor();
Console.WriteLine("Ferrariknál a 3 leggyakoribb hiba:");
var ferrariHibak = versenyek.GetFerrariLeggyakoribbHibak();
foreach (var hiba in ferrariHibak)
{
    Console.WriteLine($"\t- {hiba.Key}: {hiba.Value} eset");
}

// 6. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("\n6. feladat: ");
Console.ResetColor();
var csapatNelkuliek = versenyek.GetCsapatNelkuliekSzama();
Console.WriteLine($"{csapatNelkuliek} olyan versenyző volt, akinek valamelyik versenyén nem volt csapata");

// 7. Feladat
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("\n7. feladat: ");
Console.ResetColor();
Console.Write("Magyarország után rendezték az első nagydíjukat: ");
Console.WriteLine($"{versenyek.GetKesobbiHelyszinekAsString()}");

// 8. Feladat
var monacoLines = new List<string>();
var monacoEredmenyek = versenyek.GetMonacoEredmenyek();
foreach (var evGroup in monacoEredmenyek)
{
    monacoLines.Add(evGroup.Key.ToString());
    foreach (var v in evGroup.OrderBy(v => v.Helyezes))
    {
        monacoLines.Add($"{v.Helyezes}. {v.Nev} ({v.Csapat})");
    }
    monacoLines.Add("");
}
File.WriteAllLines("monaco.txt", monacoLines);
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("\n8. feladat: ");
Console.ResetColor();
Console.ForegroundColor= ConsoleColor.Yellow;
Console.Write("monaco.txt ");
Console.ResetColor();
Console.WriteLine("létrehozva a \"root\" mappába.");