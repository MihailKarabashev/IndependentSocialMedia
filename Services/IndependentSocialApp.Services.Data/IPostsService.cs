namespace IndependentSocialApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using IndependentSocialApp.Common;
    using IndependentSocialApp.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task<Result> CreateAsync(CreatePostRequestModel model, string userId);

        Task<T> GetByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<Result> DeleteAsync(int id, string userId);

        Task<Result> EditAsync(int id, string userId, UpdatePostRequestModel model);

    }
}
