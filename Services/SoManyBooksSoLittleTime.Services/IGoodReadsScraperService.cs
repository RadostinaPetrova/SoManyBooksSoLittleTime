namespace SoManyBooksSoLittleTime.Services
{
    using System.Threading.Tasks;

    public interface IGoodreadsScraperService
    {
        Task PopulateDbWithBooksAsync(int booksCount);
    }
}
