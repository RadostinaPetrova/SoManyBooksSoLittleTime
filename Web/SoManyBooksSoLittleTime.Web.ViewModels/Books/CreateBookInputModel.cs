namespace SoManyBooksSoLittleTime.Web.ViewModels.Books
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateBookInputModel : BaseBookInputModel
    {
        [Required]
        public IEnumerable<IFormFile> Images { get; set; }

        [Required]
        public IEnumerable<BookGenreInputModel> Genres { get; set; }
    }
}
