namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoManyBooksSoLittleTime.Web.ViewModels.Articles;

    public interface IArticlesService
    {
        Task CreateAsync(CreateArticleInputModel input, string userId, string imageInputPath);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task EditAsync(int id, EditArticleInputModel input);
    }
}
