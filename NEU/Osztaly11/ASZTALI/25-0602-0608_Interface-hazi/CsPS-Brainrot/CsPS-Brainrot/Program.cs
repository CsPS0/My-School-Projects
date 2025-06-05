using brainrotLib;

namespace CsPS_Brainrot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== BRAINROT PROGRAM ===");
            Console.WriteLine();

            var factory = new BrainrotFactory();
            var output = new BrainrotOutput(factory);

            Console.Write("Add meg a mátrix méretét (N): ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                n = 5;
                Console.WriteLine($"Érvénytelen méret, alapértelmezett {n}x{n} mátrix használata.");
            }

            output.CreateMatrix(n);

            output.DisplayMatrix();

            // 3.Feladat
            Console.WriteLine("=== STATISZTIKÁK ===");
            output.DisplayTotalCount();
            Console.WriteLine();

            // 4.Feladat
            output.DisplayGroupByName();

            // 5.Feladat
            output.DisplayGroupByType();

            // 6.Feladat
            output.DisplayMostAndLeastCommon();
        }
    }
}