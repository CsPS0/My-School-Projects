using System;

namespace RusztikusAdmin.Models
{
    public class Booking
    {
        public string Id { get; set; } = string.Empty; // Changed to string to match JSON/Web
        public string Name { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public int Guests { get; set; }
        public int? TableNumber { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = "Megerősítésre vár";
    }
}