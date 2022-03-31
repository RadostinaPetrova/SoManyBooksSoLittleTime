namespace SoManyBooksSoLittleTime.Web.ViewModels.Faq
{
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;

    public class FaqViewModel : IMapFrom<FrequentlyAskedQuestion>
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }
    }
}
