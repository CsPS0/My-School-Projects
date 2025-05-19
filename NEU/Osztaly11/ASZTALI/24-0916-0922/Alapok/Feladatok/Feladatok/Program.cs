using System;

class Program
{
    static void Main()
    {
        // |-----------------------------------------------------------------------------------------------|
        // 1. Feladat: Kedvenc dalszövegek kiírása különböző színekkel
        Console.WriteLine("Első Feladat:");
        string[] sorok = {
            "Never gonna give you up",
            "Never gonna let you down",
            "Never gonna run around and desert you",
            "Never gonna make you cry"
        };
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(sorok[0]);
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(sorok[1]);
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(sorok[2]);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(sorok[3]);
        Console.ResetColor();
        // |-----------------------------------------------------------------------------------------------|

        // |-----------------------------------------------------------------------------------------------|
        // 2. Feladat: Két szám matematikai műveletei
        Console.WriteLine("Második Feladat:");
        Console.Write("Írj be egy számot kééérlek: ");
        int numero1 = int.Parse(Console.ReadLine());
        Console.Write("Írj be még egy másikat is pls: ");
        int numero2 = int.Parse(Console.ReadLine());

        Console.WriteLine("Összeg: " + (numero1 + numero2));
        Console.WriteLine("Különbség: " + (numero1 - numero2));
        Console.WriteLine("Szorzat: " + (numero1 * numero2));
        Console.WriteLine("Hányszor van meg a 2. szám az elsőben: " + (numero1 / numero2));
        Console.WriteLine("Osztási maradék: " + (numero1 % numero2));
        Console.WriteLine("Valós osztás eredménye: " + ((double)numero1 / numero2));
        // |-----------------------------------------------------------------------------------------------|

        // |-----------------------------------------------------------------------------------------------|
        // 3. Feladat: Diák életkorának kiszámítása
        Console.WriteLine("Harmadik Feladat:");
        Console.Write("Ha tudod melyik évben születtél, akkor pls add meg: ");
        int geburtsjahr = int.Parse(Console.ReadLine());
        int aktuellesjahr = DateTime.Now.Year;
        Console.WriteLine("Remekül tartod magad, mivel " + (aktuellesjahr - geburtsjahr) + " éves vagy.");
        // |-----------------------------------------------------------------------------------------------|

        // |-----------------------------------------------------------------------------------------------|
        // 4. Feladat: Érettségi teljesítmény százalékos kiszámítása
        Console.WriteLine("Negyedik Feladat:");
        Console.Write("Hány pontot értél el az ágazati alapvizsgán?: ");
        int pontocskak = int.Parse(Console.ReadLine());
        double szazalekocska = (double)pontocskak / 120 * 100;
        Console.WriteLine("Teljesítmény: " + szazalekocska.ToString("0.00") + "%");
        // |-----------------------------------------------------------------------------------------------|

        // |-----------------------------------------------------------------------------------------------|
        // 5. Feladat: Háromszög kerületének kiszámítása
        Console.WriteLine("Ötödik Feladat:");
        Console.Write("Add meg az 'a' oldalt: ");
        double a = double.Parse(Console.ReadLine());
        Console.Write("Add meg a 'b' oldalt: ");
        double b = double.Parse(Console.ReadLine());
        Console.Write("Add meg a 'c' oldalt: ");
        double c = double.Parse(Console.ReadLine());
        double kerulet = a + b + c;
        Console.WriteLine("A háromszög kerülete: " + kerulet);
        // |-----------------------------------------------------------------------------------------------|

        // |-----------------------------------------------------------------------------------------------|
        // 6. Feladat: Testtömegindex (BMI) számítása
        Console.WriteLine("Hatodik Feladat:");
        Console.Write("Add meg a súlyodat (kg): ");
        double suly = double.Parse(Console.ReadLine());
        Console.Write("Add meg a magasságodat (cm): ");
        double magassag = double.Parse(Console.ReadLine()) / 100;
        double bmi = suly / (magassag * magassag);
        Console.WriteLine("Body mass index (BMI): " + bmi.ToString("0.00"));
        // |-----------------------------------------------------------------------------------------------|

        // |-----------------------------------------------------------------------------------------------|
        // 7. Feladat: Euró vásárlás forintban
        Console.WriteLine("Hetedik Feladat:");
        Console.Write("Mennyi eurót szeretnél vásárolni? ");
        double euroMennyiseg = double.Parse(Console.ReadLine());
        Console.Write("Mennyi az aktuális euró árfolyama? ");
        double arfolyam = double.Parse(Console.ReadLine());
        double osszeg = euroMennyiseg * arfolyam;
        Console.WriteLine("Fizetendő összeg: " + osszeg + " Ft");
        // |-----------------------------------------------------------------------------------------------|

        // |-----------------------------------------------------------------------------------------------|
        // 8. Feladat: Törtek matematikai műveletei
        Console.WriteLine("Nyolcadik Feladat:");
        Console.Write("Add meg az első tört számlálóját: ");
        int szam1a = int.Parse(Console.ReadLine());
        Console.Write("Add meg az első tört nevezőjét: ");
        int szam1b = int.Parse(Console.ReadLine());
        Console.Write("Add meg a második tört számlálóját: ");
        int szam2a = int.Parse(Console.ReadLine());
        Console.Write("Add meg a második tört nevezőjét: ");
        int szam2b = int.Parse(Console.ReadLine());

        int osszegSzamolo = szam1a * szam2b + szam2a * szam1b;
        int osszegNevezo = szam1b * szam2b;
        int kulonbsegSzamolo = szam1a * szam2b - szam2a * szam1b;
        int kulonbsegNevezo = szam1b * szam2b;
        int szorzatSzamolo = szam1a * szam2a;
        int szorzatNevezo = szam1b * szam2b;
        int hanyadosSzamolo = szam1a * szam2b;
        int hanyadosNevezo = szam1b * szam2a;

        Console.WriteLine("Összeg: " + osszegSzamolo + "/" + osszegNevezo);
        Console.WriteLine("Különbség: " + kulonbsegSzamolo + "/" + kulonbsegNevezo);
        Console.WriteLine("Szorzat: " + szorzatSzamolo + "/" + szorzatNevezo);
        Console.WriteLine("Hányados: " + hanyadosSzamolo + "/" + hanyadosNevezo);
        // |-----------------------------------------------------------------------------------------------|

        // |-----------------------------------------------------------------------------------------------|
        // 9. Feladat: Gyümölcsök árának kiszámítása
        Console.WriteLine("Kilencedik Feladat:");
        Console.Write("Add meg az alma mennyiségét (kg): ");
        double almaMennyiseg = double.Parse(Console.ReadLine());
        Console.Write("Add meg a szilva mennyiségét (kg): ");
        double szilvaMennyiseg = double.Parse(Console.ReadLine());
        Console.Write("Add meg a szőlő mennyiségét (kg): ");
        double szoloMennyiseg = double.Parse(Console.ReadLine());

        double almaAr = 240.0;
        double szilvaAr = 310.0;
        double szoloAr = 650.0;

        double almaArTotal = almaMennyiseg * almaAr;
        double szilvaArTotal = szilvaMennyiseg * szilvaAr;
        double szoloArTotal = szoloMennyiseg * szoloAr;
        double vegosszeg = almaArTotal + szilvaArTotal + szoloArTotal;

        Console.WriteLine("Alma ára: " + almaArTotal + " Ft");
        Console.WriteLine("Szilva ára: " + szilvaArTotal + " Ft");
        Console.WriteLine("Szőlő ára: " + szoloArTotal + " Ft");
        Console.WriteLine("Végösszeg: " + vegosszeg + " Ft");
        // |-----------------------------------------------------------------------------------------------|

        // |-----------------------------------------------------------------------------------------------|
        // 10. Feladat: Lufik árának kiszámítása
        Console.WriteLine("Tizedik Feladat:");
        int pirosAr = 125;
        int zoldAr = 150;
        int kekAr = 135;

        Console.Write("Hány piros lufit szeretnél? ");
        int pirosDb = int.Parse(Console.ReadLine());
        Console.Write("Hány zöld lufit szeretnél? ");
        int zoldDb = int.Parse(Console.ReadLine());
        Console.Write("Hány kék lufit szeretnél? ");
        int kekDb = int.Parse(Console.ReadLine());

        int totalAr = (pirosDb * pirosAr) + (zoldDb * zoldAr) + (kekDb * kekAr);
        Console.WriteLine("Összesen fizetendő összeg: " + totalAr + " Ft");
        // |-----------------------------------------------------------------------------------------------|

        // |-----------------------------------------------------------------------------------------------|
        // 11. Feladat: Tapéta mennyiségének kiszámítása
        Console.WriteLine("Tizenegyedik Feladat:");
        Console.Write("Szoba hossza (cm): ");
        int tehossz = int.Parse(Console.ReadLine());
        Console.Write("Szoba szélessége (cm): ");
        int teszelesseg = int.Parse(Console.ReadLine());
        Console.Write("Szoba magassága (cm): ");
        int temagassag = int.Parse(Console.ReadLine());
        Console.Write("Ablak területe (cm²): ");
        int ablak = int.Parse(Console.ReadLine());
        Console.Write("Ajtó területe (cm²): ");
        int ajto = int.Parse(Console.ReadLine());

        double falfelulet = (tehossz + teszelesseg) * temagassag;
        double osszesFelulet = falfelulet - ablak - ajto;

        // Hibás konvertálás: az eredmény m² helyett cm²-ben van
        double szuksegesTapeta = osszesFelulet * 1.1;
        Console.WriteLine("Szükséges tapéta (m²): " + szuksegesTapeta.ToString("0.00"));
        // |-----------------------------------------------------------------------------------------------|

        // |-----------------------------------------------------------------------------------------------|
        // 12. Feladat: Kert kerítésének anyagszükséglete
        Console.WriteLine("Tizenketedik Feladat:");
        Console.Write("Kert hosszúsága (m): ");
        int tkhossz = int.Parse(Console.ReadLine());
        Console.Write("Kert szélessége (m): ");
        int tkszelesseg = int.Parse(Console.ReadLine());
        Console.Write("Kapunál kihagyott terület (m): ");
        int kapu = int.Parse(Console.ReadLine());

        int kerulet = 2 * (tkhossz - tkszelesseg) - kapu;
        double lecHossz = kerulet / 0.2;
        Console.WriteLine("Szükséges lécek száma: " + Math.Ceiling(lecHossz));
        // |-----------------------------------------------------------------------------------------------|
    }
}