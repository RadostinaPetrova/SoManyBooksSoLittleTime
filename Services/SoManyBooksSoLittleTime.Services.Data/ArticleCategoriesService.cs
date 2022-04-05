namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Web.ViewModels.Articles;

    public class ArticleCategoriesService : IArticleCategoriesService
    {
        private readonly IDeletableEntityRepository<ArticleCategory> articleCategoryRepository;

        public ArticleCategoriesService(IDeletableEntityRepository<ArticleCategory> articleCategoryRepository)
        {
            this.articleCategoryRepository = articleCategoryRepository;
        }

        public IEnumerable<ArticleCategoryViewModel> GetAllArticleCategories()
        {
            var viewModel = this.articleCategoryRepository
                .AllAsNoTracking()
                .Select(x => new ArticleCategoryViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                })
                .ToList();

            return viewModel;
        }
    }
}
