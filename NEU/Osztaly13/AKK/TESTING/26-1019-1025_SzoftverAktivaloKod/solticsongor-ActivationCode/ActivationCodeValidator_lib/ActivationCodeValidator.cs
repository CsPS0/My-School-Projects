using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ActivationCodeValidator_lib;

public class ActivatetionCodeValidator_lib
{
    public static bool CheckCodeValidity(string? code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return false;
        }

        string cleanedCode;
        if (code.Contains('-'))
        {
            if (!Regex.IsMatch(code, @"^[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}$"))
            {
                return false;
            }
            cleanedCode = code.Replace("-", "");
        }
        else
        {
            if (!Regex.IsMatch(code, @"^[a-zA-Z0-9]{12}$"))
            {
                return false;
            }
            cleanedCode = code;
        }

        if (cleanedCode.Length != 12)
        {
            return false;
        }

        string upper = cleanedCode.ToUpperInvariant();

        int kCount = upper.Count(c => c == 'K');
        if (kCount < 1)
        {
            return false;
        }

        int aCount = upper.Count(c => c == 'A');
        if (aCount > 3)
        {
            return false;
        }

        int eightCount = upper.Count(c => c == '8');
        if (eightCount != 2 || !upper.Contains("88"))
        {
            return false;
        }

        if (!upper.All(c => (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')))
        {
            return false;
        }

        return true;
    }
}

public class ActivationCodeValidator : ActivatetionCodeValidator_lib
{
}
