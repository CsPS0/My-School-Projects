namespace dobozokLib
{
    public abstract class TeasDoboz : IDoboz
    {
        public int DarabSzam { get; protected set; }
        public abstract int Ar { get; }
        public abstract string Nev { get; }

        public TeasDoboz(int darabSzam)
        {
            DarabSzam = darabSzam;
        }

        public override string ToString()
        {
            return $"{Nev} ({Ar} Ft)";
        }
    }
}