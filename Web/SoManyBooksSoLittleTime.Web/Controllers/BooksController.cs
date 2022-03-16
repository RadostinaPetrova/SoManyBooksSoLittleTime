namespace SoManyBooksSoLittleTime.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoManyBooksSoLittleTime.Services.Data;
    using SoManyBooksSoLittleTime.Web.ViewModels.Books;
    using System.Threading.Tasks;

    public class BooksController : Controller
    {
        private readonly IAuthorsService authorsService;
        private readonly IBooksService booksService;

        public BooksController(IAuthorsService authorsService, IBooksService booksService)
        {
            this.authorsService = authorsService;
            this.booksService = booksService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateBookInputModel();
            viewModel.Authors = this.authorsService.GetAllAuthorsAsKeyValuePairs();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Authors = this.authorsService.GetAllAuthorsAsKeyValuePairs();
                return this.View(input);
            }

            await this.booksService.CreateAsync(input);

            return this.Redirect("/");
        }
    }
}
