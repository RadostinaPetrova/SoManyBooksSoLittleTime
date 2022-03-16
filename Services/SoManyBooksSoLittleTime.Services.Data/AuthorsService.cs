namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;

    public class AuthorsService : IAuthorsService
    {
        private readonly IDeletableEntityRepository<Author> authorsRepository;

        public AuthorsService(IDeletableEntityRepository<Author> authorsRepository)
        {
            this.authorsRepository = authorsRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAuthorsAsKeyValuePairs()
        {
            return this.authorsRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
