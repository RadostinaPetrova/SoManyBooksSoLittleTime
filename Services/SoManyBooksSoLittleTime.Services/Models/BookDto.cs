using System.Collections.Generic;

namespace SoManyBooksSoLittleTime.Services.Models
{
    public class BookDto
    {
        public BookDto()
        {
            this.Genres = new List<string>();
        }

        public string TitleName { get; set; }

        public string AuthorName { get; set; }

        public string Description { get; set; }

        public decimal Rating { get; set; }

        public string ISBN { get; set; }

        public string ImageUrl { get; set; }

        public List<string> Genres { get; set; }
    }
}
