namespace RusztikusAdmin.Models
{
    public class RestaurantSettings
    {
        public string RestaurantName { get; set; } = "Rusztikus Étterem";
        public string Address { get; set; } = "1052 Budapest, Petőfi Sándor utca 5.";
        public string Phone { get; set; } = "+36 1 234 5678";
        public string Email { get; set; } = "info@rusztikusetterem.hu";
        public OpeningHours OpeningHours { get; set; } = new OpeningHours();
    }

    public class OpeningHours
    {
        public string Weekdays { get; set; } = "11:00 - 23:00";
        public string Weekends { get; set; } = "12:00 - 24:00";
    }
}