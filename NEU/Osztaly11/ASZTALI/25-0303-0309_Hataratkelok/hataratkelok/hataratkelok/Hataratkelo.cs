using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hataratkelok
{
    public class Hataratkelo
    {
        public string Nev { get; set; }
        public string Tipus { get; set; }
        public string Megye { get; set; }
        public string Szomszedtelepules { get; set; }
        public string Orszag { get; set; }
        public string AtkeloTipus { get; set; }

        public Hataratkelo()
        {

        }

        public Hataratkelo(string nev, string tipus, string megye, string szomszedtelepules, string orszag, string atkelotipus)
        {
            Nev = nev;
            Tipus = tipus;
            Megye = megye;
            Szomszedtelepules = szomszedtelepules;
            Orszag = orszag;
            AtkeloTipus = atkelotipus;
        }
    }
}
