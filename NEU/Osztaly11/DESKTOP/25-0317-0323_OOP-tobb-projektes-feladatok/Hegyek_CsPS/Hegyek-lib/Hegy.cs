using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegyek_lib
{
    public class Hegy
    {
        public string Nev { get; init; }
        public string Hegyseg { get; init; }
        public double Magassag { get; init; }

        public Hegy(string sor)
        {
            string[] parts = sor.Split(';');
            Nev = parts[0];
            Hegyseg = parts[1];
            Magassag = double.Parse(parts[2]);
        }
        public double MagassagFoot => Magassag * 3.280839895;
    }
}