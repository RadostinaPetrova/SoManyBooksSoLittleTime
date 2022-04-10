namespace SoManyBooksSoLittleTime.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SoManyBooksSoLittleTime.Data.Common.Repositories;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;

    public class FaqService : IFaqService
    {
        private readonly IRepository<FrequentlyAskedQuestion> faqRepository;

        public FaqService(IRepository<FrequentlyAskedQuestion> faqRepository)
        {
            this.faqRepository = faqRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var faqs = this.faqRepository
                .AllAsNoTracking()
                .OrderBy(x => x.Id)
                .To<T>()
                .ToList();

            return faqs;
        }
    }
}
