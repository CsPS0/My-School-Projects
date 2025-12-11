namespace solticsongor_Tort
{
    internal class Tort
    {
        public int Szamlalo { get; init; }
        public int Nevezo { get; init; }

        public Tort()
        {
            this.Szamlalo = 0;
            this.Nevezo = 1;
        }

        public static int NagyobbKozosOszto(int a, int b)
        {
            return b == 0 ? a : NagyobbKozosOszto(b, a % b);
        }

        public Tort(int szamlalo, int nevezo)
        {
            if (nevezo == 0)
            {
                throw new ArgumentException("A nevező nem lehet nulla.");
            }

            if (nevezo < 0)
            {
                szamlalo = -szamlalo;
                nevezo = -nevezo;
            }

            int nko = NagyobbKozosOszto(Math.Abs(szamlalo), nevezo);
            this.Szamlalo = szamlalo / nko;
            this.Nevezo = nevezo / nko;
        }

        public override string ToString()
        {
            if (Nevezo == 1)
            {
                return Szamlalo.ToString();
            }
            return $"{Szamlalo}/{Nevezo}";
        }

        public double TizedesTort => (double) Szamlalo / Nevezo;
        
        public static Tort operator + (Tort a, Tort b)
        {
            int ujNevezo = a.Nevezo * b.Nevezo;
            int ujSzamlalo = a.Szamlalo * b.Nevezo + b.Szamlalo * a.Nevezo;
            return new Tort(ujSzamlalo, ujNevezo);
        }

        public static Tort operator - (Tort a, Tort b)
        {
            int ujNevezo = a.Nevezo * b.Nevezo;
            int ujSzamlalo = a.Szamlalo * b.Nevezo - b.Szamlalo * a.Nevezo;
            return new Tort(ujSzamlalo, ujNevezo);
        }

        public static Tort operator * (Tort a, Tort b)
        {
            int ujNevezo = a.Nevezo * b.Nevezo;
            int ujSzamlalo = a.Szamlalo * b.Szamlalo;
            return new Tort(ujSzamlalo, ujNevezo);
        }

        public static Tort operator / (Tort a, Tort b)
        {
            if (b.Szamlalo == 0)
            {
                throw new DivideByZeroException("Nullával való osztás.");
            }

            int ujNevezo = a.Nevezo * b.Szamlalo;
            int ujSzamlalo = a.Szamlalo * b.Nevezo;
            return new Tort(ujSzamlalo, ujNevezo);
        }

        public static bool operator == (Tort a, Tort b)
        {
            return a.Szamlalo == b.Szamlalo && a.Nevezo == b.Nevezo;
        }

        public static bool operator != (Tort a, Tort b)
        {
            return !(a == b);
        }

        public override bool Equals(object? targy)
        {
            if (targy is Tort egyeb)
            {
                return this == egyeb;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Szamlalo, Nevezo);
        }

        public static bool operator < (Tort a, Tort b)
        {
            return a.Szamlalo * b.Nevezo < b.Szamlalo * a.Nevezo;
        }

        public static bool operator > (Tort a, Tort b)
        {
            return a.Szamlalo * b.Nevezo > b.Szamlalo * a.Nevezo;
        }

        public static bool operator <= (Tort a, Tort b)
        {
            return a.Szamlalo * b.Nevezo <= b.Szamlalo * a.Nevezo;
        }

        public static bool operator >= (Tort a, Tort b)
        {
            return a.Szamlalo * b.Nevezo >= b.Szamlalo * a.Nevezo;
        }

        public static Tort operator +(Tort a, int b) => a + new Tort(b, 1);
        public static Tort operator -(Tort a, int b) => a - new Tort(b, 1);
        public static Tort operator *(Tort a, int b) => a * new Tort(b, 1);
        public static Tort operator /(Tort a, int b) => a / new Tort(b, 1);

        public static implicit operator Tort(int n) => new Tort(n, 1);

        public static explicit operator int(Tort t) => (int)Math.Round(t.TizedesTort);

        public static implicit operator Tort(double d)
        {
            const int maxNev = 10000;
            if (d == 0) return new Tort(0, 1);
            if (double.IsInfinity(d) || double.IsNaN(d)) throw new ArgumentException("Nem konvertálható végtelen, vagy NaN Tört-é.");

            int nev = 1;
            while (Math.Abs(d * nev - Math.Round(d * nev)) > 1e-9 && nev < maxNev)
            {
                nev *= 10;
            }

            int szam = (int)Math.Round(d * nev);
            return new Tort(szam, nev);
        }

    }
}