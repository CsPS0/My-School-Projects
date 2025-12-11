namespace teremLib
{
    public class TeremNyilvantartas
    {
        private Dictionary<int, Terem> termek;

        public TeremNyilvantartas()
        {
            termek = new Dictionary<int, Terem>();
        }

        public void AddTerem(Terem terem)
        {
            termek.Add(terem.Azonosito, terem);
        }

        public IEnumerable<int> GetTeremAzonositok()
        {
            return termek.Keys;
        }

        public Terem this[int azon]
        {
            get { return termek[azon]; }
        }

        public IEnumerable<Terem> GetAllTermek()
        {
            return termek.Values;
        }

        public void TeremFoglalasok(IEnumerable<Foglalas> foglalasiKerelmek)
        {
            foreach (var foglalasKerem in foglalasiKerelmek)
            {
                if (termek.TryGetValue(foglalasKerem.HelyszinAzonosito, out Terem terem))
                {
                    try
                    {
                        terem.IdopontFoglalas(foglalasKerem);
                    }
                    catch (FoglalasException ex)
                    {
                        string naploUzenet = $"{foglalasKerem.Kezdete:yyyy.MM.dd HH:mm:ss};" +
                                            $"{(int)(foglalasKerem.Vege - foglalasKerem.Kezdete).TotalMinutes};" +
                                            $"{foglalasKerem.HelyszinAzonosito};" +
                                            $"{foglalasKerem.TanarAzonosito} - {ex.Message ?? "Ismeretlen hiba."}";
                        File.AppendAllText("hibalista.txt", naploUzenet + Environment.NewLine);
                    }
                }
                else
                {
                    string naploUzenet = $"{foglalasKerem.Kezdete:yyyy.MM.dd HH:mm:ss};" +
                                        $"{(int)(foglalasKerem.Vege - foglalasKerem.Kezdete).TotalMinutes};" +
                                        $"{foglalasKerem.HelyszinAzonosito};" +
                                        $"{foglalasKerem.TanarAzonosito} - Terem nem található.";
                    File.AppendAllText("hibalista.txt", naploUzenet + Environment.NewLine);
                }
            }
        }
    }
}