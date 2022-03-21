namespace SoManyBooksSoLittleTime.Web.ViewModels.Books
{
    using System.Collections.Generic;

    public class BooksListViewModel : PagingViewModel
    {
        public IEnumerable<BookInListViewModel> Books { get; set; }
    }
}
