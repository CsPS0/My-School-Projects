namespace brainrotLib
{
    public class BrainrotOutput
    {
        private readonly BrainrotFactory factory;
        private IBrainrot[,] matrix;
        private int matrixSize;

        public BrainrotOutput(BrainrotFactory factory)
        {
            this.factory = factory;
            this.matrixSize = 0;
            this.matrix = new IBrainrot[0, 0];
        }

        // 1.Feladat
        public void CreateMatrix(int n)
        {
            matrixSize = n;
            matrix = new IBrainrot[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = factory.Create();
                }
            }
        }

        // 2.Feladat
        public void DisplayMatrix()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Brainrot mátrix:");
            Console.ResetColor();

            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    try
                    {
                        var brainrot = matrix[i, j];
                        if (brainrot == null)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write('?');
                        }
                        else if (brainrot is SkibidiToilet)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(brainrot.Megjelenites);
                        }
                        else if (brainrot is ItalianAnimal)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write('X');
                        }
                        else if (brainrot is VigyazMagyar)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(brainrot.Megjelenites);
                        }
                    }
                    catch (NotSupportedException)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('X'); // állat esetén
                    }
                    Console.ResetColor();
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        // 3.Feladat
        public void DisplayTotalCount()
        {
            var allBrainrots = GetAllBrainrotsFromMatrix();
            var totalCount = allBrainrots.Count();
            Console.Write("Összesen ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(totalCount);
            Console.ResetColor();
            Console.WriteLine(" brainrot található a területen.");
        }

        // 4.Feladat
        public void DisplayGroupByName()
        {
            var allBrainrots = GetAllBrainrotsFromMatrix();
            var groupedByName = allBrainrots
                .Where(b => b != null)
                .GroupBy(b => b.Nev)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .ToList();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Csoportosítás név szerint:");
            Console.ResetColor();

            foreach (var group in groupedByName)
            {
                Console.Write("  ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(group.Name);
                Console.ResetColor();
                Console.Write(": ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(group.Count);
                Console.ResetColor();
                Console.WriteLine(" db");
            }
            Console.WriteLine();
        }

        // 5.Feladat
        public void DisplayGroupByType()
        {
            var allBrainrots = GetAllBrainrotsFromMatrix();
            var groupedByType = allBrainrots
                .Where(b => b != null)
                .GroupBy(b => b.GetType().Name)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToList();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Csoportosítás típus szerint:");
            Console.ResetColor();

            foreach (var group in groupedByType)
            {
                Console.Write("  ");
                Console.ForegroundColor = GetColorForType(group.Type);
                Console.Write(group.Type);
                Console.ResetColor();
                Console.Write(": ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(group.Count);
                Console.ResetColor();
                Console.WriteLine(" db");
            }
            Console.WriteLine();
        }

        // 6.Feladat
        public void DisplayMostAndLeastCommon()
        {
            var allBrainrots = GetAllBrainrotsFromMatrix();
            var typeGroups = allBrainrots
                .Where(b => b != null)
                .GroupBy(b => b.GetType().Name)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToList();

            if (typeGroups.Any())
            {
                var mostCommon = typeGroups.OrderByDescending(g => g.Count).First();
                var leastCommon = typeGroups.OrderBy(g => g.Count).First();

                Console.Write("Legtöbb: ");
                Console.ForegroundColor = GetColorForType(mostCommon.Type);
                Console.Write(mostCommon.Type);
                Console.ResetColor();
                Console.Write(" (");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(mostCommon.Count);
                Console.ResetColor();
                Console.WriteLine(" db)");

                Console.Write("Legkevesebb: ");
                Console.ForegroundColor = GetColorForType(leastCommon.Type);
                Console.Write(leastCommon.Type);
                Console.ResetColor();
                Console.Write(" (");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(leastCommon.Count);
                Console.ResetColor();
                Console.WriteLine(" db)");
            }
            Console.WriteLine();
        }

        // 7.Feladat
        public void DisplayDetailedDescriptions()
        {
            var allBrainrots = GetAllBrainrotsFromMatrix().Where(b => b != null).ToList();

            foreach (var brainrot in allBrainrots.DistinctBy(b => b.Nev))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\nNév: {brainrot.Nev}");
                
                Console.ForegroundColor = GetColorForType(brainrot.GetType().Name);
                Console.WriteLine($"Típus: {brainrot.GetType().Name}");
                
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Leírás: {brainrot.Leiras}");
                
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Létrehozva: {brainrot.Datum:yyyy.MM.dd}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                if (brainrot is SkibidiToilet toilet)
                {
                    Console.WriteLine($"Gyakoriság: {toilet.Gyakorisag}");
                }
                else if (brainrot is ItalianAnimal animal)
                {
                    Console.WriteLine($"Mix: {animal.Mix}");
                    Console.WriteLine($"Veszélyesség: {animal.Veszelyesseg}/10");
                }
                else if (brainrot is VigyazMagyar magyar)
                {
                    Console.WriteLine($"Politikai: {(magyar.PolitikaE ? "Igen" : "Nem")}");
                    Console.WriteLine($"Offenzív szint: {magyar.Offenziv}");
                }
                Console.ResetColor();
                Console.WriteLine(new string('-', 50));
            }
            Console.WriteLine();
        }

        // 8.Feladat
        public void DisplayChronologicalStats()
        {
            var allBrainrots = GetAllBrainrotsFromMatrix().Where(b => b != null);

            var chronological = allBrainrots
                .GroupBy(b => b.Datum.ToString("yyyy.MM"))
                .OrderBy(g => g.Key)
                .Select(g => new { 
                    Month = g.Key, 
                    Count = g.Count(),
                    Types = g.GroupBy(b => b.GetType().Name).ToDictionary(t => t.Key, t => t.Count())
                });

            foreach (var month in chronological)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{month.Month}");
                Console.ResetColor();
                Console.Write(" - Összesen: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{month.Count} db");
                Console.ResetColor();

                foreach (var type in month.Types)
                {
                    Console.Write("  ");
                    Console.ForegroundColor = GetColorForType(type.Key);
                    Console.Write(type.Key);
                    Console.ResetColor();
                    Console.Write(": ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(type.Value);
                    Console.ResetColor();
                    Console.WriteLine(" db");
                }
            }
            Console.WriteLine();
        }

        // 9.Feladat
        public void DisplaySpecialStats()
        {
            var allBrainrots = GetAllBrainrotsFromMatrix().Where(b => b != null);

            // Toilet statisztika
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nToilet gyakoriság eloszlás:");
            Console.ResetColor();

            var toilets = allBrainrots.OfType<SkibidiToilet>();
            var toiletStats = toilets
                .GroupBy(t => t.Gyakorisag)
                .Select(g => new { Frequency = g.Key, Count = g.Count() });

            foreach (var stat in toiletStats.OrderByDescending(s => s.Count))
            {
                Console.Write("  ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(stat.Frequency);
                Console.ResetColor();
                Console.Write(": ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(stat.Count);
                Console.ResetColor();
                Console.WriteLine(" db");
            }

            // Animal statisztika
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nÁllat statisztika:");
            Console.ResetColor();

            var animals = allBrainrots.OfType<ItalianAnimal>();
            var avgDanger = animals.Any() ? animals.Average(a => a.Veszelyesseg) : 0;
            var mostDangerous = animals.OrderByDescending(a => a.Veszelyesseg).FirstOrDefault();

            Console.Write("  Átlagos veszélyesség: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{avgDanger:F1}/10");
            Console.ResetColor();

            if (mostDangerous != null)
            {
                Console.Write("  Legveszélyesebb: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(mostDangerous.Nev);
                Console.ResetColor();
                Console.Write(" (");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{mostDangerous.Veszelyesseg}/10");
                Console.ResetColor();
                Console.WriteLine(")");
            }

            // Magyar statisztika
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nMagyar statisztika:");
            Console.ResetColor();

            var magyarok = allBrainrots.OfType<VigyazMagyar>();
            var politicalCount = magyarok.Count(m => m.PolitikaE);
            var nonPoliticalCount = magyarok.Count(m => !m.PolitikaE);

            Console.Write("  Politikai: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(politicalCount);
            Console.ResetColor();
            Console.WriteLine(" db");

            Console.Write("  Nem politikai: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(nonPoliticalCount);
            Console.ResetColor();
            Console.WriteLine(" db");
            Console.WriteLine();
        }

        private ConsoleColor GetColorForType(string type)
        {
            return type switch
            {
                "SkibidiToilet" => ConsoleColor.Green,
                "ItalianAnimal" => ConsoleColor.Yellow,
                "VigyazMagyar" => ConsoleColor.Magenta,
                _ => ConsoleColor.White
            };
        }

        private IEnumerable<IBrainrot> GetAllBrainrotsFromMatrix()
        {
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    yield return matrix[i, j];
                }
            }
        }
    }
}