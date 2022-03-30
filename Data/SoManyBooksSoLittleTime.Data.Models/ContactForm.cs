namespace SoManyBooksSoLittleTime.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SoManyBooksSoLittleTime.Data.Common.Models;

    public class ContactForm : BaseModel<int>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
