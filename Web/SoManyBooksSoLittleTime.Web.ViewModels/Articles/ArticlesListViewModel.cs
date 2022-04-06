namespace SoManyBooksSoLittleTime.Web.ViewModels.Articles
{
    using System.Collections.Generic;

    public class ArticlesListViewModel
    {
        public IEnumerable<ArticleInListViewModel> Articles { get; set; }
    }
}
