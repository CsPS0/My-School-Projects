namespace ijaszLib
{
    public class Ijasz
    {
        readonly int constTP = 10;
        private int alloKepesseg;
        private int ugyesseg;
        private int tapasztalat;
        private static Random random = new Random();

        public int Szint { get; private set; }
        public int AlloKepesseg
        {
            get
            {
                return alloKepesseg;
            }
            set
            {
                if (value < 0)
                {
                    alloKepesseg = 0;
                }
                else if (value > 100)
                {
                    alloKepesseg = 100;
                }
                else
                {
                    alloKepesseg = value;
                }
            }
        }
        public int Ugyesseg
        {
            get
            {
                return ugyesseg;
            }
            set
            {
                if (value < 1)
                {
                    ugyesseg = 1;
                }
                else if (value > 10)
                {
                    ugyesseg = 10;
                }
                else
                {
                    ugyesseg = value;
                }
            }
        }
        public bool Akaratero
        {
            get
            {
                return random.Next(0, 10) < 1;
            }
        }
        public Ijasz()
        {
            AlloKepesseg = random.Next(50, 76);
            Ugyesseg = 1;
            Szint = 1;
            tapasztalat = 0;
        }
        public Ijasz(int ugyesseg)
        {
            AlloKepesseg = random.Next(50, 76);
            Ugyesseg = ugyesseg;
            Szint = 1;
            tapasztalat = 0;
        }
        public bool Lo()
        {
            if (AlloKepesseg > 5 || Akaratero)
            {
                this.AlloKepesseg -= 5;
                this.tapasztalat += Szint * Ugyesseg;

                if (tapasztalat > constTP * Szint * Szint)
                {
                    Szint++;
                    tapasztalat = 0;
                    if (Szint % 3 == 0)
                    {
                        Ugyesseg++;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Pihen()
        {
            AlloKepesseg += random.Next(1, 11);
        }
        public string Info()
        {
            return $"Szint: {Szint}, Ügyesség: {Ugyesseg}, Állóképesség: {AlloKepesseg}";
        }
    }
}