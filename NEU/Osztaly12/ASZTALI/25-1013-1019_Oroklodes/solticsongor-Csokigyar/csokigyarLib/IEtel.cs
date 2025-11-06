namespace csokigyarLib
{
    public interface IEtel
    {
        IEnumerable<string> MibolKeszul();
        bool MegfeleloMinosegu { get; }
    }
}