namespace solticsongor_Lift
{
    internal class Lift
    {
        public static readonly Random random = new Random();
        public readonly int maxEmelet;
        public int aktualisEmelet { get; set; }

        public Lift(int emeletekSzama)
        {
            this.maxEmelet = emeletekSzama;
            this.aktualisEmelet = random.Next(1, emeletekSzama + 1);
        }

        public void Lefele()
        {
            if (random.Next(1, 101) == 1)
            {
                throw new Exception("A lift elromlott! Bocsi.");
            }

            if (aktualisEmelet == 1)
            {
                throw new Exception("A lift már így is a földszinten van...");
            }
            aktualisEmelet--;
        }

        public void Felfele()
        {
            if (random.Next(1, 101) == 1)
            {
                throw new Exception("A lift elromlott! Bocsi.");
            }

            if (aktualisEmelet == maxEmelet)
            {
                throw new Exception($"A lift már így is a {maxEmelet}. emeleten van.");
            }
                aktualisEmelet++;
            }
            
        public override string ToString()
        {
            return $"A lift a(z) {aktualisEmelet}. emeleten van.";
        }
    }
}