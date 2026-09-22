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

        Assert.Throws<InvalidOperationException>(() => game.Move(0, 1), "Nagyobb korongot nem tehetsz kisebbre.");
    }

    [Test]
    public void Move_ThrowsException_WhenSourcePegIsEmpty()
    {
        var game = new HanoiGame(3);

        Assert.Throws<InvalidOperationException>(() => game.Move(1, 2), "Az forrásrúd üres.");
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
}