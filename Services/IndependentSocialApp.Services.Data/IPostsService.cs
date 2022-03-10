namespace IndependentSocialApp.Services.Data
{
    using System.Threading.Tasks;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task<Post> CreateAsync(CreatePostRequestModel model, string userId);

        Task<T> GetByIdAsync<T>(int id);
    }
}
