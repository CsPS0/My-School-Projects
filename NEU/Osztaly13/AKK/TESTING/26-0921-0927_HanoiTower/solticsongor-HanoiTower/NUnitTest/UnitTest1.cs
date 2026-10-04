using hanoiLib;

namespace NUnitTest;

public class HanoiGameTests
{
    [Test]
    public void Constructor_InitializesDisksOnFirstPeg()
    {
        var game = new HanoiGame(3);

        Assert.That(game.Pegs[0].ToArray(), Is.EqualTo(new[] { 1, 2, 3 }), "A forrásrúd nem tartalmazza a várt értéket.");
        Assert.That(game.Pegs[1], Is.Empty, "A célrúd nem marad üres.");
        Assert.That(game.Pegs[2], Is.Empty, "A célrúd nem marad üres.");
    }

    [Test]
    public void Move_TransfersDiskCorrectly()
    {
        var game = new HanoiGame(3);

        game.Move(0, 1);

        Assert.That(game.Pegs[0].Peek(), Is.EqualTo(2), "A forrásrúd nem tartalmazza a várt értéket.");
        Assert.That(game.Pegs[1].Peek(), Is.EqualTo(1), "A célrúd nem tartalmazza a várt értéket.");
    }

    [Test]
    public void Move_ThrowsException_WhenPlacingBiggerDiskOnSmaller()
    {
        var game = new HanoiGame(3);
        game.Move(0, 1);

        Assert.That(() => game.Move(0, 1), Throws.InvalidOperationException, "Nagyobb korongot nem tehetsz kisebbre.");
    }

    [Test]
    public void Move_ThrowsException_WhenSourcePegIsEmpty()
    {
        var game = new HanoiGame(3);

        Assert.That(() => game.Move(1, 2), Throws.InvalidOperationException, "Az forrásrúd üres.");
    }

    [Test]
    public void IsSolved_ReturnsTrue_WhenAllDisksReachLastPeg()
    {
        var game = new HanoiGame(3);

        game.Move(0, 2);
        game.Move(0, 1);
        game.Move(2, 1);
        game.Move(0, 2);
        game.Move(1, 0);
        game.Move(1, 2);
        game.Move(0, 2);

        Assert.That(game.IsSolved(), Is.True, "A játék nem lett megoldva.");
    }

    [Test]
    public void IsSolved_ReturnsFalse_InitiallyAndPartway()
    {
        var game = new HanoiGame(3);

        Assert.That(game.IsSolved(), Is.False, "Kezdetben a játék nem lehet megoldva.");

        game.Move(0, 2);

        Assert.That(game.IsSolved(), Is.False, "Egy korong áthelyezése után sem lehet megoldva.");
    }

    [Test]
    public void Constructor_ThrowsException_WhenDiskCountIsLessThanOne()
    {
        Assert.That(() => new HanoiGame(0), Throws.ArgumentException, "0 korongal nem hozható létre játék.");
    }

    [Test]
    public void Constructor_ThrowsException_WhenPegCountIsLessThanThree()
    {
        Assert.That(() => new HanoiGame(3, 2), Throws.ArgumentException, "2 rúddal nem hozható létre játék.");
    }

    [Test]
    public void Constructor_SupportsMoreThanThreePegs()
    {
        var game = new HanoiGame(3, 5);

        Assert.That(game.Pegs.Count, Is.EqualTo(5), "A rudak száma nem az elvárt paraméterezett érték.");
        Assert.That(game.Pegs[0].ToArray(), Is.EqualTo(new[] { 1, 2, 3 }), "A forrásrúd nem tartalmazza a várt értéket.");
    }

    [Test]
    public void Move_AllowsPlacingDiskOntoEmptyPeg()
    {
        var game = new HanoiGame(3);

        game.Move(0, 2);

        Assert.That(game.Pegs[2].Peek(), Is.EqualTo(1), "Üres rúdra nem sikerült korongot helyezni.");
    }

    [Test]
    public void Move_ThrowsException_WhenSourceAndTargetPegAreTheSame()
    {
        var game = new HanoiGame(3);

        Assert.That(() => game.Move(0, 0), Throws.InvalidOperationException, "Ugyanarra a rúdra nem lehet mozgatni.");
    }

    [Test]
    public void Move_ThrowsException_WhenPegIndexIsOutOfRange()
    {
        var game = new HanoiGame(3);

        Assert.That(() => game.Move(-1, 0), Throws.InvalidOperationException, "Negatív rúdindex nem engedélyezett.");
        Assert.That(() => game.Move(0, 3), Throws.InvalidOperationException, "Tartományon kívüli rúdindex nem engedélyezett.");
    }

    [Test]
    public void Move_CanMoveDiskBackToPreviousPeg()
    {
        var game = new HanoiGame(3);

        game.Move(0, 1);
        game.Move(1, 0);

        Assert.That(game.Pegs[0].ToArray(), Is.EqualTo(new[] { 1, 2, 3 }), "A korong nem került vissza a kiinduló rúdra.");
        Assert.That(game.Pegs[1], Is.Empty, "A köztes rúdnak üresnek kellene lennie.");
    }

    [Test]
    public void IsSolved_ReturnsTrue_ForFourDisks_AfterOptimalRecursiveSolution()
    {
        const int diskCount = 4;
        var game = new HanoiGame(diskCount);
        int moveCount = 0;

        void Solve(int n, int from, int to, int aux)
        {
            if (n == 0)
            {
                return;
            }

            Solve(n - 1, from, aux, to);
            game.Move(from, to);
            moveCount++;
            Solve(n - 1, aux, to, from);
        }

        Solve(diskCount, 0, 2, 1);

        Assert.That(moveCount, Is.EqualTo((1 << diskCount) - 1), "A lépésszám nem az optimális 2^n - 1.");
        Assert.That(game.IsSolved(), Is.True, "A játék négy koronggal sem lett megoldva.");
    }
}