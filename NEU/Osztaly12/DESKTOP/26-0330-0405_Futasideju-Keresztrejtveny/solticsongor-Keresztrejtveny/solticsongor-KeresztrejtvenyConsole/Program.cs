using solticsongor_KeresztrejtvenyLib;

namespace solticsongor_KeresztrejtvenyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "kr1.txt";
            KeresztrejtvenyRacs kr = new KeresztrejtvenyRacs(filename);

            Console.WriteLine("5. feladat: A keresztrejtveny mérete");
            Console.WriteLine($"Sorok száma: {kr.SorokDb}");
            Console.WriteLine($"Oszlopok száma: {kr.OszlopokDb}");

            Console.WriteLine("\n6. feladat: A beolvasott keresztrejtveny");
            kr.MegjelenitRacs();

            Console.WriteLine($"\n7. feladat: A leghosszabb függ.: {kr.LeghosszabbFuggoleges()} karakter");

            Console.WriteLine("\n8. feladat: Vízszintes szavak statisztikája");
            var stat = kr.VizszintesStatisztika();
            foreach (var item in stat)
            {
                Console.WriteLine($"{item.Key} betűs: {item.Value} darab");
            }

            Console.WriteLine("\n9. feladat: A keresztrejtveny számokkal");
            kr.Sorszamoz();
            kr.MegjelenitSorszamokkal();
        }
    }
}
