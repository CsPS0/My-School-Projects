using System;
using System.IO;

namespace labirintusConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // 5. feladat
            string file1 = "Lab1.txt";
            if (!File.Exists(file1))
            {
                // Try to find it in the parent directory if not in the current
                file1 = Path.Combine("..", "Lab1.txt");
            }

            Console.WriteLine("5. feladat: Labirintus adatai");
            LabSim lab1 = new LabSim(file1);
            Console.WriteLine($"\tSorok száma: {lab1.SorokSzama}");
            Console.WriteLine($"\tOszlopok száma: {lab1.OszlopokSzama}");
            Console.WriteLine($"\tKijárat indexe: sor:{lab1.KijaratSorindex} oszlop:{lab1.KijaratOszlopindex}");

            // 6. feladat
            Console.WriteLine("6. feladat: A labirintus");
            lab1.KiirLab();

            // 7. feladat
            lab1.Utkereses();

            // 8. feladat (Szimuláció/Eredmény kijelzése)
            Console.WriteLine("\nAz útkeresés eredménye:");
            lab1.KiirLab();
            if (lab1.KeresesKesz)
            {
                Console.WriteLine("Útvonal megtalálva!");
            }
            else
            {
                Console.WriteLine("Nincs megoldás!");
            }

            // Demonstrating with Lab2.txt as well
            string file2 = "Lab2.txt";
            if (!File.Exists(file2)) file2 = Path.Combine("..", "Lab2.txt");
            if (File.Exists(file2))
            {
                Console.WriteLine("\nLab2.txt tesztelése:");
                LabSim lab2 = new LabSim(file2);
                lab2.Utkereses();
                lab2.KiirLab();
                if (lab2.KeresesKesz) Console.WriteLine("Útvonal megtalálva!");
                else Console.WriteLine("Nincs megoldás!");
            }

            Console.WriteLine("\nNyomjon meg egy billentyűt a kilépéshez...");
            Console.ReadKey();
        }
    }
}
