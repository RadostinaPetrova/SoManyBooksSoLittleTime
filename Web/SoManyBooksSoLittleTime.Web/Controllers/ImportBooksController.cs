namespace SoManyBooksSoLittleTime.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using SoManyBooksSoLittleTime.Services;

    // TODO: Move in administration area
    public class ImportBooksController : BaseController
    {
        private readonly IGoodreadsScraperService goodReadsScraperService;

        public ImportBooksController(IGoodreadsScraperService goodReadsScraperService)
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
