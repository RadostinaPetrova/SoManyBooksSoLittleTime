namespace SoManyBooksSoLittleTime.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoManyBooksSoLittleTime.Data.Common.Models;

    public class ArticleCategory : BaseDeletableModel<int>
    {
        public ArticleCategory()
        {
            this.Articles = new HashSet<Article>();
        }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
