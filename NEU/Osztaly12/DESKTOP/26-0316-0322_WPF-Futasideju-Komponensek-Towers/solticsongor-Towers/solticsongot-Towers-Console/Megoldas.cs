namespace solticsongot_Towers_Console;

public class Megoldas
{
    public string Nev;
    public int[,] Tabla;
    public int N;

    public Megoldas(string nev, int[,] tabla)
    {
        Nev = nev;
        Tabla = tabla;
        N = tabla.GetLength(0);
    }

    public int[] Felso() => Enumerable.Range(0, N).Select(c => CountVisible(Enumerable.Range(0, N).Select(r => Tabla[r, c]))).ToArray();
    public int[] Also() => Enumerable.Range(0, N).Select(c => CountVisible(Enumerable.Range(0, N).Reverse().Select(r => Tabla[r, c]))).ToArray();
    public int[] Bal() => Enumerable.Range(0, N).Select(r => CountVisible(Enumerable.Range(0, N).Select(c => Tabla[r, c]))).ToArray();
    public int[] Jobb() => Enumerable.Range(0, N).Select(r => CountVisible(Enumerable.Range(0, N).Reverse().Select(c => Tabla[r, c]))).ToArray();

    private int CountVisible(IEnumerable<int> line)
    {
        int count = 0, max = 0;
        foreach (int v in line) { if (v > max) { count++; max = v; } }
        return count;
    }

    public bool Ellenorzes()
    {
        for (int r = 0; r < N; r++)
        {
            HashSet<int> seen = new HashSet<int>();
            for (int c = 0; c < N; c++)
            {
                if (Tabla[r, c] < 1 || Tabla[r, c] > N || !seen.Add(Tabla[r, c])) return false;
            }
        }
        for (int c = 0; c < N; c++)
        {
            HashSet<int> seen = new HashSet<int>();
            for (int r = 0; r < N; r++)
            {
                if (!seen.Add(Tabla[r, c])) return false;
            }
        }
        return true;
    }
}
