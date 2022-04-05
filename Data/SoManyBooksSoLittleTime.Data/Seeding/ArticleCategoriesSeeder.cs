namespace SoManyBooksSoLittleTime.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using SoManyBooksSoLittleTime.Data.Models;

    public class ArticleCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ArticleCategories.Any())
            {
                return;
            }

            await dbContext.ArticleCategories.AddAsync(new ArticleCategory { Title = "Book Review" });
            await dbContext.ArticleCategories.AddAsync(new ArticleCategory { Title = "Movies Based On Books" });

            await dbContext.SaveChangesAsync();
        }
    }
}
