namespace SoManyBooksSoLittleTime.Services
{
    using System.Threading.Tasks;

    public interface IGoodReadsScraperService
    {
        Task PopulateDbWithBooksAsync(int booksCount);
    }
}
