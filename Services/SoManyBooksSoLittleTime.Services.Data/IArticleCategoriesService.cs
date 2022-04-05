namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Collections.Generic;

    using SoManyBooksSoLittleTime.Web.ViewModels.Articles;

    public interface IArticleCategoriesService
    {
        IEnumerable<ArticleCategoryViewModel> GetAllArticleCategories();
    }
}
