namespace IndependentSocialApp.Services.Data
{
    using System.Threading.Tasks;

    using IndependentSocialApp.Web.ViewModels.Likes;

    public interface ILikesService
    {
        Task CreatePostLikeAsync(LikeRequestModel model, string userId);

        Task CreatePostUnlikeAsync(LikeRequestModel model, string userId);

        Task CreateCommentLikeAsync(LikeRequestModel model, string userId);

        Task CreateCommentUnlikeAsync(LikeRequestModel model, string userId);
    }
}
