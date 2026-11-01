using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torpek
{
    public class Torpe
    {
        public string Nev { get; set; }
        public string Klan { get; set; }
        public char Nem { get; set; }
        public int Suly { get; set; }
        public int Magassag { get; set; }

        public Torpe(string nev, string klan, char nem, int suly, int magassag)
        {
            Nev = nev;
            Klan = klan;
            Nem = nem;
            Suly = suly;
            Magassag = magassag;
        }

        public double TTI()
        {
            double magassagMeterben = Magassag / 100.0;
            return Suly / (magassagMeterben * magassagMeterben);
        }
    }
}