using solticsongor_FizzBuzz_ConsoleApp;

namespace solticsongor_FizzBuzz_UnitTest;

public class Tests
{
    [Test]
    public void Convert_When1_Returns1String()
    {
        var converter = new FizzBuzzConverter();
        
        var result = converter.Convert(1);
        
        Assert.That(result, Is.EqualTo("1"));
    }

    [Test]
    public void Convert_When3_ReturnsFizz()
    {
        var converter = new FizzBuzzConverter();
        
        var result = converter.Convert(3);
        
        Assert.That(result, Is.EqualTo("Fizz"));
    }

    [Test]
    public void Convert_When5_ReturnsBuzz()
    {
        var converter = new FizzBuzzConverter();
        
        var result = converter.Convert(5);
        
        Assert.That(result, Is.EqualTo("Buzz"));
    }

    [Test]
    public void Convert_When15_ReturnsFizzBuzz()
    {
        var converter = new FizzBuzzConverter();
        
        var result = converter.Convert(15);
        
        Assert.That(result, Is.EqualTo("FizzBuzz"));
    }
}
