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
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Article> articleRepository;

        public ArticlesService(IDeletableEntityRepository<Article> articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public async Task CreateAsync(CreateArticleInputModel input, string userId, string imagePath)
        {
            var article = new Article
            {
                Title = input.Title,
                Content = input.Content,
                UserId = userId,
                CategoryId = input.CategoryId,
            };

            Directory.CreateDirectory($"{imagePath}/articles/");

            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');

            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new System.Exception($"Invalid image extension {extension}");
            }

            var physicalPath = $"{imagePath}/articles/{article.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            article.ImagePath = physicalPath;

            await this.articleRepository.AddAsync(article);
            await this.articleRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            var articles = this.articleRepository.AllAsNoTracking()
              .OrderByDescending(x => x.Id)
              .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
              .To<T>().ToList();
            return articles;
        }
    }
}
