namespace brainrotLib
{
    public class BrainrotFactory
    {
        readonly Random random = new();

        private readonly List<SkibidiToilet> toilets = new()
        {

        };

        private readonly List<ItalianAnimal> animals = new()
        {

        };

        private readonly List<VigyazMagyar> magyarok = new()
        {

        };

        public IBrainrot Create()
        {
            int ismertseg = random.Next(3);

            switch (ismertseg)
            {
                case 0: return toilets[random.Next(toilets.Count)];
                case 1: return animals[random.Next(animals.Count)];
                default: return magyarok[random.Next(magyarok.Count)];
            }
        }
    }
}