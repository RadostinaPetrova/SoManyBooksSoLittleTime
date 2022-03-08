namespace SoManyBooksSoLittleTime.Web.Areas.Administration.Controllers
{
    using SoManyBooksSoLittleTime.Common;
    using SoManyBooksSoLittleTime.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
