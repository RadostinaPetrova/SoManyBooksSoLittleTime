namespace SoManyBooksSoLittleTime.Web.ViewModels.Articles
{
    using SoManyBooksSoLittleTime.Data.Models;
    using SoManyBooksSoLittleTime.Services.Mapping;

    public class EditArticleInputModel : CreateArticleInputModel, IMapFrom<Article>
    {
        public int Id { get; set; }
    }
}
