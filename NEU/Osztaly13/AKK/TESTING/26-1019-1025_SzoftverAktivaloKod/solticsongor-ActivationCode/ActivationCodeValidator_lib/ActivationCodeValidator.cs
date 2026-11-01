using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ActivationCodeValidator_lib;

/// <summary>
/// Szoftver aktiváló kód ellenőrző osztály (TDD szerint készítve).
/// A feladat leírása alapján a specifikáció:
/// - 3 db 4 jegyű blokk kötőjelekkel elválasztva (XXXX-XXXX-XXXX) vagy kötőjelek nélkül (12 karakter)
/// - Kis- és nagybetűs alakban is érvényes
/// - Legalább 1 'K' és legfeljebb 3 db 'A' betűt tartalmaz
/// - Pontosan két darab 8-as számjegyet tartalmaz, egymás mellett ("88")
/// - Csak az angol ábécé betűit (A-Z) és számokat (0-9) tartalmazhat
/// </summary>
public class ActivatetionCodeValidator_lib
{
    public static bool CheckCodeValidity(string? code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return false;
        }

        // 1. Formátum és hossz ellenőrzése (kötőjeles vagy kötőjel nélküli)
        string cleanedCode;
        if (code.Contains('-'))
        {
            // Kötőjeles formátum: pontosan XXXX-XXXX-XXXX
            // 3 db 4 jegyű blokk kötőjellel
            if (!Regex.IsMatch(code, @"^[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}$"))
            {
                return false;
            }
            cleanedCode = code.Replace("-", "");
        }
        else
        {
            // Kötőjel nélküli formátum: pontosan 12 alfanumerikus karakter
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

        // 2. Legalább 1 'K' betű
        int kCount = upper.Count(c => c == 'K');
        if (kCount < 1)
        {
            return false;
        }

        // 3. Legfeljebb 3 db 'A' betű
        int aCount = upper.Count(c => c == 'A');
        if (aCount > 3)
        {
            return false;
        }

        // 4. Pontosan két darab 8-as számjegy egymás mellett (88)
        int eightCount = upper.Count(c => c == '8');
        if (eightCount != 2 || !upper.Contains("88"))
        {
            return false;
        }

        // 5. Csak az angol abc betűit és számokat tartalmazhatja
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
