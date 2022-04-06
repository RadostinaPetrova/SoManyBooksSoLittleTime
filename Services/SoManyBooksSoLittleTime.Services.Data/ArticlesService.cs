namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;
    using SoManyBooksSoLittleTime.Web.ViewModels.Articles;

    public class ArticlesService : IArticlesService
    {
        private readonly string[] allowedExtensions = new[] { "png", };
        private readonly IDeletableEntityRepository<Article> articlesRepository;

        public ArticlesService(IDeletableEntityRepository<Article> articlesRepository)
        {
            this.articlesRepository = articlesRepository;
        }

        public async Task CreateAsync(CreateArticleInputModel input, string userId, string imageInputPath)
        {
            var article = new Article
            {
                Title = input.Title,
                Content = input.Content,
                UserId = userId,
                CategoryId = input.CategoryId,
            };

            await this.articlesRepository.AddAsync(article);
            await this.articlesRepository.SaveChangesAsync();

            Directory.CreateDirectory($"{imageInputPath}/articles/");

            var articleId = article.Id;
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');

            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new System.Exception($"Invalid image extension {extension}");
            }

            var physicalPath = $"{imageInputPath}/articles/{articleId}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            article.ImagePath = physicalPath;
            await this.articlesRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, EditArticleInputModel input)
        {
            var articles = this.articlesRepository.All().FirstOrDefault(x => x.Id == id);
            articles.Title = input.Title;
            articles.Content = input.Content;
            articles.CategoryId = input.CategoryId;

            await this.articlesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            var articles = this.articlesRepository.AllAsNoTracking()
              .OrderByDescending(x => x.Id)
              .To<T>().ToList();
            return articles;
        }

        public T GetById<T>(int id)
        {
            var article = this.articlesRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return article;
        }
    }
}
