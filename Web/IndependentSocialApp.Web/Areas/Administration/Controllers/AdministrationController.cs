namespace IndependentSocialApp.Web.Areas.Administration.Controllers
{
    using IndependentSocialApp.Common;
    using IndependentSocialApp.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
