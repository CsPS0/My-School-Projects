using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epuletek
{
    internal class Epulet
    {
        public string Nev { get; init; }
        public string Varos { get; init; }
        public string Orszag { get; init; }
        public float Magassag { get; init; }
        public int Emelet { get; init; }
        public int Epult { get; init; }

        public Epulet(string nev, string varos, string orszag, float magassag, int emelet, int epult)
        {
            Nev = nev;
            Varos = varos;
            Orszag = orszag;
            Magassag = magassag;
            this.Emelet = emelet;
            this.Epult = epult;
        }
    }
}
