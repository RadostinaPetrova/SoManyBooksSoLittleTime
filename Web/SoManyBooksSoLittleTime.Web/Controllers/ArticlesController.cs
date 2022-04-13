namespace SoManyBooksSoLittleTime.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoManyBooksSoLittleTime.Services.Data;
    using SoManyBooksSoLittleTime.Web.ViewModels.Articles;

    public class ArticlesController : BaseController
    {
        private readonly IArticlesService articlesService;

        public ArticlesController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public async Task<IActionResult> All()
        {
            var viewModel = new ArticlesListViewModel { Articles = this.articlesService.GetAll<ArticleInListViewModel>(), };
            return this.View(viewModel);
        }
    }
}
