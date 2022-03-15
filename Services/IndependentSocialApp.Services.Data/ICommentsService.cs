namespace IndependentSocialApp.Services.Data
{
    using System.Threading.Tasks;

    using IndependentSocialApp.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task<CreateCommentResponseModel> CreateAsync(CreateCommentRequestModel model, string userId);
    }
}
