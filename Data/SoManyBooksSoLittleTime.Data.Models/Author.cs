namespace SoManyBooksSoLittleTime.Data.Models
{
    using System.Collections.Generic;

    using SoManyBooksSoLittleTime.Data.Common.Models;

    public class Author : BaseDeletableModel<int>
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
