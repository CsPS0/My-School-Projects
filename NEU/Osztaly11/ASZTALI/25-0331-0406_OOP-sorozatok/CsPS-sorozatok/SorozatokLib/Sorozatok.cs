using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SorozatokLib
{
    public class Sorozatok
    {
        readonly List<Sorozat> sorozatok = new();

        public Sorozatok(IEnumerable<string> sor)
        {
            foreach (var i in sor)
            {
                sorozatok.Add(new Sorozat(i));
            }
        }
        public int SorozatokSzama => sorozatok.Count;
        public int EgyRendezo => sorozatok.Count(x => x.Rendezok() == 1);
        public int KettoRendezo => sorozatok.Count(x => x.Rendezok() == 2);
        public string EgyVagyKetto()
        {
            if (EgyRendezo > KettoRendezo)
            {
                return "Az egy rendezős sorozatok száma több mint a két rendezős sorozatok száma.";
            }
            else if (EgyRendezo < KettoRendezo)
            {
                return "A két rendezős sorozatok száma több mint az egy rendezős sorozatok száma.";
            }
            else if (EgyRendezo == KettoRendezo)
            {
                return "Az egy rendezős sorozatok száma ugyanannyi mint a két rendezős sorozatok száma.";
            }
            else
            {
                return "Something went wrong.";
            }
        }
        public double HuszadikElott => sorozatok.Where(x => x.Ev <= 2000).Average(y => y.Evadok);
        public string LegKevesebbKat => sorozatok.GroupBy(x => x.Mufaj).MinBy(y => y.Count()).Key;
        public void FajlIr(string szarmazas)
        {
            string fajl = "szarmazas.txt";
            using (StreamWriter iro = new StreamWriter(fajl))
            {
                var valogatot = sorozatok.Where(x => x.Szarmazas.Equals(szarmazas, StringComparison.OrdinalIgnoreCase));
                foreach (var sorozat in valogatot)
                {
                    iro.WriteLine($"{sorozat.Cim};{sorozat.Rendezo};{sorozat.Szarmazas};{sorozat.Ev};{sorozat.Mufaj};{sorozat.Evadok}");
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"A {szarmazas} származású sorozatok adatai a {fajl} fájlba lettek mentve.");
            Console.ResetColor();
        }
    }
}