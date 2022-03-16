namespace IndependentSocialApp.Services.Data
{
    using System.Threading.Tasks;

    using IndependentSocialApp.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task<CommentResponseModel> CreateAsync(CreateCommentRequestModel model, string userId);

        Task DeleteOwnCommentAsync(int commentId, string userId);

        Task EditAsync(UpdateCommentRequestModel model, string userId,int id);
    }
}
