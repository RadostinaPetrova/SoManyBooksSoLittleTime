namespace SoManyBooksSoLittleTime.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using SoManyBooksSoLittleTime.Services;

    // TODO: Move in administration area
    public class CollectBooksController : BaseController
    {
        private readonly IGoodReadsScraperService goodReadsScraperService;

        public CollectBooksController(IGoodReadsScraperService goodReadsScraperService)
        {
            this.goodReadsScraperService = goodReadsScraperService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Add()
        {
            await this.goodReadsScraperService.PopulateDbWithBooksAsync(100);

            return this.Redirect("/");
        }
    }
}
