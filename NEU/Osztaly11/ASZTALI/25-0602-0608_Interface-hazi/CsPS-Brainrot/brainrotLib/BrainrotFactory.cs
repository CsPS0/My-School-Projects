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
            // Load toilets from toilets.txt
            if (File.Exists("toilets.txt"))
            {
                var toiletLines = File.ReadAllLines("toilets.txt").Skip(1);
                foreach (var line in toiletLines)
                {
                    var parts = line.Split(';');
                    if (parts.Length >= 5)
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
                }
            }

            // Load animals from animals.txt
            if (File.Exists("animals.txt"))
            {
                var animalLines = File.ReadAllLines("animals.txt").Skip(1);
                foreach (var line in animalLines)
                {
                    var parts = line.Split(';');
                    if (parts.Length >= 6)
                    {
                        var datum = DateTime.ParseExact(parts[2], "yyyy.MM.dd", CultureInfo.InvariantCulture);
                        var veszelyesseg = int.Parse(parts[5]);
                        animals.Add(new ItalianAnimal(
                            parts[0],
                            parts[1],
                            datum,
                            parts[4],
                            veszelyesseg
                        ));
                    }
                }
            }

            // Load VigyazMagyar from vmagyar.txt
            if (File.Exists("vmagyar.txt"))
            {
                var magyarLines = File.ReadAllLines("vmagyar.txt").Skip(1);
                foreach (var line in magyarLines)
                {
                    var parts = line.Split(';');
                    if (parts.Length >= 6)
                    {
                        var datum = DateTime.ParseExact(parts[2], "yyyy.MM.dd", CultureInfo.InvariantCulture);
                        var politika = parts[4] == "1";
                        var offenziv = parts[5][0];
                        magyarok.Add(new VigyazMagyar(
                            parts[0],
                            parts[1],
                            datum,
                            parts[3][0],
                            politika,
                            offenziv
                        ));
                    }
                }
            }
        }

        public IBrainrot Create()
        {
            int choice = random.Next(3);
            switch (choice)
            {
                case 0:
                    return toilets.Count > 0 ? toilets[random.Next(toilets.Count)] : null;
                case 1:
                    return animals.Count > 0 ? animals[random.Next(animals.Count)] : null;
                default:
                    return magyarok.Count > 0 ? magyarok[random.Next(magyarok.Count)] : null;
            }
        }
    }
}