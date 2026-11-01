using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repak
{
    public class Repa
    {
        public int Kor { get; private set; }
        public int Tap { get; init; }
        private double minoseg;
        public double Minoseg
        {
            get { return minoseg; }
            set
            {
                if (value < 0)
                {
                    minoseg = 0;
                }
                else if (value > 1)
                {
                    minoseg = 1;
                }
                else
                {
                    minoseg = value;
                }
            }
        }
        public string Fajta { get; init; }

        public double TapErtek
        {
            get { return Tap * Minoseg; }
        }

        public Repa()
        {
            this.Kor = 0;
            Random random = new Random();
            this.Tap = random.Next(1, 7);
            this.Minoseg = 0.99;
            string[] repak = { "sárgarépa", "fehérrépa", "cukorrépa" };
            this.Fajta = repak[random.Next(0, 3)];
        }

        public Repa(string fajta) : this()
        {
            this.Fajta = fajta;
        }

        public void Informacio()
        {
            Console.WriteLine($"{Fajta}, {Kor}, {TapErtek:F2}");
        }

        public bool Tick(int ertek, double csokkenes)
        {
            this.Kor += ertek;
            this.Minoseg -= csokkenes;

            if (this.Kor <= 30 && this.Minoseg > 0)
            {
                return true;
            }
            return false;
        }
    }
}