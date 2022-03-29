namespace SoManyBooksSoLittleTime.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using AngleSharp;
    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Models;

    public class GoodreadsScraperService : IGoodreadsScraperService
    {
        private const string Url = "https://www.goodreads.com/book/show/";

        private readonly IConfiguration config;
        private readonly IBrowsingContext context;

        private readonly IDeletableEntityRepository<Author> authorsRepository;
        private readonly IDeletableEntityRepository<Genre> genresRepository;
        private readonly IDeletableEntityRepository<Book> booksRepository;
        private readonly IRepository<BookGenre> bookGenresRepository;
        private readonly IRepository<Image> imagesRepository;

        public GoodreadsScraperService(IDeletableEntityRepository<Author> authorsRepository, IDeletableEntityRepository<Genre> genresRepository, IDeletableEntityRepository<Book> booksRepository, IRepository<BookGenre> bookGenresRepository, IRepository<Image> imagesRepository)
        {
            this.authorsRepository = authorsRepository;
            this.genresRepository = genresRepository;
            this.booksRepository = booksRepository;
            this.bookGenresRepository = bookGenresRepository;
            this.imagesRepository = imagesRepository;

            this.config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(this.config);
        }

        public async Task PopulateDbWithBooksAsync(int booksCount)
        {
            var concurrentBag = new ConcurrentBag<BookDto>();

            Parallel.For(1, booksCount, (i) =>
            {
                try
                {
                    var book = this.GetBook(i);
                    concurrentBag.Add(book);
                }
                catch
                {
                }
            });

            foreach (var book in concurrentBag)
            {
                var authorId = await this.GetOrCreateAuthorAsync(book.AuthorName);
                var bookExists = this.booksRepository.AllAsNoTracking().Any(x => x.Title == book.TitleName);

                if (bookExists)
                {
                    continue;
                }

                var newBook = new Book()
                {
                    Title = book.TitleName,
                    Description = book.Description,
                    ISBN = book.ISBN,
                    Rating = book.Rating,
                    AuthorId = authorId,
                };

                await this.booksRepository.AddAsync(newBook);
                await this.booksRepository.SaveChangesAsync();

                foreach (var item in book.Genres)
                {
                    var genreId = await this.GetOrCreateGenreAsync(item);

                    var bookGenre = new BookGenre
                    {
                        GenreId = genreId,
                        Book = newBook,
                    };

                    await this.bookGenresRepository.AddAsync(bookGenre);
                    await this.bookGenresRepository.SaveChangesAsync();
                }

                var image = new Image
                {
                    ImageUrl = book.ImageUrl,
                    Book = newBook,
                };

                await this.imagesRepository.AddAsync(image);
                await this.imagesRepository.SaveChangesAsync();
            }
        }

        private async Task<int> GetOrCreateGenreAsync(string name)
        {
            var genre = this.genresRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == name);

            if (genre == null)
            {
                genre = new Genre
                {
                    Name = name,
                };

                await this.genresRepository.AddAsync(genre);
                await this.genresRepository.SaveChangesAsync();
            }

            return genre.Id;
        }

        private async Task<int> GetOrCreateAuthorAsync(string authorName)
        {
            var author = this.authorsRepository.AllAsNoTracking()
                .FirstOrDefault(x => x.FullName == authorName);

            if (author == null)
            {
                author = new Author()
                {
                    FullName = authorName,
                };

                await this.authorsRepository.AddAsync(author);
                await this.authorsRepository.SaveChangesAsync();
            }

            return author.Id;
        }

        private BookDto GetBook(int id)
        {
            var document = this.context.OpenAsync($"{Url}{id}").GetAwaiter().GetResult();

            if (document.StatusCode == HttpStatusCode.NotFound)
            {
                throw new InvalidOperationException();
            }

            var book = new BookDto();

            book.TitleName = document.QuerySelector("#bookTitle").TextContent.Trim();

            book.AuthorName = document.QuerySelector("a.authorName > span").TextContent.Trim();

            try
            {
                var descriptionData = document.QuerySelectorAll("#description > span");
                book.Description = descriptionData[1].TextContent.Trim();
            }
            catch (Exception)
            {
                var descriptionData = document.QuerySelectorAll("#description > span");
                book.Description = descriptionData[0].TextContent.Trim();
            }

            var ratingInput = document.QuerySelectorAll("#bookMeta > span");
            book.Rating = decimal.Parse(ratingInput[1].TextContent.Trim());

            var isbnData = document.QuerySelectorAll("#bookDataBox > .clearFloats > .infoBoxRowItem");
            var isbnInput = isbnData[1].FirstChild.TextContent.Trim();
            if (isbnInput.Any(char.IsDigit))
            {
                book.ISBN = isbnInput;
            }
            else
            {
                book.ISBN = null;
            }

            var genres = document.QuerySelectorAll(".bigBoxContent > .elementList > .left");
            foreach (var item in genres)
            {
                book.Genres.Add(item.TextContent.Trim());
            }

            book.ImageUrl = document.QuerySelector("#coverImage").GetAttribute("src");

            return book;
        }
    }
}
