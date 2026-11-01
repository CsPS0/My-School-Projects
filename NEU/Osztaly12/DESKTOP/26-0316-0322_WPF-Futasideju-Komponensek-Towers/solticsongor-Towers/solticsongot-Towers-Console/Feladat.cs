namespace solticsongot_Towers_Console;

public class Feladat
{
    public int N;
    public int[] Felul;
    public int[] Alul;
    public int[] Bal;
    public int[] Jobb;

    public Feladat(string filename)
    {
        string[] sorok = File.ReadAllLines(filename);
        N = int.Parse(sorok[0]);
        Felul = sorok[1].Split(' ', System.StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        Alul = sorok[2].Split(' ', System.StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        Bal = sorok[3].Split(' ', System.StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        Jobb = sorok[4].Split(' ', System.StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    }
}
