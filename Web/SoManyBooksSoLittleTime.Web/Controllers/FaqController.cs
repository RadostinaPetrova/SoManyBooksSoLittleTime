namespace SoManyBooksSoLittleTime.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoManyBooksSoLittleTime.Data;
    using SoManyBooksSoLittleTime.Services.Data;
    using SoManyBooksSoLittleTime.Web.ViewModels.Faq;
    using System.Collections.Generic;
    using System.Linq;

    public class FaqController : BaseController
    {
        private readonly IFaqService faqService;

        public FaqController(IFaqService faqService)
        {
            this.faqService = faqService;
        }

        public IActionResult Index()
        {
            var model = new FaqListViewModel();
            model.Faqs = this.faqService.GetAll<FaqViewModel>();

            return this.View(model);
        }
    }
}
