namespace SoManyBooksSoLittleTime.Web.ViewModels.Books
{
    using System.ComponentModel.DataAnnotations;

    public class BookGenreInputModel
    {
        [Required]
        [MinLength(3)]
        [Display(Name = "Genres*")]
        public string GenreName { get; set; }
    }
}
