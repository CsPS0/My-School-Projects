using IskolaLib;
var isk = new Iskola();

var tantargyak = new List<Tantargy>
{
    new Tantargy("Asztali alkalmazások fejlesztése", "AAF"),
    new Tantargy("Német", "NN2"),
    new Tantargy("Backend programozás és tesztelés", "BPT"),
    new Tantargy("Irodalom", "IRO"),
    new Tantargy("Webprogramozás", "WP"),
    new Tantargy("Szoftvertesztelés", "SZT"),
    new Tantargy("Testnevelés", "TES"),
    new Tantargy("Angol nyelv", "AN1"),
    new Tantargy("IKT projektmunka", "IKT2"),
    new Tantargy("Docker - Web", "DW"),
    new Tantargy("Magyar nyelv", "MNY"),
    new Tantargy("Fizika", "FIZ"),
    new Tantargy("Szakmai angol", "SZAN1"),
    new Tantargy("Matematika", "MM5"),
    new Tantargy("Backend", "BE"),
    new Tantargy("Történelem", "TOR"),
    new Tantargy("Osztályfőnöki", "OF"),
    new Tantargy("Állampolgári ismeretek", "AI")
};

foreach (var t in tantargyak)
{
    isk.UjTantargy(t);
}

var diakNevek = new List<string>
{
    "Fábián Barna",
    "Solti Csongor Péter",
    "Kiss Péter Áron",
    "Sőregi Kristóf",
    "Fehér Marcell",
    "Sári Máté",
    "Polyák Dávid",
    "Bodolai Richárd Tamás",
    "Páva Zalán",
    "Varga Zsombor Csanád"
};

var diakok = new Dictionary<string, Diak>();
int idCounter = 1;
foreach (var nev in diakNevek)
{
    var reszek = nev.Split(' ');
    var azon = $"{reszek[0].Substring(0, 2)}{reszek[1].Substring(0, 2)}{idCounter++}".ToUpper();
    var diak = new Diak(nev, azon);
    isk.UjDiak(diak);
    diakok[azon] = diak;
}

Console.Title = "Iskola Nyilvántartó";

while (true)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n===================================");
    Console.WriteLine("      ISKOLA NYILVÁNTARTÓ");
    Console.WriteLine("===================================");
    Console.ResetColor();

    Console.WriteLine("\nVálasszon az alábbi lehetőségek közül:");
    Console.WriteLine("  1. Diák keresése azonosító alapján");
    Console.WriteLine("  2. Tantárgy keresése név alapján");
    Console.WriteLine("  3. Kilépés");

    Console.Write("\nAdja meg a választott menüpont számát: ");
    var valasztas = Console.ReadLine();

    switch (valasztas)
    {
        case "1":
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nAdja meg a diák azonosítóját: ");
            Console.ResetColor();
            var diakAzon = Console.ReadLine();
            if (string.IsNullOrEmpty(diakAzon))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Az azonosító nem lehet üres.");
                Console.ResetColor();
                break;
            }
            try
            {
                var keresettDiak = isk[diakAzon.ToUpper()];
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nMegtalált diák:");
                Console.WriteLine($"  Név: {keresettDiak.Nev}");
                Console.WriteLine($"  Azonosító: {keresettDiak.Azon}");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nHiba: {e.Message}");
                Console.ResetColor();
            }
            break;
        case "2":
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nAdja meg a tantárgy nevét: ");
            Console.ResetColor();
            var targyNev = Console.ReadLine();
            if (string.IsNullOrEmpty(targyNev))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("A tantárgy neve nem lehet üres.");
                Console.ResetColor();
                break;
            }
            try
            {
                var keresettTargy = isk[targyNev, true];
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nMegtalált tantárgy:");
                Console.WriteLine($"  Név: {keresettTargy.Nev}");
                Console.WriteLine($"  Kód: {keresettTargy.Kod}");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nHiba: {e.Message}");
                Console.ResetColor();
            }
            break;
        case "3":
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nViszlát!");
            Console.ResetColor();
            return;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nÉrvénytelen választás. Kérem, a menüpontok közül válasszon.");
            Console.ResetColor();
            break;
    }
}