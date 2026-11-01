using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repak
{
    public class Nyul
    {
        public int Kor { get; private set; }
        public string Nem { get; init; }
        public int Jollakottsag { get; private set; }
        private double ugyesseg;
        public double Ugyesseg
        {
            get { return ugyesseg; }
            set
            {
                if (value < 0) ugyesseg = 0;
                else if (value > 0.5) ugyesseg = 0.5;
                else ugyesseg = value;
            }
        }

        public double Esely
        {
            get
            {
                if (Kor >= 6 && Kor <= 80)
                {
                    return Ugyesseg;
                }
                return Ugyesseg * 0.75;
            }
        }

        public Nyul()
        {
            Random random = new Random();
            Kor = 0;
            Nem = random.Next(0, 2) == 0 ? "hím" : "nőstény";
            Jollakottsag = 50;
            Ugyesseg = random.NextDouble() * 0.5;
        }

        public Nyul(double ugyesseg) : this()
        {
            this.Ugyesseg = ugyesseg;
        }

        public void Informacio()
        {
            Console.WriteLine($"{Kor}, {Nem}, {Esely:F2}");
        }

        public bool Eszik(int etelMennyiseg)
        {
            if (Jollakottsag + etelMennyiseg > 100)
            {
                return false;
            }
            Jollakottsag += etelMennyiseg;
            return true;
        }
    }
}