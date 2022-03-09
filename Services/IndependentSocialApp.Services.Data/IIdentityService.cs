namespace IndependentSocialApp.Services.Data
{
    using System.Threading.Tasks;

    using IndependentSocialApp.Web.ViewModels.Identity;

    public interface IIdentityService
    {
        Task RegisterAsync(RegisterRequestModel model);
    }
}
