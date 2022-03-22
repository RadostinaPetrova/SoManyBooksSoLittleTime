namespace SoManyBooksSoLittleTime.Web.ViewModels.Books
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateBookInputModel
    {
        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        [Required]
        [MinLength(50)]
        public string Description { get; set; }

        [Range(1, 5)]
        public decimal Rating { get; set; }

        // TODO: Add max date
        [DataType(DataType.Date)]
        public DateTime Published { get; set; }

        // TODO: Write custom validator
        public string ISBN { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<BookGenreInputModel> Genres { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Authors { get; set; }
    }
}
