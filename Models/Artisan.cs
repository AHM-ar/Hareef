namespace Hareef.Models
{
    public class Artisan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } // للإيميل والربط
        public string Profession { get; set; }
        public string Bio { get; set; }
        public string WhatsAppNumber { get; set; }
        public string ProfileImage { get; set; }
    }
}