namespace IndependentSocialApp.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using IndependentSocialApp.Data.Common.Repositories;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Web.ViewModels.Identity;
    using Microsoft.AspNetCore.Identity;

    public class IdentityService : IIdentityService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> _usersRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            UserManager<ApplicationUser> userManager)
        {
            this._usersRepository = usersRepository;
            this._userManager = userManager;
        }

        public async Task RegisterAsync(RegisterRequestModel model)
        {
            var user = new ApplicationUser()
            {
                UserName = model.Username,
                Email = model.Email,
            };

            var result = await this._userManager.CreateAsync(user, model.Password);

            // implement try/catch in global middleware
            if (!result.Succeeded)
            {
                throw new ArgumentException($"{result.Errors}");
            }
        }
    }
}
