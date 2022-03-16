namespace IndependentSocialApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IndependentSocialApp.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task<CommentResponseModel> CreateAsync(CreateCommentRequestModel model, string userId);

        Task<T> GetByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllCommentsByPostIdAsync<T>(CommentParams model, int postId);

        Task DeleteOwnCommentAsync(int commentId, string userId);

        Task EditAsync(UpdateCommentRequestModel model, string userId,int id);
    }
}
