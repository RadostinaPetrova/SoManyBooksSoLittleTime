namespace SoManyBooksSoLittleTime.Web.ViewModels.Articles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateArticleInputModel
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Description should be between {2} and {1} symbols.", MinimumLength = 2)]
        [Display(Name = "Title*")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [StringLength(3000, ErrorMessage = "Content should be between {2} and {1} symbols.", MinimumLength = 50)]
        [Display(Name = "Content*")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Category*")]
        public int CategoryId { get; set; }

        public IEnumerable<ArticleCategoryViewModel> Categories { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "Image (.png)")]
        public IFormFile Image { get; set; }
    }
}
