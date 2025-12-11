using appleLib;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        ISzimulacio szimulacio;

        string fajlNev = "alma.json";
        try
        {
            szimulacio = JsonSerializer.Deserialize<Alma>(File.ReadAllText(fajlNev))!;
        }
        catch
        {
            szimulacio = new Alma();
        }

        bool kilepes = false;
        Parallel.Invoke(
            () =>
            {
                while (!kilepes && szimulacio.EletbenVan)
                {
                    szimulacio.Kor();
                    Console.Clear();
                    Console.WriteLine(szimulacio.ToString());
                    Thread.Sleep(100);
                }

                if (szimulacio.EletbenVan)
                {
                    File.WriteAllText(fajlNev, JsonSerializer.Serialize<Alma>((szimulacio as Alma)!));
                }
                else
                {
                    if (File.Exists(fajlNev))
                    {
                        File.Delete(fajlNev);
                    }
                }
            },
            () =>
            {
                Console.ReadLine();
                kilepes = true;
            }
        );
    }
}