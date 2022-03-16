namespace SoManyBooksSoLittleTime.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using SoManyBooksSoLittleTime.Data.Models;

    public class GenresSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Genres.Any())
            {
                return;
            }

            await dbContext.Genres.AddAsync(new Genre { Name = "Fantasy" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Classics" });
            await dbContext.Genres.AddAsync(new Genre { Name = "History" });

            await dbContext.SaveChangesAsync();
        }
    }
}
