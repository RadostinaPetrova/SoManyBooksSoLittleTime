namespace SoManyBooksSoLittleTime.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SoManyBooksSoLittleTime.Data.Common.Models;

    public class Book : BaseDeletableModel<int>
    {
        public Book()
        {
            this.Genres = new HashSet<BookGenre>();
            this.Images = new HashSet<Image>();
        }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal? Rating { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Published { get; set; }

        [MaxLength(13)]
        public string ISBN { get; set; }

        public virtual ICollection<BookGenre> Genres { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
