namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;

    public class GenresService : IGenresService
    {
        private const int MinGenreCount = 5;

        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public GenresService(IDeletableEntityRepository<Genre> genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public IEnumerable<T> GetAllPopular<T>()
        {
            return this.genresRepository
                .All()
                .Where(x => x.Books.Count >= MinGenreCount)
                .OrderBy(x => x.Name)
                .To<T>()
                .ToList();
        }
    }
}
