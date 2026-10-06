namespace csomagokLib;

public class EgyszeruProgram : UtazasiProgram
{
    public string Azonosito { get; init; }
    public string Megnevezes { get; init; }
    public string Helyszin { get; init; }
    public int Ar { get; init; }

    public EgyszeruProgram(string data)
    {
        string Nev = Megnevezes;
        string Ar = int.Parse(Ar);
    }
}