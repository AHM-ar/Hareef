using System.ComponentModel.DataAnnotations;

namespace Hareef.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string FullName { get; set; }
        public string Bio { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public string ProfileImage { get; set; }
        public string WhatsAppNumber { get; set; }

        // إضافة حقل لتحديد نوع الحساب (هل هو أدمن أم لا)
        public bool IsAdmin { get; set; } = false;
    }
}