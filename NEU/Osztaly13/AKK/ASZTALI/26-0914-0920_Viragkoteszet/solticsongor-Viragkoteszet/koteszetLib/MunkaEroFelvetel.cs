namespace koteszetLib
{
    public static class MunkaEroFelvetel
    {
        public static Dolgozo DolgozoLetrehozasa(string dolgozoiAdat)
        {
            var adatok = dolgozoiAdat.Split(';');
            var id = int.Parse(adatok[0]);
            var nev = adatok[1];
            var beosztas = adatok[2];

            if (beosztas == "v")
            {
                return new Viragkoto(id, nev);
            }
            else
            {
                var kepesitesek = adatok.Skip(3).ToArray();
                return new Gyakornok(id, nev, kepesitesek);
            }
        }
    }
}