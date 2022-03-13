namespace IndependentSocialApp.Web.Controllers
{
    using IndependentSocialApp.Web.Infrastructure.CustomFilters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}
