namespace sudokuCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Feladvany> feladvanyok = new List<Feladvany>();
            string filePath = "feladvanyok.txt";

            if (!File.Exists(filePath))
            {
                string currentDir = Directory.GetCurrentDirectory();
                for (int i = 0; i < 4; i++)
                {
                    string p = Path.Combine(currentDir, filePath);
                    if (File.Exists(p))
                    {
                        filePath = p;
                        break;
                    }
                    var parent = Directory.GetParent(currentDir);
                    if (parent == null) break;
                    currentDir = parent.FullName;
                }
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Hiba: A feladvanyok.txt fájl nem található!");
                return;
            }

            string[] sorok = File.ReadAllLines(filePath);
            foreach (var sor in sorok)
            {
                if (!string.IsNullOrWhiteSpace(sor))
                {
                    feladvanyok.Add(new Feladvany(sor.Trim()));
                }
            }

            Console.WriteLine($"3. feladat: Beolvasva {feladvanyok.Count} feladvány");

            int meret = 0;
            do
            {
                Console.Write("4. feladat: Kérem a feladvány méretét [4..9]: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int m) && m >= 4 && m <= 9)
                {
                    meret = m;
                }
            } while (meret == 0);

            List<Feladvany> meretSzerinti = feladvanyok.Where(f => f.Meret == meret).ToList();
            Console.WriteLine($"{meret}x{meret} méretű feladványból {meretSzerinti.Count} darab van tárolva");

            if (meretSzerinti.Count > 0)
            {
                Random rnd = new Random();
                Feladvany kivalasztott = meretSzerinti[rnd.Next(meretSzerinti.Count)];
                Console.WriteLine($"5. feladat: A kiválasztott feladvány: {kivalasztott.Kezdo}");

                int nemNulla = kivalasztott.Kezdo.Count(c => c != '0');
                int osszes = kivalasztott.Kezdo.Length;
                double arany = (double)nemNulla / osszes * 100;
                Console.WriteLine($"6. feladat: A feladvány kitöltöttsége: {Math.Round(arany)}%");

                Console.WriteLine("7. feladat: A feladvány kirajzolva:");
                kivalasztott.Kirajzol();

                string outputFileName = $"sudoku{meret}.txt";
                try
                {
                    using (StreamWriter sw = new StreamWriter(outputFileName))
                    {
                        foreach (var f in meretSzerinti)
                        {
                            sw.WriteLine(f.Kezdo);
                        }
                    }
                    Console.WriteLine($"8. feladat: {outputFileName} állomány {meretSzerinti.Count} darab feladvánnyal létrehozva");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba a fájl írásakor: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Ebből a méretből nincs feladvány.");
            }
        }
    }

    class Feladvany
    {
        public string Kezdo { get; private set; }
        public int Meret { get; private set; }

        public Feladvany(string sor)
        {
            Kezdo = sor;
            Meret = Convert.ToInt32(Math.Sqrt(sor.Length));
        }

        public void Kirajzol()
        {
            for (int i = 0; i < Kezdo.Length; i++)
            {
                if (Kezdo[i] == '0')
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write(Kezdo[i]);
                }
                if (i % Meret == Meret - 1)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}