namespace SoManyBooksSoLittleTime.Data.Models
{
    using System;
    using System.Collections.Generic;

    using SoManyBooksSoLittleTime.Data.Common.Models;

    public class Book : BaseDeletableModel<int>
    {
        public Book()
        {
            this.Genres = new HashSet<BookGenre>();
            this.Images = new HashSet<Image>();
        }

        public string Title { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public decimal? Rating { get; set; }

        public DateTime Published { get; set; }

        public string ISBN { get; set; }

        public virtual ICollection<BookGenre> Genres { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
