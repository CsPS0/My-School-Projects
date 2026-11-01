using koteszetLib;

namespace solticsongor_Viragkoteszet
{
    class Program
    {
        static void Main(string[] args)
        {
            var alapanyagokSorai = File.ReadAllLines("alapanyagok.txt").Skip(1);
            var alapanyagok = new Katalogus(alapanyagokSorai.Select(sor => new Alapanyag(sor)));

            var termekekSorai = File.ReadAllLines("termekek.txt").Skip(1);
            var termekek = new Termekek(termekekSorai, alapanyagok);

            var dolgozokSorai = File.ReadAllLines("dolgozok.txt").Skip(1);
            var dolgozok = new Dolgozok(dolgozokSorai);

            var feladatkiosztasSorai = File.ReadAllLines("feladatkiosztas.txt").Skip(1);
            var feladatKiosztas = new FeladatKiosztas();
            feladatKiosztas.Kioszt(feladatkiosztasSorai, dolgozok, termekek);

            Console.WriteLine(termekek.ToString());

            Console.WriteLine("Dolgozók munkaideje:");
            foreach (var dolgozo in dolgozok)
            {
                Console.WriteLine(dolgozo.ToString());
            }
        }
    }
}