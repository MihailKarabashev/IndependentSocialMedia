namespace IndependentSocialApp.Web.Controllers
{
    using System.Threading.Tasks;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Services.Data;
    using IndependentSocialApp.Web.ViewModels.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using static IndependentSocialApp.Common.ModelValidations.Identity;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityController(
            IIdentityService identityService,
            UserManager<ApplicationUser> userManager)
        {
            this._identityService = identityService;
            this._userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            await this.ValidateRegisterModel(model);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this._identityService.RegisterAsync(model);

            return this.StatusCode(201);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var token = await this._identityService.LoginAsync(model);

            return this.Ok(token);
        }

        private async Task ValidateRegisterModel(RegisterRequestModel model)
        {
            if (await this._userManager.Users.AnyAsync(x => x.Email == model.Email))
            {
                this.ModelState.AddModelError(nameof(model.Email), EmailAlreadyExsist);
            }

            if (model.Password != model.ConfirmPassword)
            {
                this.ModelState.AddModelError(nameof(model.Password), PasswordDoNotMatch);
            }
        }
    }
}
