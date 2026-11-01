using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barlangok
{
    public class Barlang
    {
        public string BarlangNev { get; init; }
        public float Hossz { get; init; }
        public float Melyseg { get; init; }
        public float Magassag { get; init; }
        public string TelepulesNev { get; init; }
        public string Voltak { get; init; }

        public Barlang(string barlangnev, float hossz, float melyseg, float magassag, string telepulesnev, string voltak) {
            BarlangNev = barlangnev;
            Hossz = hossz;
            Melyseg = melyseg;
            Magassag = magassag;
            TelepulesNev = telepulesnev;
            Voltak = voltak;
        }
    }
}