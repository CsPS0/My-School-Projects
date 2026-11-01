using solticsongor_SzokoEvConsole;

namespace solticsongor_SzokoEvTest;

public class Tests
{
    [Test]
    public void IsLeapYear_When2021_ReturnsFalse()
    {
        var converter = new LeapYearConverter();
        var result = converter.IsLeapYear(2021);
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsLeapYear_When2024_ReturnsTrue()
    {
        var converter = new LeapYearConverter();
        var result = converter.IsLeapYear(2024);
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsLeapYear_When1900_ReturnsFalse()
    {
        var converter = new LeapYearConverter();
        var result = converter.IsLeapYear(1900);
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsLeapYear_When2000_ReturnsTrue()
    {
        var converter = new LeapYearConverter();
        var result = converter.IsLeapYear(2000);
        Assert.That(result, Is.True);
    }
}
