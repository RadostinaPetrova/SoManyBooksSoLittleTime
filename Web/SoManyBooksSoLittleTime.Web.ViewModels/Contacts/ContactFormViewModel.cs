namespace SoManyBooksSoLittleTime.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    public class ContactFormViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your name")]
        [Display(Name = "Your name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        [Display(Name = "Your email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your message title")]
        [StringLength(150, ErrorMessage = "Message title should be between {2} and {1} symbols.", MinimumLength = 2)]
        [Display(Name = "Message title")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your message")]
        [StringLength(5000, ErrorMessage = "Message should be at least {2} symbols.", MinimumLength = 20)]
        [Display(Name = "Message content")]
        public string Content { get; set; }
    }
}
