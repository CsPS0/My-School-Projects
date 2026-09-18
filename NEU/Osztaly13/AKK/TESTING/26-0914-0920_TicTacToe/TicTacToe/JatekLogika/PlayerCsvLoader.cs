using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JatekLogika
{
    /// <summary>
    /// Beolvassa a players.csv fájlt, és a benne szereplő játékos-jelölteket adja vissza.
    /// A fájl első sora fejléc ("id;name"), ezt a betöltő átugorja.
    /// </summary>
    public static class PlayerCsvLoader
    {
        public static List<PlayerCandidate> LoadCandidates(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"A megadott fájl nem található: '{filePath}'.", filePath);
            }

            string[] lines = File.ReadAllLines(filePath);

            return lines
                .Skip(1) // fejléc sor kihagyása
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(ParseLine)
                .ToList();
        }

        private static PlayerCandidate ParseLine(string line)
        {
            string[] fields = line.Split(';');
            int id = int.Parse(fields[0]);
            string name = fields[1].Trim();
            return new PlayerCandidate(id, name);
        }
    }
}
