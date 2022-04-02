namespace SoManyBooksSoLittleTime.Web.ViewModels.Home
{
    using System.Linq;

    using AutoMapper;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;

    public class IndexPageBookViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int AuthorId { get; set; }

        public string AuthorFullName { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Book, IndexPageBookViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                 opt.MapFrom(x =>
                         x.Images.FirstOrDefault().ImageUrl != null ?
                         x.Images.FirstOrDefault().ImageUrl :
                         "/images/books/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension))
                 .ForMember(x => x.Description, opt =>
                 opt.MapFrom(x => x.Description.Length > 200 ? x.Description.Substring(0, 200) : x.Description));
        }
    }
}
