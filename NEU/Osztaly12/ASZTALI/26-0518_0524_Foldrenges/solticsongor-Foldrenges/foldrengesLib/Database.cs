using System.Globalization;
using System.Text;

namespace foldrengesLib;

public class Database
{
    public List<Naplo> NaploList { get; set; } = new();
    public List<Telepules> TelepulesList { get; set; } = new();

    public void LoadData(string naploPath, string telepulesPath)
    {
        var culture = new CultureInfo("hu-HU");

        var telepulesLines = File.ReadAllLines(telepulesPath, Encoding.UTF8).Skip(1);
        foreach (var line in telepulesLines)
        {
            var parts = line.Split('\t');
            TelepulesList.Add(new Telepules
            {
                Id = int.Parse(parts[0]),
                Nev = parts[1],
                Varmegye = parts[2]
            });
        }

        var naploLines = File.ReadAllLines(naploPath, Encoding.UTF8).Skip(1);
        foreach (var line in naploLines)
        {
            var parts = line.Split('\t');
            NaploList.Add(new Naplo
            {
                Id = int.Parse(parts[0]),
                Datum = DateTime.ParseExact(parts[1], "yyyy.MM.dd", CultureInfo.InvariantCulture),
                Ido = TimeSpan.Parse(parts[2]),
                TelepId = int.Parse(parts[3]),
                Magnitudo = string.IsNullOrWhiteSpace(parts[4]) ? null : double.Parse(parts[4], culture),
                Intenzitas = double.Parse(parts[5], culture)
            });
        }
    }
}
