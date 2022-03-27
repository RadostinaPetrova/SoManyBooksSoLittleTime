namespace SoManyBooksSoLittleTime.Web.ViewModels.Books
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class CreateBookInputModel : BaseBookInputModel
    {
        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<BookGenreInputModel> Genres { get; set; }
    }
}
