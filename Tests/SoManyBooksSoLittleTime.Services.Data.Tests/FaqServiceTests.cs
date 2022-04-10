namespace SoManyBooksSoLittleTime.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Moq;
    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;
    using SoManyBooksSoLittleTime.Web.ViewModels;
    using SoManyBooksSoLittleTime.Web.ViewModels.Faq;
    using Xunit;

    public class FaqServiceTests
    {
        [Fact]
        public void GetAllReturnNotNullIfDataIsProvided()
        {
            var list = new List<FrequentlyAskedQuestion>
            {
            new FrequentlyAskedQuestion
            {
                Question = "Question 1",
                Answer = "Answer 1",
            },
            new FrequentlyAskedQuestion
            {
                Question = "Question 2",
                Answer = "Answer 2",
            },
            };

            var mockRepo = new Mock<IRepository<FrequentlyAskedQuestion>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());

            var service = new FaqService(mockRepo.Object);

            Assert.NotNull(list);
        }

        [Fact]
        public void GetAllReturnRightCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var list = new List<FrequentlyAskedQuestion>
            {
            new FrequentlyAskedQuestion
            {
                Question = "Question 1",
                Answer = "Answer 1",
            },
            new FrequentlyAskedQuestion
            {
                Question = "Question 2",
                Answer = "Answer 2",
            },
            };

            var mockRepo = new Mock<IRepository<FrequentlyAskedQuestion>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());

            var service = new FaqService(mockRepo.Object);

            var result = service.GetAll<FaqViewModel>();

            Assert.Equal(2, list.Count());
        }

        [Fact]
        public void GetAllReturnsCorrectData()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var list = new List<FrequentlyAskedQuestion>
            {
            new FrequentlyAskedQuestion
            {
                Question = "Question 1",
                Answer = "Answer 1",
            },
            new FrequentlyAskedQuestion
            {
                Question = "Question 2",
                Answer = "Answer 2",
            },
            };

            var mockRepo = new Mock<IRepository<FrequentlyAskedQuestion>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());

            var service = new FaqService(mockRepo.Object);

            var result = service.GetAll<FaqViewModel>();

            Assert.Equal("Question 1", result.FirstOrDefault().Question);
            Assert.Equal("Answer 1", result.FirstOrDefault().Answer);
            Assert.Equal("Answer 2", result.Skip(1).First().Answer);
        }
    }
}
