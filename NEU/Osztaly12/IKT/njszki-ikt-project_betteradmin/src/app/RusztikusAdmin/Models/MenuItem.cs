namespace RusztikusAdmin.Models
{
    public class MenuItem
    {
        public long Id { get; set; } // Changed to long to match Date.now() from web
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public bool Available { get; set; } = true;
        public string? Image { get; set; }
        public string? Allergens { get; set; }
    }
}