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
    using SoManyBooksSoLittleTime.Web.ViewModels.Articles;
    using Xunit;

    public class ArticlesServiceTests
    {
        [Fact]
        public async Task CreateAsyncIsSuccessfulWhenOneArticleIsAdded()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile image = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.png");

            var list = new List<Article>();

            var model = new CreateArticleInputModel
            {
                Title = "Test Article",
                Content = "Content should be at least 50 symbols. Here we go ... ",
                CategoryId = 1,
                Image = image,
            };

            var mockRepoArticle = new Mock<IDeletableEntityRepository<Article>>();
            mockRepoArticle.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepoArticle.Setup(x => x.AddAsync(It.IsAny<Article>())).Callback((Article article) => list.Add(article));

            var service = new ArticlesService(mockRepoArticle.Object);

            var userId = "a29916d1-be82-41c5-a189-7366a9502467";
            var imagePath = "C:\\Users\\User\\source\\repos\\SoManyBooksSoLittleTime\\Web\\SoManyBooksSoLittleTime.Web\\wwwroot/images";

            await service.CreateAsync(model, userId, imagePath);

            Assert.Equal(1, list.Count);
            Assert.Equal("Test Article", list.First().Title);
            Assert.Equal(1, list.First().CategoryId);
        }

        [Fact]
        public async Task CreateAsyncIsSuccessfulWhenMoreThatOneArticlesAreAdded()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile image = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.png");

            var list = new List<Article>();

            var model = new CreateArticleInputModel
            {
                Title = "Test Article",
                Content = "Content should be at least 50 symbols. Here we go ... ",
                CategoryId = 1,
                Image = image,
            };

            var secondModel = new CreateArticleInputModel
            {
                Title = "Second Article",
                Content = "Content should be at least 50 symbols. Here we go ... ",
                CategoryId = 2,
                Image = image,
            };

            var mockRepoArticle = new Mock<IDeletableEntityRepository<Article>>();
            mockRepoArticle.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepoArticle.Setup(x => x.AddAsync(It.IsAny<Article>())).Callback((Article article) => list.Add(article));

            var service = new ArticlesService(mockRepoArticle.Object);

            var userId = "a29916d1-be82-41c5-a189-7366a9502467";
            var imagePath = "C:\\Users\\User\\source\\repos\\SoManyBooksSoLittleTime\\Web\\SoManyBooksSoLittleTime.Web\\wwwroot/images";

            await service.CreateAsync(model, userId, imagePath);
            await service.CreateAsync(model, userId, imagePath);
            await service.CreateAsync(secondModel, userId, imagePath);
            await service.CreateAsync(secondModel, userId, imagePath);

            Assert.Equal(4, list.Count);
            Assert.Equal("Second Article", list.Last().Title);
            Assert.Equal(2, list.Last().CategoryId);
        }

        [Fact]
        public async Task CreateAsyncThrowsExceptionWhenDataIsInvalid()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile image = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.png");

            var list = new List<Article>();

            var model = new CreateArticleInputModel
            {
                Title = "A",
                Content = "Content should be at least 50 symbols. Here we go ... ",
                CategoryId = 1,
            };

            var mockRepoArticle = new Mock<IDeletableEntityRepository<Article>>();
            mockRepoArticle.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepoArticle.Setup(x => x.AddAsync(It.IsAny<Article>())).Callback((Article article) => list.Add(article));

            var service = new ArticlesService(mockRepoArticle.Object);

            var userId = "a29916d1-be82-41c5-a189-7366a9502467";
            var imagePath = "C:\\Users\\User\\source\\repos\\SoManyBooksSoLittleTime\\Web\\SoManyBooksSoLittleTime.Web\\wwwroot/images";

            Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(model, userId, imagePath));
        }

        [Fact]
        public async Task EditAsyncIsSuccessfulWhenIdIsCorrect()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile image = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.png");

            var list = new List<Article>();

            var model = new CreateArticleInputModel
            {
                Title = "Test Article",
                Content = "Content should be at least 50 symbols. Here we go ... ",
                CategoryId = 1,
                Image = image,
            };

            var editModel = new EditArticleInputModel
            {
                Id = 1,
                Title = "EditModel Article",
                Content = "Content should be at least 50 symbols. Here we go ... ",
                CategoryId = 10,
            };

            var mockRepoArticle = new Mock<IDeletableEntityRepository<Article>>();
            mockRepoArticle.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepoArticle.Setup(x => x.AddAsync(It.IsAny<Article>())).Callback((Article article) => list.Add(article));
            mockRepoArticle.Setup(x => x.Update(It.IsAny<Article>()));

            var service = new ArticlesService(mockRepoArticle.Object);

            var userId = "a29916d1-be82-41c5-a189-7366a9502467";
            var imagePath = "C:\\Users\\User\\source\\repos\\SoManyBooksSoLittleTime\\Web\\SoManyBooksSoLittleTime.Web\\wwwroot/images";

            await service.CreateAsync(model, userId, imagePath);
            var id = list.FirstOrDefault().Id;
            await service.EditAsync(id, editModel);

            Assert.Equal(1, list.Count);
            Assert.Equal("EditModel Article", list.First().Title);
            Assert.Equal(10, list.First().CategoryId);
        }

        [Fact]
        public async Task EditAsyncShouldThrowExceptionIfProvidedIdIsNotCorrect()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile image = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.png");

            var list = new List<Article>();

            var model = new CreateArticleInputModel
            {
                Title = "Test Article",
                Content = "Content should be at least 50 symbols. Here we go ... ",
                CategoryId = 1,
                Image = image,
            };

            var editModel = new EditArticleInputModel
            {
                Id = 1,
                Title = "EditModel Article",
                Content = "Content should be at least 50 symbols. Here we go ... ",
                CategoryId = 10,
            };

            var mockRepoArticle = new Mock<IDeletableEntityRepository<Article>>();
            mockRepoArticle.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepoArticle.Setup(x => x.AddAsync(It.IsAny<Article>())).Callback((Article article) => list.Add(article));
            mockRepoArticle.Setup(x => x.Update(It.IsAny<Article>()));

            var service = new ArticlesService(mockRepoArticle.Object);

            var userId = "a29916d1-be82-41c5-a189-7366a9502467";
            var imagePath = "C:\\Users\\User\\source\\repos\\SoManyBooksSoLittleTime\\Web\\SoManyBooksSoLittleTime.Web\\wwwroot/images";

            await service.CreateAsync(model, userId, imagePath);
            var id = list.FirstOrDefault().Id + 10;

            Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync(id, editModel));
        }
    }
}
