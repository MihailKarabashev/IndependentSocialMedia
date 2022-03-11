namespace IndependentSocialApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task<Post> CreateAsync(CreatePostRequestModel model, string userId);

        Task<T> GetByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<bool> DeleteAsync(int id, string userId);

        Task<bool> EditAsync(int id, string userId, UpdatePostRequestModel model);

    }
}
