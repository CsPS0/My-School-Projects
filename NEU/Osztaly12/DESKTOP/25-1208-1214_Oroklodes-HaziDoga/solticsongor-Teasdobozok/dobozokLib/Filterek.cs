namespace dobozokLib
{
    public class Filterek
    {
        private List<Filter> filterek;

        public Filterek(IEnumerable<Filter> filterekInput)
        {
            filterek = filterekInput.ToList();
        }

        public Filter? this[string id]
        {
            get
            {
                var f = filterek.FirstOrDefault(f => f.Azonosito == id);
                return f;
            }
        }

        public List<Filter> GyogynovenyFilterek()
        {
            return filterek.Where(f => f.Gyogytea)
                            .OrderBy(f => f.Tipus)
                            .ToList();
        }
        
        public bool Contains(string id)
        {
            return filterek.Any(f => f.Azonosito == id);
        }
    }
}