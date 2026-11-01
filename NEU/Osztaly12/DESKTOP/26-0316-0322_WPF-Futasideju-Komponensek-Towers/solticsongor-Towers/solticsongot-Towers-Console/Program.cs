namespace solticsongot_Towers_Console;

class Program
{
    static void Main(string[] args)
    {
        Feladat f = new Feladat("feladat.txt");
        List<Megoldas> megoldasok = new List<Megoldas>();
        string[] mSorok = File.ReadAllLines("megoldasok.txt");

        for (int i = 0; i < mSorok.Length; i += f.N + 1)
        {
            if (string.IsNullOrWhiteSpace(mSorok[i])) continue;
            string nev = mSorok[i];
            int[,] t = new int[f.N, f.N];
            for (int r = 0; r < f.N; r++)
            {
                int[] v = mSorok[i + 1 + r].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for (int c = 0; c < f.N; c++) t[r, c] = v[c];
            }
            megoldasok.Add(new Megoldas(nev, t));
        }

        Console.WriteLine($"5. feladat: A megoldást beküldők száma: {megoldasok.Count}");

        Console.Write("8. feladat: A beküldő neve: ");
        string keresettNev = Console.ReadLine() ?? "";
        Megoldas? m = megoldasok.FirstOrDefault(x => x.Nev == keresettNev);
        if (m != null)
        {
            Console.WriteLine($"{m.Nev} megoldása:");
            int[] felso = m.Felso();
            int[] also = m.Also();
            int[] bal = m.Bal();
            int[] jobb = m.Jobb();
            
            Console.WriteLine("     " + string.Join("  ", felso));
            for (int r = 0; r < f.N; r++)
            {
                Console.Write($"{bal[r]}    ");
                for (int c = 0; c < f.N; c++) Console.Write($"{m.Tabla[r, c]}  ");
                Console.WriteLine($"{jobb[r]}");
            }
            Console.WriteLine("     " + string.Join("  ", also));
        }
        else
        {
            Console.WriteLine("Nincs ilyen néven beküldött megoldás!");
        }

        var helyesek = megoldasok.Where(x => x.Ellenorzes() && 
            Enumerable.SequenceEqual(x.Felso(), f.Felul) &&
            Enumerable.SequenceEqual(x.Also(), f.Alul) &&
            Enumerable.SequenceEqual(x.Bal(), f.Bal) &&
            Enumerable.SequenceEqual(x.Jobb(), f.Jobb)).Select(x => x.Nev);
        
        Console.WriteLine($"9. feladat: A feladványra helyes megoldást adtak: {string.Join(", ", helyesek)}");
    }
}
