namespace IndependentSocialApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task<PostResponseModel> CreateAsync(CreatePostRequestModel model, string userId);

        Task<T> GetByIdAsync<T>(int id);

        Post FindPostById(int id);

        Task<IEnumerable<T>> GetAllAsync<T>(PostParams model);

        Task DeleteAsync(int id, string userId);

        Task EditAsync(int id, string userId, UpdatePostRequestModel model);
    }
}
