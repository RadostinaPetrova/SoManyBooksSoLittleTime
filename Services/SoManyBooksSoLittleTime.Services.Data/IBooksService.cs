namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Threading.Tasks;

    using SoManyBooksSoLittleTime.Web.ViewModels.Books;

    public interface IBooksService
    {
        Task CreateAsync(CreateBookInputModel input);
    }
}
