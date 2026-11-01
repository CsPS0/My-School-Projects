using System.Text;

namespace teremLib
{
    public sealed class AltalanosTerem : Terem
    {
        public AltalanosTerem(int azon, int feroh) : base(azon, feroh)
        {
            // ha nem megy, nem megy
        }

        public override void IdopontFoglalas(Foglalas foglalas)
        {
            Orarend = Orarend + foglalas;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"t{Azonosito}:");
            sb.AppendLine("Foglalt időpontok:");
            sb.Append(Orarend.ToString());
            return sb.ToString();
        }
    }
}