using System.Drawing;

namespace csomagokLib;
    
public class UtazasiCsomag : ProgramElem
{
    public string Nev { get; init; }
    public string Ar { get; init; }

    public UtazasiCsomag(string data)
    {
        string[] datas = data.Split(";");
        Nev = "Utazási csomag: ";
        Ar = "\t-\tFt";
    }
}