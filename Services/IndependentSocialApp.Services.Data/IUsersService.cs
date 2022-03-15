namespace IndependentSocialApp.Services.Data
{
    using System.Threading.Tasks;

    using IndependentSocialApp.Data.Models;

    public interface IUsersService
    {
        Task<ApplicationUser> FindUserAsync(string id);
    }
}
