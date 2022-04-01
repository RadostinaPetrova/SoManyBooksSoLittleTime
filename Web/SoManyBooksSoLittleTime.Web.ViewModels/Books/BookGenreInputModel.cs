namespace SoManyBooksSoLittleTime.Web.ViewModels.Books
{
    using System.ComponentModel.DataAnnotations;

    public class BookGenreInputModel
    {
        [Required]
        [StringLength(150, ErrorMessage = "Genre should be between {2} and {1} symbols.", MinimumLength = 3)]
        [Display(Name = "Genres*")]
        public string GenreName { get; set; }
    }
}
