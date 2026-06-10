namespace RusztikusAdmin.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; } = string.Empty;
        public bool Available { get; set; } = true;
    }
}