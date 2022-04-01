namespace SoManyBooksSoLittleTime.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoManyBooksSoLittleTime.Services.Data;

    public class DeleteBookController : AdministrationController
    {
        private readonly IBooksService booksService;

        public DeleteBookController(IBooksService booksService)
        {
            this.booksService = booksService;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.booksService.DeleteAsync(id);
            return this.RedirectToAction("All", "Books", new { area = "" });
        }
    }
}
