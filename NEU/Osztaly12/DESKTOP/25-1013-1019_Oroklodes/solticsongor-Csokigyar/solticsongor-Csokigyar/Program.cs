using csokigyarLib;

var etelek = new List<IEtel>();
var gyar = new EtelFactory();

using var sr = new StreamReader("input.txt");
string? dbSor = sr.ReadLine();
if (dbSor != null)
{
    int db = int.Parse(dbSor);
    for (int i = 0; i < db; i++)
    {
        try
        {
            string? sor = sr.ReadLine();
            if (sor != null)
            {
                etelek.Add(gyar.Factory(sor));
            }
        }
        catch (SilanyMinosegException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
foreach (var etel in etelek)
{
    Console.WriteLine(etel);
}