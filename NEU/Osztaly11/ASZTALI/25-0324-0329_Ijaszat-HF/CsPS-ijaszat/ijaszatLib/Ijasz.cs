namespace ijaszatLib
{
    public class Ijasz
    {
        private const int TP = 10;
        private int tapasztalat;
        private int alloKepesseg;
        private int ugyesseg;

        public int Szint { get; private set; }

        public int AlloKepesseg
        {
            get => alloKepesseg;
            set => alloKepesseg = Math.Clamp(value, 0, 100);
        }

        public int Ugyesseg
        {
            get => ugyesseg;
            set => ugyesseg = Math.Clamp(value, 1, 10);
        }

        public bool AkaratEro
        {
            get
            {
                Random rnd = new Random();
                return rnd.Next(0, 100) < 10;
            }
        }

        public Ijasz()
        {
            Random rnd = new Random();
            AlloKepesseg = rnd.Next(30, 61);
            Ugyesseg = 1;
            Szint = 1;
            tapasztalat = 0;
        }

        public Ijasz(int ugyesseg)
        {
            Random rnd = new Random();
            AlloKepesseg = rnd.Next(50, 76);
            Ugyesseg = ugyesseg;
            Szint = 1;
            tapasztalat = 0;
        }

        public bool Lo()
        {
            if (AlloKepesseg < 5 && !AkaratEro)
            {
                return false;
            }

            AlloKepesseg -= 5;
            tapasztalat += Szint * Ugyesseg;

            if (tapasztalat > TP * Szint * Szint)
            {
                Szint++;
                tapasztalat = 0;
                if (Szint % 3 == 0 && Ugyesseg < 10)
                {
                    Ugyesseg++;
                }
            }

            return true;
        }

        public void Pihen()
        {
            Random rnd = new Random();
            AlloKepesseg = Math.Min(100, AlloKepesseg + rnd.Next(1, 11));
        }

        public string Info()
        {
            return $"Szint: {Szint}, Ügyesség: {Ugyesseg}, Állóképesség: {AlloKepesseg}";
        }
    }
}