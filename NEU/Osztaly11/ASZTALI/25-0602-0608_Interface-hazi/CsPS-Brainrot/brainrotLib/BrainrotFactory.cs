using System.Globalization;

namespace brainrotLib
{
    public class BrainrotFactory
    {
        private readonly Random random = new();
        private readonly List<SkibidiToilet> toilets = new();
        private readonly List<ItalianAnimal> animals = new();
        private readonly List<VigyazMagyar> magyarok = new();

        public BrainrotFactory()
        {
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists("toilets.txt"))
            {
                var toiletLines = File.ReadAllLines("toilets.txt").Skip(1);
                foreach (var line in toiletLines)
                {
                    var parts = line.Split(';');
                    if (parts.Length >= 5)
                    {
                        try
                        {
                            var datum = DateTime.ParseExact(parts[2], "yyyy.MM.dd", CultureInfo.InvariantCulture);
                            toilets.Add(new SkibidiToilet(
                                parts[0],
                                parts[1],
                                datum,
                                parts[3][0],
                                parts[4]
                            ));
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Warning: Could not parse date in line: {line}");
                            continue;
                        }
                    }
                }
            }

            if (File.Exists("animals.txt"))
            {
                var animalLines = File.ReadAllLines("animals.txt").Skip(1);
                foreach (var line in animalLines)
                {
                    var parts = line.Split(';');
                    if (parts.Length >= 6)
                    {
                        try
                        {
                            var datum = DateTime.ParseExact(parts[2], "yyyy.MM.dd", CultureInfo.InvariantCulture);
                            animals.Add(new ItalianAnimal(
                                parts[0],
                                parts[1],
                                datum,
                                parts[4],
                                int.Parse(parts[5])
                            ));
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Warning: Could not parse date or number in line: {line}");
                            continue;
                        }
                    }
                }
            }

            if (File.Exists("vmagyar.txt"))
            {
                var magyarLines = File.ReadAllLines("vmagyar.txt").Skip(1);
                foreach (var line in magyarLines)
                {
                    var parts = line.Split(';');
                    if (parts.Length >= 6)
                    {
                        try
                        {
                            var datum = DateTime.ParseExact(parts[2], "yyyy.MM.dd", CultureInfo.InvariantCulture);
                            var politikaE = parts[4] == "1";
                            magyarok.Add(new VigyazMagyar(
                                parts[0],
                                parts[1],
                                datum,
                                parts[3][0],
                                politikaE,
                                parts[5][0]
                            ));
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Warning: Could not parse date in line: {line}");
                            continue;
                        }
                    }
                }
            }
        }

        public IBrainrot Create()
        {
            var availableTypes = new List<(List<IBrainrot> list, string name)>();
            
            if (toilets.Count > 0)
                availableTypes.Add((toilets.Cast<IBrainrot>().ToList(), "toilet"));
            if (animals.Count > 0)
                availableTypes.Add((animals.Cast<IBrainrot>().ToList(), "animal"));
            if (magyarok.Count > 0)
                availableTypes.Add((magyarok.Cast<IBrainrot>().ToList(), "magyar"));

            if (availableTypes.Count == 0)
                return null;

            var selectedType = availableTypes[random.Next(availableTypes.Count)];
            return selectedType.list[random.Next(selectedType.list.Count)];
        }
    }
}