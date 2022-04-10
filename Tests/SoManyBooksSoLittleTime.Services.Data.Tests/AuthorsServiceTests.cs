namespace SoManyBooksSoLittleTime.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;
    using Xunit;

    public class AuthorsServiceTests
    {
        [Fact]
        public void GetAllAuthorsAsKvpReturnsCorrectCount()
        {
            var list = new List<Author>
            {
                new Author { Id = 1, FullName = "J.K.Rowling"},
                new Author { Id = 2, FullName = "J.R.R. Tolkien"},
            };

            var mockRepo = new Mock<IDeletableEntityRepository<Author>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);

            var service = new AuthorsService(mockRepo.Object);

            var result = service.GetAllAuthorsAsKeyValuePairs();

            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void GetAllAuthorsAsKvpReturnsCorrectData()
        {
            var list = new List<Author>
            {
                new Author { Id = 1, FullName = "J.K.Rowling"},
                new Author { Id = 2, FullName = "J.R.R. Tolkien"},
            };

            var mockRepo = new Mock<IDeletableEntityRepository<Author>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);

            var service = new AuthorsService(mockRepo.Object);

            var result = service.GetAllAuthorsAsKeyValuePairs();

            Assert.Equal(1, list.First().Id);
            Assert.Equal("J.R.R. Tolkien", list.Last().FullName);
        }

        [Fact]
        public void GetAllAuthorsAsKvpReturnsEmptyIfThereIsNoData()
        {
            var list = new List<Author>();

            var mockRepo = new Mock<IDeletableEntityRepository<Author>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);

            var service = new AuthorsService(mockRepo.Object);

            var result = service.GetAllAuthorsAsKeyValuePairs();

            Assert.Empty(result);
        }
    }
}
