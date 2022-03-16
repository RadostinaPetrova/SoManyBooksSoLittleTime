namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Web.ViewModels.Books;

    public class BooksService : IBooksService
    {
        private readonly IDeletableEntityRepository<Book> booksRepository;
        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public BooksService(IDeletableEntityRepository<Book> booksRepository, IDeletableEntityRepository<Genre> genresRepository)
        {
            this.booksRepository = booksRepository;
            this.genresRepository = genresRepository;
        }

        public async Task CreateAsync(CreateBookInputModel input)
        {
            var book = new Book()
            {
                Title = input.Title,
                AuthorId = input.AuthorId,
                Description = input.Description,
                Rating = input.Rating,
                Published = input.Published,
                ISBN = input.ISBN,
            };

            foreach (var inputGenre in input.Genres)
            {
                var genre = this.genresRepository.All().FirstOrDefault(x => x.Name == inputGenre.GenreName);

                if (genre == null)
                {
                    genre = new Genre { Name = inputGenre.GenreName };
                }

                book.Genres.Add(new BookGenre
                {
                    Genre = genre,
                });
            }

            await this.booksRepository.AddAsync(book);
            await this.booksRepository.SaveChangesAsync();
        }
    }
}
