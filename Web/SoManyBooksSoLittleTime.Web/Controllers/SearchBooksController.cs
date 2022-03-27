namespace SoManyBooksSoLittleTime.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoManyBooksSoLittleTime.Services.Data;
    using SoManyBooksSoLittleTime.Web.ViewModels.Books;
    using SoManyBooksSoLittleTime.Web.ViewModels.SearchBooks;

    public class SearchBooksController : BaseController
    {
        private readonly IBooksService booksService;
        private readonly IGenresService genresService;

        public SearchBooksController(IBooksService booksService, IGenresService genresService)
        {
            this.booksService = booksService;
            this.genresService = genresService;
        }

        public IActionResult Index()
        {
            var viewModel = new SearchIndexViewModel
            {
                Genres = this.genresService.GetAllPopular<GenreNameIdViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult List(SearchListInputModel input)
        {
            var viewModel = new ListViewModel
            {
                Books = this.booksService.GetByGenres<BookInListViewModel>(input.Genres),
            };
            return this.View(viewModel);
        }
    }
}
