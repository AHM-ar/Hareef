namespace Hareef.Models
{
    public class Artisan
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
