namespace IndependentSocialApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using IndependentSocialApp.Data.Common.Repositories;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Web.Common.ExecptionFactory.Auth;
    using IndependentSocialApp.Web.ViewModels.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using static IndependentSocialApp.Common.ModelValidations.Identity;

    public class IdentityService : IIdentityService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> _usersRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppSettings _appSettings;

        public IdentityService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            UserManager<ApplicationUser> userManager,
            IOptions<AppSettings> appSettings)
        {
            this._usersRepository = usersRepository;
            this._userManager = userManager;
            this._appSettings = appSettings.Value;
        }

        public async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._appSettings.Secret);

            List<string> roles = (await this._userManager.GetRolesAsync(user)).ToList();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object>()
                {
                    { ClaimTypes.NameIdentifier, user.Id },
                    { ClaimTypes.Name, user.UserName },
                    { ClaimTypes.Email, user.Email },
                },
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            foreach (string role in roles)
            {
                tokenDescriptor.Claims.Add(ClaimTypes.Role, role);
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel model)
        {
            var user = await this._userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new AuthNotFoundException(UserNotFound);
            }

            var checkPassword = await this._userManager.CheckPasswordAsync(user, model.Password);

            if (!checkPassword)
            {
                throw new AuthUnAuthorizedException(IncorrectEmailOrPassword);
            }

            var generateToken = await this.GenerateJwtToken(user);

            this._usersRepository.Update(user);
            await this._usersRepository.SaveChangesAsync();

            return new LoginResponseModel
            {
                Token = generateToken,
            };
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
                throw new AuthBadRequestException($"{result.Errors}");
            }
        }
    }
}
