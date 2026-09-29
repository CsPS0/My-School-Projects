using starTrekLib;
using System.IO;

namespace StarTrekGUI
{
    public static class DataStore
    {
        private static readonly string mappa = Path.Combine("..", "..", "..", "..", "Console");

        public static Tablak Betolt()
        {
            Tablak tablak = new Tablak();

            foreach (string sor in File.ReadAllLines(Path.Combine(mappa, "fajok.csv")).Skip(1))
            {
                string[] m = sor.Split(';');
                tablak.Fajok.Rows.Add(int.Parse(m[0]), m[1].Trim());
            }

            foreach (string sor in File.ReadAllLines(Path.Combine(mappa, "hajo_szerepek.csv")).Skip(1))
            {
                string[] m = sor.Split(';');
                tablak.HajoSzerepek.Rows.Add(int.Parse(m[0]), m[1].Trim());
            }

            foreach (string sor in File.ReadAllLines(Path.Combine(mappa, "hajo_osztalyok.csv")).Skip(1))
            {
                string[] m = sor.Split(';');
                tablak.HajoOsztalyok.Rows.Add(int.Parse(m[0]), m[1].Trim(), int.Parse(m[2]));
            }

            foreach (string sor in File.ReadAllLines(Path.Combine(mappa, "urhajok.csv")).Skip(1))
            {
                string[] m = sor.Split(';');
                tablak.Urhajok.Rows.Add(int.Parse(m[0]), m[1].Trim(), m[2].Trim(), int.Parse(m[3]), int.Parse(m[4]));
            }

            return tablak;
        }
    }
}