﻿namespace SoManyBooksSoLittleTime.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoManyBooksSoLittleTime.Services;

    // TODO: Move in administration area
    public class ImportBooksController : BaseController
    {
        private readonly IGoodreadsScraperService goodreadsScraperService;

        public ImportBooksController(IGoodreadsScraperService goodreadsScraperService)
        {
            this.goodreadsScraperService = goodreadsScraperService;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            await this.goodreadsScraperService.PopulateDbWithBooksAsync(100);

            return this.Redirect("/");
        }
    }
}
