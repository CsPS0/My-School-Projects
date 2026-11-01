using ijaszLib;

List<Ijasz> _lista = new();
Random rnd = new Random();
for (int i = 0; i < 100; i++)
{
    _lista.Add(new Ijasz());
    _lista[i] = new Ijasz(rnd.Next(3, 6));
}

foreach (var ijasz in _lista)
{
    Console.WriteLine(ijasz.Info());
    if (ijasz.Lo())
    {
        Console.WriteLine("Sikeres lövés!");
    }
    else
    {
        Console.WriteLine("Túl fáradt, pihennie kell.");
        ijasz.Pihen();
        Console.WriteLine("Pihent: " + ijasz.Info());
    }
}