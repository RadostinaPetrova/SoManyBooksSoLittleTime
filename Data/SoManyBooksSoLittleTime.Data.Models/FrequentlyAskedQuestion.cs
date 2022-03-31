namespace SoManyBooksSoLittleTime.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SoManyBooksSoLittleTime.Data.Common.Models;

    public class FrequentlyAskedQuestion : BaseModel<int>
    {
        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}
