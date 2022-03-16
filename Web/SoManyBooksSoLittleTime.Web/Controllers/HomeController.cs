namespace SoManyBooksSoLittleTime.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using SoManyBooksSoLittleTime.Services.Data;
    using SoManyBooksSoLittleTime.Web.ViewModels;
    using SoManyBooksSoLittleTime.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IGetCountsService countService;

        public HomeController(IGetCountsService countService)
        {
            this.countService = countService;
        }

        public IActionResult Index()
        {
            var countsDto = this.countService.GetCounts();
            var viewModel = new IndexViewModel
            {
                BooksCount = countsDto.BooksCount,
                AuthorsCount = countsDto.AuthorsCount,
                GenresCount = countsDto.GenresCount,
                ImagesCount = countsDto.ImagesCount,
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
