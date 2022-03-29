namespace SoManyBooksSoLittleTime.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoManyBooksSoLittleTime.Data.Common.Models;

    public class Author : BaseDeletableModel<int>
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        [Required]
        [MaxLength(150)]
        public string FullName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
