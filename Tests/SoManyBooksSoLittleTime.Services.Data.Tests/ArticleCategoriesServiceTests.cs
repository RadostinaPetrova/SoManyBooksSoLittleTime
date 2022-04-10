namespace SoManyBooksSoLittleTime.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;
    using Xunit;

    public class ArticleCategoriesServiceTests
    {
        [Fact]
        public void GetAllArticleCategoriesReturnCorrectCount()
        {
            var list = new List<ArticleCategory>
            {
            new ArticleCategory
            {
                Id = 1,
                Title = "Test Title",
            },
            new ArticleCategory
            {
                Id = 2,
                Title = "Another test Title",
            },
            };

            var mockRepo = new Mock<IDeletableEntityRepository<ArticleCategory>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable);

            var service = new ArticleCategoriesService(mockRepo.Object);

            service.GetAllArticleCategories();

            Assert.Equal(2, list.Count());
        }

        [Fact]
        public void GetAllArticleCategoriesReturnsCorrectData()
        {
            var list = new List<ArticleCategory>
            {
            new ArticleCategory
            {
                Id = 1,
                Title = "Test Title",
            },
            new ArticleCategory
            {
                Id = 2,
                Title = "Another test Title",
            },
            };

            var mockRepo = new Mock<IDeletableEntityRepository<ArticleCategory>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable);

            var service = new ArticleCategoriesService(mockRepo.Object);

            var result = service.GetAllArticleCategories();

            Assert.Equal(1, result.First().Id);
            Assert.Equal("Test Title", result.First().Title);
            Assert.Equal("Another test Title", result.Skip(1).First().Title);
        }

        [Fact]
        public void GetAllArticleCategoriesReturnsEmptyIfNoDataIsProvided()
        {
            var list = new List<ArticleCategory>();

            var mockRepo = new Mock<IDeletableEntityRepository<ArticleCategory>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable);

            var service = new ArticleCategoriesService(mockRepo.Object);

            var result = service.GetAllArticleCategories();

            Assert.Empty(result);
        }
    }
}
