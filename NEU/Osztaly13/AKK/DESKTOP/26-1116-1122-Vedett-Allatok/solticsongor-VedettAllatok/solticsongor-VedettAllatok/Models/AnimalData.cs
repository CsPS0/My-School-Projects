using System.Text;

namespace solticsongor_VedettAllatok.Models;

public static class AnimalData
{
    public static List<Animal> Animals = Load();

    static List<Animal> Load()
    {
        List<Animal> list = new List<Animal>();
        string path = Path.Combine(AppContext.BaseDirectory, "vedett.csv");
        string[] lines = File.ReadAllLines(path, Encoding.UTF8);

        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "")
            {
                continue;
            }

            string[] parts = lines[i].Split(';');
            Animal animal = new Animal();
            animal.Id = int.Parse(parts[0]);
            animal.Name = parts[1];
            animal.Value = parts[2];
            animal.Year = int.Parse(parts[3]);
            animal.Category = parts[4];
            list.Add(animal);
        }

        return list;
    }
}
