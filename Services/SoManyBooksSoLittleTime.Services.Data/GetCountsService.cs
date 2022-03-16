namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Linq;

    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Data.Models;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Book> booksRepository;

        private readonly IDeletableEntityRepository<Author> authorsRepository;

        private readonly IDeletableEntityRepository<Genre> genresRepository;

        private readonly IRepository<Image> imagesRepository;

        public GetCountsService(IDeletableEntityRepository<Book> booksRepository, IDeletableEntityRepository<Author> authorsRepository, IDeletableEntityRepository<Genre> genresRepository, IRepository<Image> imagesRepository)
        {

            this.booksRepository = booksRepository;
            this.authorsRepository = authorsRepository;
            this.genresRepository = genresRepository;
            this.imagesRepository = imagesRepository;
        }

        public CountsDto GetCounts()
        {
            var data = new CountsDto
            {
                BooksCount = this.booksRepository.All().Count(),
                AuthorsCount = this.authorsRepository.All().Count(),
                GenresCount = this.genresRepository.All().Count(),
                ImagesCount = this.imagesRepository.All().Count(),
            };

            return data;
        }
    }
}
