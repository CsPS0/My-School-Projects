namespace csomagokLib
{
    public interface IProgram
    {
        public string Nev { get; init; }
        public int Ar { get; init; }

        private void Ertekesitheto()
        {
            int value = Nev + Ar;
        }
    }
}
