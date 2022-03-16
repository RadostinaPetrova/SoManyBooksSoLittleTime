namespace SoManyBooksSoLittleTime.Web.ViewModels.Books
{
    using System.ComponentModel.DataAnnotations;

    public class BookGenreInputModel
    {
        [Required]
        [MinLength(5)]
        public string GenreName { get; set; }
    }
}
