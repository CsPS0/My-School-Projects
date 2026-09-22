namespace hanoiLib;

public class HanoiGame
{
    public List<Stack<int>> Pegs { get; } = new();
    public int DiskCount { get; }

    public HanoiGame(int diskCount, int pegCount = 3)
    {
        if (diskCount < 1 || pegCount < 3)
        {
            throw new ArgumentException("Érvénytelen paraméterek.");
        }

        DiskCount = diskCount;

        for (int i = 0; i < pegCount; i++)
        {
            Pegs.Add(new Stack<int>());
        }

        for (int i = diskCount; i >= 1; i--)
        {
            Pegs[0].Push(i);
        }
    }

    public void Move(int from, int to)
    {
        if (from == to || from < 0 || to < 0 || from >= Pegs.Count || to >= Pegs.Count)
        {
            throw new InvalidOperationException("Érvénytelen rúd sorszám.");
        }

        if (Pegs[from].Count == 0)
        {
            throw new InvalidOperationException("Üres rúdról nem lehet mozgatni.");
        }

        if (Pegs[to].Count > 0 && Pegs[to].Peek() < Pegs[from].Peek())
        {
            throw new InvalidOperationException("Nagyobb korongot nem tehetsz kisebbre.");
        }

        Pegs[to].Push(Pegs[from].Pop());
    }

    public bool IsSolved()
    {
        return Pegs[^1].Count == DiskCount;
    }
}