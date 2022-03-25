namespace SoManyBooksSoLittleTime.Web.ViewModels.Books
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;

    public class SingleBookViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string AuthorName { get; set; }

        public string UserUserName { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public decimal Rating { get; set; }

        public DateTime Published { get; set; }

        public string ISBN { get; set; }

        public IEnumerable<GenresViewModel> Genres { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Book, SingleBookViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                 opt.MapFrom(x =>
                         x.Images.FirstOrDefault().ImageUrl != null ?
                         x.Images.FirstOrDefault().ImageUrl :
                         "/images/books/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
