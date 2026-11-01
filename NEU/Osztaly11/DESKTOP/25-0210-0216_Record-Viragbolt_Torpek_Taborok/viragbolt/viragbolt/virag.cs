using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace viragbolt
{
    public record Virag(string Name, int Price)
    {
        public string Kiiras1 => $"{Name} ({Price} Ft)";

        public static string AtlagAr(List<Virag> viragok)
        {
            double atlag = viragok.Sum(v => v.Price) / (double)viragok.Count;
            return $"Átlagosan {atlag:F2} Ft-ba kerül egy szál virág.";
        }

        public static Virag? Legdragabb(List<Virag> viragok)
        {
            if (viragok.Count == 0)
            {
                return null;
            }

            Virag legdragabb = viragok[0];

            foreach (var virag in viragok)
            {
                if (virag.Price > legdragabb.Price)
                {
                    legdragabb = virag;
                }
            }

            return legdragabb;
        }

        public static string LegalabbEzerForintos(List<Virag> viragok)
        {
            int db = viragok.Count(v => v.Price >= 1000);
            return $"Legalább 1000 Ft-os virágok száma: {db}";
        }

        public static string KeresVirag(List<Virag> viragok, string nev)
        {
            foreach (var v in viragok)
            {
                if (v.Name.Equals(nev, StringComparison.OrdinalIgnoreCase))
                {
                    return $"{v.Name} ({v.Price} Ft)";
                }
            }
            return "Ez a virág nincs az üzletben.";
        }
    }
}