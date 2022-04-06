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

        // GET: Administration/Articles
        public async Task<IActionResult> All()
        {
            /*var applicationDbContext = _context.Articles.Include(a => a.Category).Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());*/

            /*if (id <= 0)
            {
                return this.NotFound();
            }*/

            var viewModel = new ArticlesListViewModel { Articles = this.articlesService.GetAll<ArticleInListViewModel>(), };
            return this.View(viewModel);
        }
    }
}
