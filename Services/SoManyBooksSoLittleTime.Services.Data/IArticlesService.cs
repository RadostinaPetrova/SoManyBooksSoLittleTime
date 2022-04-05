namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoManyBooksSoLittleTime.Web.ViewModels.Articles;

    public interface IArticlesService
    {
        Task CreateAsync(CreateArticleInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);
    }
}
