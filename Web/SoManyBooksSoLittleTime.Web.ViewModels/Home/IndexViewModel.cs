namespace SoManyBooksSoLittleTime.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int BooksCount { get; set; }

        public int AuthorsCount { get; set; }

        public int GenresCount { get; set; }

        public int ImagesCount { get; set; }

        public IEnumerable<IndexPageBookViewModel> RandomBooks { get; set; }
    }
}
