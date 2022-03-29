namespace SoManyBooksSoLittleTime.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoManyBooksSoLittleTime.Services;

    public class ImportBooksController : AdministrationController
    {
        private readonly IGoodreadsScraperService goodreadsScraperService;

        public ImportBooksController(IGoodreadsScraperService goodreadsScraperService)
        {
            this.goodreadsScraperService = goodreadsScraperService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Add()
        {
            int booksCount = 10000;
            await this.goodreadsScraperService.PopulateDbWithBooksAsync(booksCount);

            return this.Redirect("/");
        }
    }
}
