namespace SoManyBooksSoLittleTime.Web.ViewModels.Books
{
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;

    public class GenresViewModel : IMapFrom<BookGenre>
    {
        public string GenreName { get; set; }
    }
}
