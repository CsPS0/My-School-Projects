namespace dobozokLib
{
    public class DobozFactory
    {
        public static TeasDoboz Factory(string sor, Filterek filterek)
        {
            var adatok = sor.Split(';');
            int darabSzam = int.Parse(adatok[0]);
            var idLista = adatok.Skip(1).ToArray();

            if (idLista.Length == 1)
            {
                return new EgyszeruDoboz(idLista[0], darabSzam, filterek);
            }
            else
            {
                ValogatasDoboz doboz = new ValogatasDoboz(filterek);
                int darabPerTipus = darabSzam / idLista.Length;

                foreach (var id in idLista)
                {
                    for (int i = 0; i < darabPerTipus; i++)
                    {
                        doboz = doboz + id;
                    }
                }
                return doboz;
            }
        }
    }
}