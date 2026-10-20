using System.Globalization;

namespace xmasFairLib;

public class Fair
{
    private readonly List<Stand> _stands = [];
    private readonly int _maxStands;
    private readonly int _standPrice;

    public string Name { get; }
    public int FreePlace => _maxStands - _stands.Count;
    public int Income => _stands.Count * _standPrice;

    public Fair(string name, int maxStands, int standPrice, string startDate, string endDate)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("A vásár neve nem lehet üres.", nameof(name));
        }

        if (maxStands < 0)
        {
            throw new ArgumentException("Negatív számú stand nem lehet a vásárban.", nameof(maxStands));
        }

        if (standPrice < 0)
        {
            throw new ArgumentException("Negatív összeget nem kérhetnek a standokért.", nameof(standPrice));
        }

        DateTime start = ParseDate(startDate);
        DateTime end = ParseDate(endDate);

        if (end < start)
        {
            throw new ArgumentException("A végdátum nem lehet kisebb, mint a kezdő dátum.");
        }

        Name = name;
        _maxStands = maxStands;
        _standPrice = standPrice;
    }

    public bool AddStand(Stand? stand)
    {
        if (stand == null)
        {
            return false;
        }

        if (FreePlace <= 0)
        {
            return false;
        }

        if (_stands.Any(s => s.Name == stand.Name))
        {
            return false;
        }

        _stands.Add(stand);
        return true;
    }

    public bool RemoveStand(string? standName)
    {
        if (string.IsNullOrEmpty(standName))
        {
            return false;
        }

        Stand? stand = _stands.FirstOrDefault(s => s.Name == standName);
        if (stand == null)
        {
            return false;
        }

        return _stands.Remove(stand);
    }

    private static DateTime ParseDate(string? dateStr)
    {
        if (string.IsNullOrWhiteSpace(dateStr))
        {
            throw new ArgumentException("A dátum nem lehet üres.", nameof(dateStr));
        }

        string[] formats = ["yyyy.MM.dd", "yyyy.MM.dd.", "yyyy-MM-dd", "yyyy/MM/dd", "yyyy. M. d.", "yyyy. M. d"];
        if (DateTime.TryParseExact(dateStr.Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
        {
            return dt;
        }

        if (DateTime.TryParse(dateStr, CultureInfo.GetCultureInfo("hu-HU"), DateTimeStyles.None, out dt))
        {
            return dt;
        }

        if (DateTime.TryParse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
        {
            return dt;
        }

        throw new ArgumentException($"Érvénytelen dátumformátum: {dateStr}", nameof(dateStr));
    }
}