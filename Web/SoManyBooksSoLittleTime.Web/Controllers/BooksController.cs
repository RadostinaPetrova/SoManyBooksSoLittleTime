namespace SoManyBooksSoLittleTime.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Data;
    using SoManyBooksSoLittleTime.Web.ViewModels.Books;

    public class BooksController : Controller
    {
        private readonly IAuthorsService authorsService;
        private readonly IBooksService booksService;
        private readonly UserManager<ApplicationUser> userManager;

        public BooksController(IAuthorsService authorsService, IBooksService booksService, UserManager<ApplicationUser> userManager)
        {
            this.authorsService = authorsService;
            this.booksService = booksService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateBookInputModel();
            viewModel.Authors = this.authorsService.GetAllAuthorsAsKeyValuePairs();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateBookInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Authors = this.authorsService.GetAllAuthorsAsKeyValuePairs();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.booksService.CreateAsync(input, user.Id);

            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
        {
            const int ItemsPerPage = 12;

            var viewModel = new BooksListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                BooksCount = this.booksService.GetCount(),
                Books = this.booksService.GetAll<BookInListViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}
