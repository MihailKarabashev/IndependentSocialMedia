namespace IndependentSocialApp.Web.Controllers
{
    using System.Diagnostics;

    using IndependentSocialApp.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ApiController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.Ok("Hello");
        }
    }
}
