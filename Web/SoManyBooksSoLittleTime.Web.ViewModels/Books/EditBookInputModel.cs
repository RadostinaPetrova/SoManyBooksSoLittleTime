namespace SoManyBooksSoLittleTime.Web.ViewModels.Books
{
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;

    public class EditBookInputModel : BaseBookInputModel, IMapFrom<Book>
    {
        public int Id { get; set; }
    }
}
