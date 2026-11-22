namespace VedettLibrary;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Value { get; set; } = "";
    public int Year { get; set; }
    public string Category { get; set; } = "";

    public override string ToString()
    {
        return Name;
    }
}
