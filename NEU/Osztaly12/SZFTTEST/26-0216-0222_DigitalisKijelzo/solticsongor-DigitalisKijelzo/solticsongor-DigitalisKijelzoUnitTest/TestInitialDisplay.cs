using displayLib;
using System.Text;

namespace solticsongor_DigitalisKijelzoUnitTest;

[TestFixture]
public class TestInitialDisplay
{
    private string GetExpectedForSingleDigit(int digit)
    {
        string rawArt = Numbers.Digits[digit];
        string[] lines = rawArt.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 10; i++)
        {
            sb.Append(lines[i]);
            if (i < 9) sb.AppendLine();
        }
        return sb.ToString();
    }

    [Test]
    public void TestEveryDigitArt([Range(0, 9)] int digit)
    {
        string result = DigitalDisplay.GetDisplayFromRawInput(digit.ToString());
        string expected = GetExpectedForSingleDigit(digit);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void InvalidCharactersAreIgnored()
    {
        string result = DigitalDisplay.GetDisplayFromRawInput("apple");

        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void InvalidCharactersAreIgnoredWhenNumbersAlsoPresent()
    {
        string input = "app4le";
        string expected = GetExpectedForSingleDigit(4);

        string result = DigitalDisplay.GetDisplayFromRawInput(input);

        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void MixedInputExtractionTest()
    {
        string input = "a1b2";
        string result = DigitalDisplay.GetDisplayFromRawInput(input);
        
        Assert.That(result, Contains.Substring("▐   /$$  ▌ ▐  /$$$$$$ ▌"));
    }
}
