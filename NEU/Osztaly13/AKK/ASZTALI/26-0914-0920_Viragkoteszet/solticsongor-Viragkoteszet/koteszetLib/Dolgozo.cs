namespace koteszetLib
{
    public abstract class Dolgozo
    {
        public int Id { get; }
        public string Nev { get; }
        public FeladatLista Feladatok { get; protected set; }

        public abstract double Gyakorlottsag { get; }
        public abstract int MunkaraForditottIdo { get; }

        protected Dolgozo(int id, string nev)
        {
            Id = id;
            Nev = nev;
            Feladatok = new FeladatLista();
        }

        public virtual void UjFeladatHozzaadasa(Termek termek)
        {
            Feladatok += termek;
        }

        public override string ToString()
        {
            return $"{Nev}: {MunkaraForditottIdo} perc";
        }
    }
}