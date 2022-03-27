namespace SoManyBooksSoLittleTime.Web.ViewModels.SearchBooks
{
    using System.Collections.Generic;

    using SoManyBooksSoLittleTime.Web.ViewModels.Books;

    public class ListViewModel
    {
        public IEnumerable<BookInListViewModel> Books { get; set; }
    }
}
