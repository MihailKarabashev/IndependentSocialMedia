namespace IndependentSocialApp.Services.Data
{
    using System.Threading.Tasks;

    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Web.ViewModels.Follows;

    public interface IFollowService
    {
        Task FollowAsync(CreateFollowRequestModel model);

        Task UnFollowAsync(int id);

        Follow FindFollowById(int id);
    }
}
