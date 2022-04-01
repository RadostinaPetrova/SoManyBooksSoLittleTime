namespace SoManyBooksSoLittleTime.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using SoManyBooksSoLittleTime.Services.Data;
    using SoManyBooksSoLittleTime.Web.ViewModels.Books;

    public class EditBookController : AdministrationController
    {
        private readonly IAuthorsService authorsService;
        private readonly IBooksService booksService;

        public EditBookController(IAuthorsService authorsService, IBooksService booksService)
        {
            this.authorsService = authorsService;
            this.booksService = booksService;
        }

        public IActionResult Edit(int id)
        {
            var inputModel = this.booksService.GetById<EditBookInputModel>(id);
            inputModel.Authors = this.authorsService.GetAllAuthorsAsKeyValuePairs();
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBookInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Authors = this.authorsService.GetAllAuthorsAsKeyValuePairs();
                return this.View(input);
            }

            await this.booksService.EditAsync(id, input);

            return this.RedirectToAction("ById", new RouteValueDictionary(new { area = "", controller = "Books", action = "ById", Id = id }));
        }
    }
}
