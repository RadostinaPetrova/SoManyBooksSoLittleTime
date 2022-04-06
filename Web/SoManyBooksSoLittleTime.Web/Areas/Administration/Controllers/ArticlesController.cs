namespace SoManyBooksSoLittleTime.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using SoManyBooksSoLittleTime.Data;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Data;
    using SoManyBooksSoLittleTime.Web.ViewModels.Articles;

    [Area("Administration")]
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IArticlesService articlesService;
        private readonly IArticleCategoriesService articleCategoriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public ArticlesController(ApplicationDbContext context, IArticlesService articlesService, IArticleCategoriesService articleCategoriesService, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            this.articlesService = articlesService;
            this.articleCategoriesService = articleCategoriesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        // GET: Administration/Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Administration/Articles/Create
        public IActionResult Create()
        {
            var viewModel = new CreateArticleInputModel();
            viewModel.Categories = this.articleCategoriesService.GetAllArticleCategories();

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateArticleInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = this.articleCategoriesService.GetAllArticleCategories();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.articlesService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.Categories = this.articleCategoriesService.GetAllArticleCategories();
                return this.View(input);
            }

            return this.RedirectToAction("All", new { area = "" });
        }

        // GET: Administration/Articles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.ArticleCategories, "Id", "Title", article.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", article.UserId);
            return View(article);*/
            var inputModel = this.articlesService.GetById<EditArticleInputModel>(id);
            inputModel.Categories = this.articleCategoriesService.GetAllArticleCategories();
            return this.View(inputModel);
        }

        // POST: Administration/Articles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditArticleInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = this.articleCategoriesService.GetAllArticleCategories();
                return this.View(input);
            }

            await this.articlesService.EditAsync(id, input);
            return this.RedirectToAction("All", new { area = "" });
        }
        /*public async Task<IActionResult> Edit(int id, [Bind("Title,Content,UserId,CategoryId,ImagePath,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ArticleCategories, "Id", "Title", article.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", article.UserId);
            return View(article);
        }*/

        // GET: Administration/Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Administration/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
