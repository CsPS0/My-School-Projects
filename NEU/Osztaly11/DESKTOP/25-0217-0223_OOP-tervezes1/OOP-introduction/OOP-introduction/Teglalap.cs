using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_introduction
{
    public class Teglalap
    {
        private double a;
        private double b;

        public double A
        {
            get { return a; }
            set { if (value > 0) a = value; }
        }

        public double B
        {
            get { return b; }
            set { if (value > 0) b = value; }
        }

        public Teglalap()
        {
            a = 1;
            b = 2;
        }

        public Teglalap(double a, double b)
        {
            if (a > 0) this.a = a;
            if (b > 0) this.b = b;
        }

        public double Kerulet()
        {
            return 2 * (A + B);
        }

        public double Terulet()
        {
            return A * B;
        }

        public string Info()
        {
            return $"Téglalap: a = {A}, b = {B}\nKerület: {Kerulet()}, Terület: {Terulet()}";
        }
    }


    public class Termek
    {
        private int egysegAr;
        private float kedvezmeny;
        private int raktarKeszlet;

        public string Nev { get; }

        public int EgysegAr
        {
            get { return egysegAr; }
            set { if (value >= 0) egysegAr = value; }
        }

        public float Kedvezmeny
        {
            get { return kedvezmeny; }
            set { if (value >= 0 && value <= 1) kedvezmeny = value; }
        }

        public int RaktarKeszlet
        {
            get { return raktarKeszlet; }
            set { if (value >= 0) raktarKeszlet = value; }
        }

        public Termek(string nev, int ar)
        {
            Nev = nev;
            EgysegAr = ar;
            RaktarKeszlet = 1;
            Kedvezmeny = 0;
        }

        public Termek(string nev, int ar, int raktar)
        {
            Nev = nev;
            EgysegAr = ar;
            RaktarKeszlet = raktar;
            Kedvezmeny = 0;
        }

        public string Informacio()
        {
            return $"Név: {Nev}\nEgységár: {EgysegAr}\nRaktárkészlet: {RaktarKeszlet}\nKedvezmény: {Kedvezmeny * 100}%";
        }

        public bool Eladas(int mennyiseg)
        {
            if (mennyiseg > 0 && RaktarKeszlet >= mennyiseg)
            {
                RaktarKeszlet -= mennyiseg;
                return true;
            }
            return false;
        }

        public void Beszerzes(int mennyiseg)
        {
            if (mennyiseg > 0)
            {
                RaktarKeszlet += mennyiseg;
            }
        }
    }
}