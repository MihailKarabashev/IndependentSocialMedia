namespace IndependentSocialApp.Services.Data
{
    using System.Threading.Tasks;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Web.ViewModels.Identity;

    public interface IIdentityService
    {
        Task RegisterAsync(RegisterRequestModel model);

        Task<string> GenerateJwtToken(ApplicationUser user);

        Task<LoginResponseModel> LoginAsync(LoginRequestModel model);
    }
}
