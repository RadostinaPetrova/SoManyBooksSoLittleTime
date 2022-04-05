namespace SoManyBooksSoLittleTime.Web.ViewModels.Articles
{
    using AutoMapper;
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;

    public class ArticleInListViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public string CategoryTitle { get; set; }

        public string ImagePath { get; set; }

        public string UserUserName { get; set; }
    }
}
