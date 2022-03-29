namespace SoManyBooksSoLittleTime.Web.ViewModels.Books
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoManyBooksSoLittleTime.Web.ViewModels.Common;

    public class BaseBookInputModel
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(2)]
        [Display(Name ="Title*")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author's name is required")]
        [Display(Name = "Author*")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(50)]
        [Display(Name = "Description*")]
        public string Description { get; set; }

        [Display(Name = "Rating*")]
        [Range(1.00, 5.00, ErrorMessage = "Raiting must be between 1.00 and 5.00")]
        public decimal Rating { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [CheckDateRangeAttribute(ErrorMessage = "Date must be less than or equal to Today's Date")]
        public DateTime Published { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [Display(Name = "ISBN*")]
        [RegularExpression(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$", ErrorMessage = "Valid ISBN is digits only (ISBN 10 or ISBN 13)")]
        public string ISBN { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Authors { get; set; }
    }
}
