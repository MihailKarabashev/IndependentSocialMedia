namespace IndependentSocialApp.Web.Controllers
{
    using System.Threading.Tasks;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Services.Data;
    using IndependentSocialApp.Web.Infrastructure.NloggerExtentions;
    using IndependentSocialApp.Web.ViewModels.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using static IndependentSocialApp.Common.NloggerMessages;

    using static IndependentSocialApp.Common.ModelValidations.Identity;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INloggerManager _nloggerManager;

        public IdentityController(
            IIdentityService identityService,
            UserManager<ApplicationUser> userManager,
            INloggerManager nloggerManager)
        {
            this._identityService = identityService;
            this._userManager = userManager;
            this._nloggerManager = nloggerManager;
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
                this._nloggerManager.LogError(LoginFailed);
                return this.BadRequest(this.ModelState);
            }

            var token = await this._identityService.LoginAsync(model);

            this._nloggerManager.LogInfo(LoginSucceed);

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
