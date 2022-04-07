namespace SoManyBooksSoLittleTime.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Data;
    using SoManyBooksSoLittleTime.Web.ViewModels.Articles;

    [Area("Administration")]
    public class ArticlesController : AdministrationController
    {
        private readonly IArticlesService articlesService;
        private readonly IArticleCategoriesService articleCategoriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public ArticlesController(IArticlesService articlesService, IArticleCategoriesService articleCategoriesService, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            this.articlesService = articlesService;
            this.articleCategoriesService = articleCategoriesService;
            this.userManager = userManager;
            this.environment = environment;
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
    }
}
