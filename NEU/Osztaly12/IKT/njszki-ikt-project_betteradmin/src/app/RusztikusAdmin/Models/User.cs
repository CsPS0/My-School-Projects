namespace RusztikusAdmin.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "user";
        public string FullName { get; set; } = string.Empty;
        
        public bool IsNotAdmin => !string.Equals(Username, "admin", System.StringComparison.OrdinalIgnoreCase);
        // Password is not stored here for security, handled in requests
    }
}