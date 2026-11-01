namespace foldrengesLib;

public class Naplo
{
    public int Id { get; set; }
    public DateTime Datum { get; set; }
    public TimeSpan Ido { get; set; }
    public int TelepId { get; set; }
    public double? Magnitudo { get; set; }
    public double Intenzitas { get; set; }

    public string RichterSkala => GetRichterScaleDescription(Magnitudo);

    public static string GetRichterScaleDescription(double? magnitude)
    {
        if (!magnitude.HasValue) return "Ismeretlen";
        if (magnitude < 2.0) return "Mikrorengés";
        if (magnitude < 3.0) return "Rendkívül gyenge";
        if (magnitude < 4.0) return "Nagyon gyenge";
        if (magnitude < 5.0) return "Gyenge";
        if (magnitude < 6.0) return "Közepes";
        if (magnitude < 7.0) return "Erős";
        if (magnitude < 8.0) return "Súlyos";
        if (magnitude < 9.0) return "Pusztító";
        return "Nagyon pusztító";
    }
}
