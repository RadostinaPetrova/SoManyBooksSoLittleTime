namespace SoManyBooksSoLittleTime.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Moq;
    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;
    using SoManyBooksSoLittleTime.Web.ViewModels;
    using SoManyBooksSoLittleTime.Web.ViewModels.Books;
    using Xunit;

    public class BooksServiceTests
    {
        [Fact]
        public async Task CreateAsyncIsSuccessfulWhenOneBookIsAdded()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile image = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.png");

            var list = new List<Book>();

            var model = new CreateBookInputModel
            {
                Title = "Test Title",
                Description = "Description should be at least 50 symbols. Oh, well...",
                AuthorId = 1,
                Rating = 5,
                Published = DateTime.UtcNow,
                ISBN = "1234567890",
                Genres = new List<BookGenreInputModel>
                {
                    new BookGenreInputModel
                    { GenreName = "Genre Name" },
                },
                Images = new List<IFormFile>
                {
                    image,
                },
            };

            var mockRepoBook = new Mock<IDeletableEntityRepository<Book>>();
            var mockRepoGenre = new Mock<IDeletableEntityRepository<Genre>>();
            mockRepoBook.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepoBook.Setup(x => x.AddAsync(It.IsAny<Book>())).Callback((Book book) => list.Add(book));
            mockRepoGenre.Setup(x => x.AddAsync(It.IsAny<Genre>()));

            var service = new BooksService(mockRepoBook.Object, mockRepoGenre.Object);

            var userId = "a29916d1-be82-41c5-a189-7366a9502467";
            var imagePath = "C:\\Users\\User\\source\\repos\\SoManyBooksSoLittleTime\\Web\\SoManyBooksSoLittleTime.Web\\wwwroot/images";

            await service.CreateAsync(model, userId, imagePath);

            Assert.Equal(1, list.Count);
            Assert.Equal("Test Title", list.First().Title);
            Assert.Equal("1234567890", list.First().ISBN);
            Assert.Equal(1, list.First().AuthorId);
        }

        [Fact]
        public async Task CreateAsyncIsSuccessfulWhenMoreThanOneBookIsAdded()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile image = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.png");

            var list = new List<Book>();

            var model = new CreateBookInputModel
            {
                Title = "Test Title",
                Description = "Description should be at least 50 symbols. Oh, well...",
                AuthorId = 1,
                Rating = 5,
                Published = DateTime.UtcNow,
                ISBN = "1234567890",
                Genres = new List<BookGenreInputModel>
                {
                    new BookGenreInputModel
                    { GenreName = "Genre Name" },
                },
                Images = new List<IFormFile>
                {
                    image,
                },
            };

            var anotherModel = new CreateBookInputModel
            {
                Title = "Another Test Title",
                Description = "Another Description should be at least 50 symbols. Oh, well...",
                AuthorId = 2,
                Rating = 4,
                Published = DateTime.UtcNow,
                ISBN = "1234567890123",
                Genres = new List<BookGenreInputModel>
                {
                    new BookGenreInputModel
                    { GenreName = "Genre Name" },
                },
                Images = new List<IFormFile>
                {
                    image,
                },
            };

            var mockRepoBook = new Mock<IDeletableEntityRepository<Book>>();
            var mockRepoGenre = new Mock<IDeletableEntityRepository<Genre>>();
            mockRepoBook.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepoBook.Setup(x => x.AddAsync(It.IsAny<Book>())).Callback((Book book) => list.Add(book));
            mockRepoGenre.Setup(x => x.AddAsync(It.IsAny<Genre>()));

            var service = new BooksService(mockRepoBook.Object, mockRepoGenre.Object);

            var userId = "a29916d1-be82-41c5-a189-7366a9502467";
            var imagePath = "C:\\Users\\User\\source\\repos\\SoManyBooksSoLittleTime\\Web\\SoManyBooksSoLittleTime.Web\\wwwroot/images";

            await service.CreateAsync(model, userId, imagePath);
            await service.CreateAsync(model, userId, imagePath);
            await service.CreateAsync(anotherModel, userId, imagePath);
            await service.CreateAsync(anotherModel, userId, imagePath);

            Assert.Equal(4, list.Count);
            Assert.Equal("Test Title", list.First().Title);
            Assert.Equal("1234567890123", list.Last().ISBN);
            Assert.Equal(2, list.Last().AuthorId);
        }

        [Fact]
        public async Task CreateAsyncThrowsExceptionWhenDataIsInvalid()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var list = new List<Book>();

            var model = new CreateBookInputModel
            {
                Title = "Test Title",
                Description = "Description should be at least 50 symbols. Oh, well...",
                AuthorId = 1,
                Rating = 10,
                Published = DateTime.UtcNow,
                ISBN = "1",
                Genres = new List<BookGenreInputModel>
                {
                    new BookGenreInputModel
                    { GenreName = "Genre Name" },
                },
                Images = new List<IFormFile>(),
            };

            var mockRepoBook = new Mock<IDeletableEntityRepository<Book>>();
            var mockRepoGenre = new Mock<IDeletableEntityRepository<Genre>>();
            mockRepoBook.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepoBook.Setup(x => x.AddAsync(It.IsAny<Book>())).Callback((Book book) => list.Add(book));
            mockRepoGenre.Setup(x => x.AddAsync(It.IsAny<Genre>()));

            var service = new BooksService(mockRepoBook.Object, mockRepoGenre.Object);

            var userId = "a29916d1-be82-41c5-a189-7366a9502467";
            var imagePath = "C:\\Users\\User\\source\\repos\\SoManyBooksSoLittleTime\\Web\\SoManyBooksSoLittleTime.Web\\wwwroot/images";

            Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(model, userId, imagePath));
        }

        [Fact]
        public async Task EditAsyncIsSuccessfulWhenDataIsCorrect()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile image = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.png");

            var list = new List<Book>();

            var model = new CreateBookInputModel
            {
                Title = "Test Title",
                Description = "Description should be at least 50 symbols. Oh, well...",
                AuthorId = 1,
                Rating = 5,
                Published = DateTime.UtcNow,
                ISBN = "1234567890",
                Genres = new List<BookGenreInputModel>
                {
                    new BookGenreInputModel
                    { GenreName = "Genre Name" },
                },
                Images = new List<IFormFile>
                {
                    image,
                },
            };

            var editModel = new EditBookInputModel
            {
                Id = 1,
                Title = "Edit Title",
                Description = "Description should be at least 50 symbols. Oh, well...",
                AuthorId = 10,
                Rating = 5,
                Published = DateTime.UtcNow,
                ISBN = "1111111111",
            };

            var userId = "a29916d1-be82-41c5-a189-7366a9502467";
            var imagePath = "C:\\Users\\User\\source\\repos\\SoManyBooksSoLittleTime\\Web\\SoManyBooksSoLittleTime.Web\\wwwroot/images";

            var mockRepoBook = new Mock<IDeletableEntityRepository<Book>>();
            var mockRepoGenre = new Mock<IDeletableEntityRepository<Genre>>();
            mockRepoBook.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepoBook.Setup(x => x.AddAsync(It.IsAny<Book>())).Callback((Book book) => list.Add(book));
            mockRepoBook.Setup(x => x.Update(It.IsAny<Book>()));
            mockRepoGenre.Setup(x => x.AddAsync(It.IsAny<Genre>()));

            var service = new BooksService(mockRepoBook.Object, mockRepoGenre.Object);

            await service.CreateAsync(model, userId, imagePath);
            var id = list.FirstOrDefault().Id;
            await service.EditAsync(id, editModel);

            Assert.Equal(1, list.Count);
            Assert.Equal("Edit Title", list.First().Title);
            Assert.Equal("1111111111", list.First().ISBN);
            Assert.Equal(10, list.First().AuthorId);
        }

        [Fact]
        public async Task EditAsyncShouldThrowExceptionIfProvidedIdIsNotCorrect()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile image = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.png");

            var list = new List<Book>();

            var model = new CreateBookInputModel
            {
                Title = "Test Title",
                Description = "Description should be at least 50 symbols. Oh, well...",
                AuthorId = 1,
                Rating = 5,
                Published = DateTime.UtcNow,
                ISBN = "1234567890",
                Genres = new List<BookGenreInputModel>
                {
                    new BookGenreInputModel
                    { GenreName = "Genre Name" },
                },
                Images = new List<IFormFile>
                {
                    image,
                },
            };

            var editModel = new EditBookInputModel
            {
                Id = 1,
                Title = "Edit Title",
                Description = "Description should be at least 50 symbols. Oh, well...",
                AuthorId = 10,
                Rating = 5,
                Published = DateTime.UtcNow,
                ISBN = "1111111111",
            };

            var userId = "a29916d1-be82-41c5-a189-7366a9502467";
            var imagePath = "C:\\Users\\User\\source\\repos\\SoManyBooksSoLittleTime\\Web\\SoManyBooksSoLittleTime.Web\\wwwroot/images";

            var mockRepoBook = new Mock<IDeletableEntityRepository<Book>>();
            var mockRepoGenre = new Mock<IDeletableEntityRepository<Genre>>();
            mockRepoBook.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepoBook.Setup(x => x.AddAsync(It.IsAny<Book>())).Callback((Book book) => list.Add(book));
            mockRepoBook.Setup(x => x.Update(It.IsAny<Book>()));
            mockRepoGenre.Setup(x => x.AddAsync(It.IsAny<Genre>()));

            var service = new BooksService(mockRepoBook.Object, mockRepoGenre.Object);

            await service.CreateAsync(model, userId, imagePath);
            var id = list.FirstOrDefault().Id + 1;

            Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync(id, editModel));
        }

        [Fact]
        public async Task DeleteAsyncIsSuccessfulWhenIdIsCorrect()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile image = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.png");

            var list = new List<Book>();

            var model = new CreateBookInputModel
            {
                Title = "Test Title",
                Description = "Description should be at least 50 symbols. Oh, well...",
                AuthorId = 1,
                Rating = 5,
                Published = DateTime.UtcNow,
                ISBN = "1234567890",
                Genres = new List<BookGenreInputModel>
                {
                    new BookGenreInputModel
                    { GenreName = "Genre Name" },
                },
                Images = new List<IFormFile>
                {
                    image,
                },
            };

            var mockRepoBook = new Mock<IDeletableEntityRepository<Book>>();
            var mockRepoGenre = new Mock<IDeletableEntityRepository<Genre>>();
            mockRepoBook.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepoBook.Setup(x => x.AddAsync(It.IsAny<Book>())).Callback((Book book) => list.Add(book));
            mockRepoBook.Setup(x => x.Delete(It.IsAny<Book>())).Callback((Book book) => list.Remove(book));
            mockRepoGenre.Setup(x => x.AddAsync(It.IsAny<Genre>()));

            var service = new BooksService(mockRepoBook.Object, mockRepoGenre.Object);

            var userId = "a29916d1-be82-41c5-a189-7366a9502467";
            var imagePath = "C:\\Users\\User\\source\\repos\\SoManyBooksSoLittleTime\\Web\\SoManyBooksSoLittleTime.Web\\wwwroot/images";

            await service.CreateAsync(model, userId, imagePath);

            var id = list.First().Id;
            await service.DeleteAsync(id);

            Assert.Empty(list);
        }

        [Fact]
        public async Task DeleteAsyncShouldThrowExceptionIfIdIsNotExisting()
        {
            var list = new List<Book>();

            var mockRepoBook = new Mock<IDeletableEntityRepository<Book>>();
            var mockRepoGenre = new Mock<IDeletableEntityRepository<Genre>>();
            mockRepoBook.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepoBook.Setup(x => x.Delete(It.IsAny<Book>())).Callback((Book book) => list.Remove(book));

            var service = new BooksService(mockRepoBook.Object, mockRepoGenre.Object);

            Assert.ThrowsAsync<ArgumentException>(async () => await service.DeleteAsync(15));
        }

        [Fact]
        public void GetAllReturnsEmptyResultWhenThereIsNoData()
        {
            var mockRepoBook = new Mock<IDeletableEntityRepository<Book>>();
            var mockRepoGenre = new Mock<IDeletableEntityRepository<Genre>>();
            var service = new BooksService(mockRepoBook.Object, mockRepoGenre.Object);

            var result = service.GetAll<BookInListViewModel>(1);

            Assert.Equal(0, result.ToList().Count);
        }
    }
}
