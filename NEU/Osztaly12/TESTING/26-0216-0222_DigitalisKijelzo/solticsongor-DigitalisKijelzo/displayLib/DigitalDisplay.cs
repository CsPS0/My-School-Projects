using System.Text;

namespace displayLib;

public static class DigitalDisplay
{
    public static string GetDisplayFromRawInput(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;

        string digitsOnly = new string(input.Where(char.IsDigit).ToArray());

        if (string.IsNullOrEmpty(digitsOnly)) return string.Empty;

        return GetDisplay(digitsOnly);
    }

    private static string GetDisplay(string input)
    {
        string[][] splitDigits = Numbers.Digits.Select(d => d.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)).ToArray();
        
        var sb = new StringBuilder();
        for (int row = 0; row < 10; row++)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int digit = input[i] - '0';
                
                if (row < splitDigits[digit].Length)
                {
                    sb.Append(splitDigits[digit][row]);
                }
                
                if (i < input.Length - 1)
                {
                    sb.Append(" ");
                }
            }
            if (row < 9)
            {
                sb.AppendLine();
            }
        }
        return sb.ToString();
    }
}
