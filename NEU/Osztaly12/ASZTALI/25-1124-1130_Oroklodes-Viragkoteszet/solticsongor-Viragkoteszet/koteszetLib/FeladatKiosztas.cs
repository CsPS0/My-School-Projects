namespace koteszetLib
{
    public class FeladatKiosztas
    {
        public void Kioszt(IEnumerable<string> feladatok, Dolgozok dolgozok, Termekek termekek)
        {
            foreach (var feladat in feladatok)
            {
                var adatok = feladat.Split(';');
                var dolgozoId = int.Parse(adatok[0]);
                var termekId = int.Parse(adatok[1]);

                var dolgozo = dolgozok[dolgozoId];
                var termek = termekek[termekId];

                try
                {
                    dolgozo.UjFeladatHozzaadasa(termek);
                }
                catch (HibasFeladatException ex)
                {
                    File.AppendAllText("hibalista.txt", $"DolgozoID: {ex.DolgozoId}, TermekID: {ex.TermekId}\n");
                }
            }
        }
    }
}