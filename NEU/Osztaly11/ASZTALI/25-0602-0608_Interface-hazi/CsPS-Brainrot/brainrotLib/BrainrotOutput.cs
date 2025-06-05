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
            Console.WriteLine("Brainrot mátrix:");
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    try
                    {
                        Console.Write(matrix[i, j]?.Megjelenites ?? '?');
                    }
                    catch (NotSupportedException)
                    {
                        Console.Write('X'); // ItalianAnimal esetén
                    }
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
            Console.WriteLine($"Összesen {totalCount} brainrot található a területen.");
        }

        // 4.Feladat
        public void DisplayGroupByName()
        {
            var allBrainrots = GetAllBrainrotsFromMatrix();
            var groupedByName = allBrainrots
                .Where(b => b != null)
                .GroupBy(b => b.Nev)
                .Select(g => new { Name = g.Key, Count = g.Count() });

            Console.WriteLine("Csoportosítás név szerint:");
            foreach (var group in groupedByName)
            {
                Console.WriteLine($"  {group.Name}: {group.Count} db");
            }
            Console.WriteLine();
        }

        // 4.Feladat
        public void DisplayGroupByType()
        {
            var allBrainrots = GetAllBrainrotsFromMatrix();
            var groupedByType = allBrainrots
                .Where(b => b != null)
                .GroupBy(b => b.GetType().Name)
                .Select(g => new { Type = g.Key, Count = g.Count() });

            Console.WriteLine("Csoportosítás típus szerint:");
            foreach (var group in groupedByType)
            {
                Console.WriteLine($"  {group.Type}: {group.Count} db");
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

                Console.WriteLine($"Legtöbb: {mostCommon.Type} ({mostCommon.Count} db)");
                Console.WriteLine($"Legkevesebb: {leastCommon.Type} ({leastCommon.Count} db)");
            }
            Console.WriteLine();
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