using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace koteszetLib
{
    public class Termek : ITermek
    {
        public int Id { get; }
        public string Tipus { get; set; }
        public string Megnevezes { get; set; }
        public int Ar { get; set; }
        public int ElkeszetesiIdo { get; set; }

        public Dictionary<string, int> SzuksegesAlapanyagok { get; }

        public Termek(string termekAdat, Katalogus alapanyagokKatalogusa)
        {
            var adatok = termekAdat.Split(';');
            Id = int.Parse(adatok[0]);
            Tipus = adatok[1];
            Megnevezes = adatok[2];
            SzuksegesAlapanyagok = new Dictionary<string, int>();

            for (int i = 3; i < adatok.Length; i += 2)
            {
                SzuksegesAlapanyagok.Add(adatok[i], int.Parse(adatok[i + 1]));
            }

            foreach (var alapanyagAzonosito in SzuksegesAlapanyagok.Keys)
            {
                var alapanyag = alapanyagokKatalogusa[alapanyagAzonosito];
                var mennyiseg = SzuksegesAlapanyagok[alapanyagAzonosito];

                Ar += alapanyag.Ar * mennyiseg;
                ElkeszetesiIdo += alapanyag.ElkeszetesiIdo * mennyiseg;
            }
        }

        public override string ToString()
        {
            return $"- {Megnevezes}, elkészítési idő: {ElkeszetesiIdo} perc, ár: {Ar} Ft";
        }
    }
}
