namespace SoManyBooksSoLittleTime.Web.ViewModels.Books
{
    using System.Linq;

    using AutoMapper;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;

    public class BookInListViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public int AuthorId { get; set; }

        public string AuthorFullName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Book, BookInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                 opt.MapFrom(x =>
                         x.Images.FirstOrDefault().ImageUrl != null ?
                         x.Images.FirstOrDefault().ImageUrl :
                         "/images/books/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
