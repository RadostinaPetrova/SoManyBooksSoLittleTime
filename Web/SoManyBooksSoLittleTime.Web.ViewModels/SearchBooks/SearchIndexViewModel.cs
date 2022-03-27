namespace SoManyBooksSoLittleTime.Web.ViewModels.SearchBooks
{
    using System.Collections.Generic;

    public class SearchIndexViewModel
    {
        public IEnumerable<GenreNameIdViewModel> Genres { get; set; }
    }
}
