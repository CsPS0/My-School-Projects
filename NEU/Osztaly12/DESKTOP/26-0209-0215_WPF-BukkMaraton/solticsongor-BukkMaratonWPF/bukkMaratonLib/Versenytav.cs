namespace bukkMaratonLib
{
    public class Versenytav
    {
        public string Rajtszam { get; private set; }
        public string Tav
        {
            get
            {
                switch (Rajtszam[0])
                {
                    case 'M': return "Mini";
                    case 'R': return "Rövid";
                    case 'K': return "Közép";
                    case 'H': return "Hosszú";
                    case 'E': return "Pedelec";
                    default: return "Hibás rajtszám";
                }
            }
        }

        public Versenytav(string rajtszam)
        {
            Rajtszam = rajtszam;
        }
    }
}
