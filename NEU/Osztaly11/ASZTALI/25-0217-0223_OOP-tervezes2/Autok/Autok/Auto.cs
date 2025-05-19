using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autok
{
    internal class Auto
    {
        private List<int> sebessegek = new List<int>();

        public string Tipus { get; init; }
        public string Vezeto { get; set; }
        public int MaxSebesseg { get; init; }

        public double AtlagSebesseg
        {
            get
            {
                return sebessegek.Count > 0 ? sebessegek.Average() : 0;
            }
        }

        public Auto()
        {
            Tipus = "Ismeretlen";
            Vezeto = string.Empty;
            MaxSebesseg = 100;
        }

        public Auto(string tipus, string vezeto, int maximalisSebesseg)
        {
            Tipus = tipus;
            Vezeto = vezeto;
            MaxSebesseg = maximalisSebesseg < 100 ? 100 : maximalisSebesseg;
        }

        public int SebessegMeres()
        {
            if (string.IsNullOrEmpty(Vezeto))
            {
                return 0;
            }
            else
            {
                Random random = new Random();
                int meres = random.Next(MaxSebesseg / 4, MaxSebesseg);
                sebessegek.Add(meres);
                return meres;
            }
        }

        public string Log()
        {
            return $"\tAz 🚗 sofőrje: {Vezeto}\n" +
                   $"\tAz 🚗 átlag sebessége: {AtlagSebesseg:0.00} km/h\n" +
                   $"\tAz 🚗 maximális sebessége: {MaxSebesseg} km/h";
        }

        public int Osszehasonlitas(Auto auto)
        {
            if (this.AtlagSebesseg > auto.AtlagSebesseg)
            {
                return 1;
            }
            else if (this.AtlagSebesseg < auto.AtlagSebesseg)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}