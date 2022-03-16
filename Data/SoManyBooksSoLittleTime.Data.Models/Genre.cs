namespace SoManyBooksSoLittleTime.Data.Models
{
    using System.Collections.Generic;

    using SoManyBooksSoLittleTime.Data.Common.Models;

    public class Genre : BaseDeletableModel<int>
    {
        public Genre()
        {
            this.Books = new HashSet<BookGenre>();
        }

        public string Name { get; set; }

        public virtual ICollection<BookGenre> Books { get; set; }
    }
}
