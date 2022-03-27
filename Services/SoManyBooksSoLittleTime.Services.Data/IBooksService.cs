namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoManyBooksSoLittleTime.Web.ViewModels.Books;

    public interface IBooksService
    {
        Task CreateAsync(CreateBookInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        IEnumerable<T> GetRandom<T>(int count);

        int GetCount();

        T GetById<T>(int id);

        Task EditAsync(int id, EditBookInputModel input);

        IEnumerable<T> GetByGenres<T>(IEnumerable<int> genreIds);
    }
}
