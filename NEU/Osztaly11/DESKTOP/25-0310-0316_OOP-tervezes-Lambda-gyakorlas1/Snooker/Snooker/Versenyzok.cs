using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snooker
{
    internal class Versenyzok
    {
        public int Helyezes { get; init; }
        public string Nev { get; init; }
        public string Orszag { get; init; }
        public int Nyeremeny { get; init; }

        public Versenyzok ()
        {

        }

        public Versenyzok(int helyezes, string nev, string orszag, int nyeremeny)
        {
            Helyezes = helyezes;
            Nev = nev;
            Orszag = orszag;
            Nyeremeny = nyeremeny;
        }
    }
}