namespace SoManyBooksSoLittleTime.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;
    using SoManyBooksSoLittleTime.Web.ViewModels.Books;

    public class BooksService : IBooksService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Book> booksRepository;
        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public BooksService(IDeletableEntityRepository<Book> booksRepository, IDeletableEntityRepository<Genre> genresRepository)
        {
            this.booksRepository = booksRepository;
            this.genresRepository = genresRepository;
        }

        public async Task CreateAsync(CreateBookInputModel input, string userId, string imagePath)
        {
            var book = new Book
            {
                Title = input.Title,
                AuthorId = input.AuthorId,
                Description = input.Description,
                Rating = input.Rating,
                Published = input.Published,
                ISBN = input.ISBN,
                UserId = userId,
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

            Directory.CreateDirectory($"{imagePath}/books/");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');

                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new System.Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    UserId = userId,
                    Extension = extension,
                };
                book.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/books/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.booksRepository.AddAsync(book);
            await this.booksRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var books = this.booksRepository.AllAsNoTracking()
              .OrderByDescending(x => x.Id)
              .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
              .To<T>().ToList();
            return books;
        }

        public T GetById<T>(int id)
        {
            var book = this.booksRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return book;
        }

        public int GetCount()
        {
            return this.booksRepository.All().Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.booksRepository.All().OrderBy(x => Guid.NewGuid()).To<T>().Take(count).ToList();
        }
    }
}
