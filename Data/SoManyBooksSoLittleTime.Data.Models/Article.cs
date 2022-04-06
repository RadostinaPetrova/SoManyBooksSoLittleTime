namespace SoManyBooksSoLittleTime.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SoManyBooksSoLittleTime.Data.Common.Models;

    public class Article : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(200)]

        public string Title { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CategoryId { get; set; }

        public virtual ArticleCategory Category { get; set; }

        public string ImagePath { get; set; }
    }
}
