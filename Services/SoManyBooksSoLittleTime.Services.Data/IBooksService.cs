namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoManyBooksSoLittleTime.Web.ViewModels.Books;

    public interface IBooksService
    {
        Task CreateAsync(CreateBookInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();

        T GetById<T>(int id);
    }
}
